FibHeap h1 = (new FibHeap()).insert(5).insert(4);
FibHeap h2 = (new FibHeap()).insert(7).insert(0);

Console.WriteLine("---------");
h2.print();
Console.WriteLine("---------");
h1.merge(h2).print();
Console.WriteLine("---------");
h1.consolidate().print();
Console.WriteLine("---------");
h1.decreaseKey(h1.min.child, -1).print();
Console.WriteLine("---------");
Console.WriteLine("Min: "+ h1.popMin());
h1.print();

static class Constant {
    public const int maxLevels = 50;
}

class FHNode {
    public int key;
    public int deg;
    public bool mark;

    public FHNode parent;
    public FHNode child; // lancana lista
    public FHNode next;
    public FHNode prev;

    public FHNode(int k = 0, int d = 0) {
            this.key = k;
            this.deg = d;
            this.mark = false;

            this.parent = null;
            this.child = null;
            this.next = this;
            this.prev = this;
        }

    //Umecemo jednu link listu unutar druge
    public FHNode merge(FHNode n2) {
        if(n2 == null) 
            return this;

        FHNode thisStart = this;
        FHNode thisEnd = this.prev;

        FHNode n2Start = n2;
        FHNode n2End = n2.prev;

        thisStart.prev = n2End;
        thisEnd.next = n2Start;
        n2Start.prev = thisEnd;
        n2End.next = thisStart;

        return this.key < n2.key ? this : n2;
    }

    public void clear() {
        parent = null;
        next = this;
        prev = this;
    }

    public void print(int level = 0) {
        if(level > Constant.maxLevels) {
            Console.WriteLine("Too many levels");
            return;
        }
        
        FHNode cur = this;

        do {
            if(cur == null) {
                Console.WriteLine("Cyclic list has null.");
                return;
            }

            for(int i = 0; i < level; i++)
                Console.Write("  ");
            Console.WriteLine(cur.key + " [" + cur.deg + "]");
            
            if(cur.child != null) cur.child.print(level + 1);
            cur = cur.next; 
        } while (cur != this);
    }
}

class FibHeap {
    public FHNode min;
    public int len; //broj elemenata

    public FibHeap(FHNode root = null) {
        this.min = root;
    }

    public void print() {
        if(min != null)
            min.print();
    }
    
    public FibHeap merge(FHNode n2) {
        len += 1;
        if(min == null) {
            min = n2;
        }
        else {
            min = min.merge(n2);
        }
        return this;
    }

    public FibHeap merge(FibHeap h2) {
        len += h2.len-1;
        return merge(h2.min);
    }

    public FibHeap insert(int k2) {
        return merge(new FHNode(k2));
    }
    //Radimo basic cut
    public FibHeap cut(FHNode p, FHNode c) {
        //Sklanjamo ga iz roditelja
        if(c.parent != null) {
            if(c.parent.child == c) {
                if(c.next == c) {
                    c.parent.child = null;
                }
                else {
                    c.parent.child = c.next;
                }
            }
            c.parent.deg -= 1;
        }
        //Izbacujemo ga iz siblings
        c.next.prev = c.prev;
        c.prev.next = c.next;

        c.clear();
        c.mark =false;

        return merge(c);
    }
    //Proverava dal je roditelj markina i seca ga ako jeste i tako rekurizvno prolazi kroz sve roditelje
    public FibHeap cascadingCut(FHNode n) {
        if(n != null && n.parent != null) {
            if(!n.mark)
                n.mark = true;
            else
                return cut(n.parent, n).cascadingCut(n.parent);
        }
        return this;
    }

    public FibHeap decreaseKey(FHNode n, int k) {
        if(n.key < k) {
            Console.WriteLine("Decrease: New key is greater than the current key");
            return null;
        }        
        n.key = k;

        if((n.parent != null) && (n.parent.key > n.key)) {
            return cut(n.parent, n).cascadingCut(n.parent);  
        }
        else {
            if(n.key < min.key)
                min = n;
            return this;
        }
    }

    public FibHeap remove(FHNode n) {
        decreaseKey(n, Int32.MinValue).popMin();
        return this;
    }

    public int popMin() {
        if(min != null) {
            //Svu decu MIN node izbacuje na nulti nivo
            if(min.child != null) {
                FHNode cur = min.child;
                do {
                    FHNode next = cur.next;
                    cur.clear();

                    len += 1;
                    min.merge(cur);

                    cur = next;
                } while(cur != min.child);
            }
            //Izbacuje min iz linked liste
            FHNode node = min;
            
            if(min == min.next) {
                min = null; 
            }
            else {
                min.prev.next = min.next;
                min.next.prev = min.prev;
                min = min.next;
                consolidate();
                len -= 1;
            }

            return node.key;
        }
        else {
            Console.WriteLine("Popping empty heap");
            return 0;
        }
    }
    
    public FibHeap consolidate() {
        //Pomocni array za preuredjivanje na osnovu rank-a
        List<FHNode> levels = new List<FHNode>();
        for (int i = 0; i < Math.Floor(Math.Log((len),2)+1); i++ ) {
            levels.Insert(i, null);
        }
        FHNode cur = min;
        do {
            FHNode next = cur.next;

            cur.clear();

            while(levels[cur.deg] != null) {
                FHNode minN = cur.key < levels[cur.deg].key ? cur : levels[cur.deg];
                FHNode maxN = cur == minN ? levels[cur.deg] : cur;

                minN.child = maxN.merge(minN.child);
                maxN.parent = minN;
                maxN.mark = false;
                
                levels[cur.deg] = null;

                cur = minN;
                cur.deg += 1;
            }
            levels[cur.deg] = cur;

            cur = next;
        } while(cur != min);
        
        min = null;
        foreach(FHNode n in levels) {
            if(n != null) {
                min = n.merge(min);
            }
        }
        return this; 
    }
}