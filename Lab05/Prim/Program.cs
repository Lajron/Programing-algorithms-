int[][] vertMat = new int[][] { 
    new int[] { 0,8,21,0,0,0,0 }, 
    new int[] { 8,0,14,2,0,0,0 }, 
    new int[] { 21,14,0,25,17,13,0 }, 
    new int[] { 0,2,25,0,19,0,0 }, 
    new int[] { 0,0,17,19,0,5,9 },
    new int[] { 0,0,13,0,5,0,1 },
    new int[] { 0,0,0,0,9,1,0 } 
};

primMST(vertMat);

static void primMST(int[][] graph) {

    int vertexCount = graph[0].Length;
    List<Vertex> list = new List<Vertex>();
    Vertex[] vertices = new Vertex[vertexCount];

    for (int i = 0; i < vertexCount; i++)
        vertices[i] = new Vertex(int.MaxValue, int.MinValue, i);
    vertices[0].key = 0;

    for (int i = 0; i < vertexCount; i++)
        list.Add(vertices[i]);
    list = list.OrderBy(q => q.key).ToList();

    while (list.Count > 0) {
        Vertex minVertex = list[0];
        list.RemoveAt(0);

        int minVertNum = minVertex.vertex;
        vertices[minVertNum].isDone = true;

        int[] edges = graph[minVertex.vertex];
        for (int v = 0; v < edges.Length; v++) {
            if (graph[minVertNum][v] > 0 && vertices[v].isDone == false && graph[minVertNum][v] < vertices[v].key) {
                
                vertices[v].parent = minVertNum;
                vertices[v].key = graph[minVertNum][v];

                Vertex obj = list.Where(x => x.key == vertices[v].key).FirstOrDefault();
                if (obj != null) obj = vertices[v];
                list = list.OrderBy(q => q.key).ToList();
            }
        }
    }

    int totalWeight = 0;
    foreach (Vertex vertex in vertices) {
        if (vertex.parent >= 0) {
            Console.WriteLine($"Od {vertex.vertex} do {vertex.parent} put je {vertex.key}");
            totalWeight += vertex.key;
        }
    }
    Console.WriteLine("[PrimMST] Tezina je: "+ totalWeight);
}


public class Vertex {
    public int key;
    public int parent;
    public int vertex;
    public bool isDone;

    public Vertex(int key, int parent, int vertex) {
        this.key = key;
        this.parent = parent;
        this.vertex = vertex;
    }
}