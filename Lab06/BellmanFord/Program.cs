Graph g = new Graph(5, 8);

g.edges[0] = new Edge(0, 1, -1);
g.edges[1] = new Edge(0, 2, 4);
g.edges[2] = new Edge(1, 2, 3);
g.edges[3] = new Edge(1, 3, 2);
g.edges[4] = new Edge(1, 4, 2);
g.edges[5] = new Edge(3, 2, 5);
g.edges[6] = new Edge(3, 1, 1);
g.edges[7] = new Edge(4, 3, -10);

BellmanFord(g);
void BellmanFord(Graph graph) {

    int vertCount = graph.vertCount;
    int edgesCount = graph.edgesCount;
    int[] dist = new int[vertCount];

    for (int i = 0; i < vertCount; i++)
        dist[i] = int.MaxValue;
    dist[0] = 0;
    
    //Opustanje ivica
    for (int i = 1; i < vertCount-1; i++)
        foreach(Edge edge in graph.edges)
            //Update-ujemo trenutno dist sa kracim putem
            if (dist[edge.source] != int.MaxValue && dist[edge.source] + edge.weight < dist[edge.destination])
                dist[edge.destination] = dist[edge.source] + edge.weight;

    //Provera za negativni ciklus
    foreach (Edge edge in graph.edges) {
        if (dist[edge.source] != int.MaxValue && dist[edge.source] + edge.weight < dist[edge.destination]) {
                Console.WriteLine("Negativan ciklus");
                dist[edge.destination] = int.MinValue;
                return;
            }
    }
    
    for (int i = 0; i < vertCount; ++i)
        Console.WriteLine($"Vertex {i} je udaljen od 0 vertex za {dist[i]}");
}

public class Graph {
    public int vertCount;
    public int edgesCount;
    public Edge[] edges;

    public Graph(int vertCount, int edgesCount) {
        this.vertCount = vertCount;
        this.edgesCount = edgesCount;
        this.edges = new Edge[this.edgesCount];
    }        
}

public class Edge {
    public int source;
    public int destination;
    public int weight;

    public Edge(int source, int destination, int weight) {
        this.source = source;
        this.destination = destination;
        this.weight = weight;
    }
}