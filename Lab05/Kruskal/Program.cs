Graph graph = new Graph(7, 11);
graph.edge[0] = new Edge(0,1,8);
graph.edge[1] = new Edge(0,2,21);
graph.edge[2] = new Edge(1,2,14);
graph.edge[3] = new Edge(1,3,2);
graph.edge[4] = new Edge(2,3,25);
graph.edge[5] = new Edge(2,4,17);
graph.edge[6] = new Edge(2,5,13);
graph.edge[7] = new Edge(3,4,19);
graph.edge[8] = new Edge(4,5,5);
graph.edge[9] = new Edge(4,6,9);
graph.edge[10] = new Edge(5,6,1);


KruskalMST(graph);
Console.WriteLine("-----------------");
BoruvkaMST(graph);

//Vraca set u kome se nalazi vertex
int Find(Set[] Sets, int v) {
	if (Sets[v].Parent != v)
		Sets[v].Parent = Find(Sets, Sets[v].Parent);

	return Sets[v].Parent;
}

//Spajanje set-ove na osnovu rank-a
void Union(Set[] Sets, int v1, int v2) {
	int v1root = Find(Sets, v1);
	int v2root = Find(Sets, v2);

	if (Sets[v1root].Rank < Sets[v2root].Rank)
		Sets[v1root].Parent = v2root;
	else if (Sets[v1root].Rank > Sets[v2root].Rank)
		Sets[v2root].Parent = v1root;
	else {
		Sets[v2root].Parent = v1root;
		Sets[v1root].Rank = Sets[v1root].Rank + 1;
	}
}

void KruskalMST(Graph graph) {
	int verticesCount = graph.VerticesCount;
	Edge[] result = new Edge[verticesCount];

	graph.edge = graph.edge.OrderBy(item => item.Weight).ToArray();
	Set[] Sets = new Set[verticesCount];

	for (int v = 0; v < verticesCount; ++v)
		Sets[v] = new Set(v, 0);

	int i = 0;
	int edge = 0;
    int sum = 0;
	while (edge < verticesCount - 1) {
		Edge nextEdge = graph.edge[i++];
		int sourceSet = Find(Sets, nextEdge.Source);
		int destSet = Find(Sets, nextEdge.Destination);

		if (sourceSet != destSet) {
			result[edge++] = nextEdge;
			Union(Sets, sourceSet, destSet);
		}
	}

	for (int j = 0; j < edge; ++j) {
		Console.WriteLine($"Od {result[j].Source} do {result[j].Destination} put je {result[j].Weight}");
		sum = sum + result[j].Weight;
	}
	Console.WriteLine("[KruskalMST] Tezina je: " + sum); 
}

void BoruvkaMST(Graph graph) {
    int verticesCount = graph.VerticesCount;
    int edgeCount = graph.EdgesCount;
    Edge[] edges = graph.edge;

    Set[] sets = new Set[verticesCount];
    int[] cheapest = new int[verticesCount];

    int comp = verticesCount;
    for (int v = 0; v < verticesCount; v++) {
        sets[v] = new Set(v,0);
    }

   	int sum = 0;
    while (comp > 1) {

		//Reset-ujemo "cheapest" za svaku iteraciju
        for (int i = 0; i < verticesCount; i++)
            cheapest[i] = int.MinValue;

		//Nalazimo najjeftiniju ivicu
        for (int i = 0; i < edgeCount; i++) {

            int sourceSet = Find(sets, edges[i].Source);
            int destSet = Find(sets, edges[i].Destination);

            if (sourceSet != destSet) {
				//Provera koji set je najjeftiniji 
                if (cheapest[sourceSet] == int.MinValue || edges[cheapest[sourceSet]].Weight > edges[i].Weight)
                    cheapest[sourceSet] = i;

                if (cheapest[destSet] == int.MinValue || edges[cheapest[destSet]].Weight > edges[i].Weight)
                    cheapest[destSet] = i;
            }
        }

		//Dodajemo u sumi za tezinu i proveravamo dal cheapest za trenutni set postoji
        for (int i = 0; i < verticesCount; i++) {
            if (cheapest[i] != int.MinValue) {
                int sourceSet = Find(sets, edges[cheapest[i]].Source);
                int destSet = Find(sets, edges[cheapest[i]].Destination);

                if (sourceSet != destSet) {
					Console.WriteLine($"Od {edges[cheapest[i]].Source} do {edges[cheapest[i]].Destination} put je {edges[cheapest[i]].Weight}");
                    sum = sum + edges[cheapest[i]].Weight;
                    Union(sets, sourceSet, destSet);
                    comp--;
                }
            }
        }
    }
    Console.WriteLine("[BoruvkaMST] Tezina je: " + sum);    
}

public class Edge {
	public int Source;
	public int Destination;
	public int Weight;

	public Edge(int s, int d, int w) {
		this.Source = s;
		this.Destination = d;
		this.Weight = w;
	}
}

public class Graph {
	public int VerticesCount;
	public int EdgesCount;
	public Edge[] edge;

	public Graph (int vc, int ec) {
		this.VerticesCount = vc;
		this.EdgesCount = ec;
		this.edge = new Edge[this.EdgesCount];
	}
}

public class Set {
	public int Parent;
	public int Rank;

	public Set(int p, int r) {
		this.Parent = p;
		this.Rank = r;
	}
}
