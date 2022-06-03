int min = 0;
int max = 10000;
Random RandNum = new Random();
BinomialHeap h = new BinomialHeap();

int[] LVL0 = Enumerable.Repeat(0, 10).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL1 = Enumerable.Repeat(0, 100).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL2 = Enumerable.Repeat(0, 1000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL3 = Enumerable.Repeat(0, 10000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL4 = Enumerable.Repeat(0, 100000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL5 = Enumerable.Repeat(0, 1000000).Select(i => RandNum.Next(min, max)).ToArray();
int[] LVL6 = Enumerable.Repeat(0, 10000000).Select(i => RandNum.Next(min, max)).ToArray();

int[] usedLVL = LVL0;
foreach(int i in usedLVL) {
    //h.insert(i);
}
//h.print();
Console.WriteLine("---------");

BinomialHeap test = new BinomialHeap();
h.insert(10).insert(1).print();
Console.WriteLine("---------");
h.merge((new BHNode(0)).merge(new BHNode(5))).print();
Console.WriteLine("---------");
h.decreaseKey(h.heap.child, -1).print();




class BHNode {
    public int key; 
    public int rank; 

    public BHNode parent;
    public BHNode child;
    public BHNode sibling; // linked list

    public BHNode(int k = 0, int rank = 0) {
        this.key = k; 
        this.rank = rank;
        this.parent = null;
        this.child = null;
        this.sibling = null;
    }


    public BHNode merge(BHNode h2) {
        if(this.rank == h2.rank) {
            //Spajamo 2 heap sa istim rankom
            BHNode maxK = this.key > h2.key ? this : h2;
            BHNode minK = this == maxK ? h2 : this;

            maxK.sibling = minK.child;
            maxK.parent = minK;

            minK.rank++;
            minK.child = maxK;
            //Nastavljamo spajanje ako rezultujuci heap ima sibling-a sa istim rankom
            if((minK.sibling != null) && minK.rank == minK.sibling.rank) {
                BHNode sib = minK.sibling;
                minK.sibling = null;

                return sib.merge(minK);
            }
            else
                return minK;
        }
        else {
            BHNode maxR = this.rank > h2.rank? this : h2;
            BHNode minR = this == maxR? h2 : this;

            if(minR.sibling != null)
                minR.sibling = minR.sibling.merge(maxR);
            else
                minR.sibling = maxR;
            return minR;
        }
    }

    public void print(int level = 0) {
        for(int i = 0; i < level; i++)
            Console.Write("  ");
        Console.WriteLine(key + " [" + rank + "]");
        
        if(child != null) child.print(level + 1);
        if(sibling != null) sibling.print(level);
    }
}

class BinomialHeap {
    public BHNode heap;

    public BinomialHeap() { heap = null; }

    void swap(ref BHNode n1, ref BHNode n2) {
    BHNode tmp = new BHNode();
    tmp = n1;
    n1 = n2;
    n2 = tmp;
    }

    void swapInt(ref int i1, ref int i2) {
        int tmp = i1;
        i1 = i2;
        i2 = tmp;
    }

    public BinomialHeap merge(BHNode h) {
        if(heap != null)
            heap = heap.merge(h);
        else
            heap = h;
        return this;
    }

    public BinomialHeap insert(int n) {
        return merge(new BHNode(n));
    }

    public BinomialHeap merge(BinomialHeap h2) {
        BHNode cur = h2.heap;
        while(cur != null) {
            BHNode next = cur.sibling;
            
            this.merge(cur);
            cur = next;
        }
        return this;
    }

    public int popMin() {
        if(heap == null) {
            Console.WriteLine("Empty heap");
            return 0;
        }

        BHNode min = heap;
        BHNode minPrev = heap;
        //Trazimo minimum u nulti rank
        for(BHNode cur = heap.sibling, prev = heap; cur != null; prev = cur, cur = cur.sibling) {
           if(cur.key < min.key) {
               minPrev = prev;
               min = cur;
           }
        }
        //Izbacujemo min iz linked liste
        if(min == heap) 
            heap = min.sibling;
        else 
            minPrev.sibling = min.sibling;

        int val = min.key;

        //Decu od min dodajemo u nulti nivo
        BHNode next = null;
        for(BHNode child = min.child; child != null; child = next) {
            next = child.sibling;
            child.parent = null;
            child.sibling = null;

            this.merge(child);
        }
        return val;
    }

    public BinomialHeap decreaseKey(BHNode n, int k) {
        if(k > n.key) {
            Console.WriteLine("New key value bigger than the old one");
            return null;
        }

        n.key = k;
        //Swapujemo key izmedju deteta i roditelja do odgvarajuce pozicije
        while((n.parent != null) && (n.key < n.parent.key)) {
            this.swapInt(ref n.key, ref n.parent.key);             
            n = n.parent;
        }
        return this;
    }

    public BinomialHeap remove(BHNode n) {
         decreaseKey(n, Int32.MinValue).popMin();
         return this;
    }

    public void print() {
        if(heap != null) heap.print();
    }
}





