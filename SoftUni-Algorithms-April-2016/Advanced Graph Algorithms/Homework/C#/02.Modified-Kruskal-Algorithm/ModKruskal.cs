using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


class ModKruskal
{
    static HashSet<int> graphNodes;
    static List<Edge> graphEdges;
    static List<Edge> graph;
    static void Main()
    {
        int nodes = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        int edges = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        ReadInput(); // you need to type "END" for it to stop reading

        graph = Kruskal(nodes, graphEdges);
        Console.WriteLine("Minimum spanning forest weight: {0}", graph.Sum(x => x.Value));
        foreach (var edge in graph)
        {
            Console.WriteLine(edge);
        }
    }
    static List<Edge> Kruskal(int n, List<Edge> edges)
    {
        edges.Sort();

        // Initialize parents
        var parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }

        // Kruskal's algorithm
        var spanningTree = new List<Edge>();
        foreach (var edge in edges)
        {
            int rootStartNode = FindRoot(edge.StartNode, parent);
            int rootEndNode = FindRoot(edge.EndNode, parent);
            if (rootStartNode != rootEndNode)
            {
                spanningTree.Add(edge);

                // Union (merge) the trees
                parent[rootStartNode] = rootEndNode;
            }
        }

        return spanningTree;
    }
    static int FindRoot(int node, int[] parent)
    {
        // Find the root parent for the node
        int root = node;
        while (parent[root] != root)
        {
            root = parent[root];
        }

        // Optimize (compress) the path from node to root
        while (node != root)
        {
            var oldParent = parent[node];
            parent[node] = root;
            node = oldParent;
        }

        return root;
    }
    public static void ReadInput()
    {
        graphNodes = new HashSet<int>();
        graphEdges = new List<Edge>();

        while (true)
        {
            // Read the input and fill the arrays
            string input = Console.ReadLine();

            if (input == "END")
                break;
            Match match = Regex.Match(input, @"(\d+)\s+(\d+)\s+(\d+)");
            int startNode = int.Parse(match.Groups[1].Value);
            int endNode = int.Parse(match.Groups[2].Value);
            int value = int.Parse(match.Groups[3].Value);


            graphEdges.Add(new Edge(startNode, endNode, value));           
        }
    }

    class Edge : IComparable<Edge>
    {
        public int Value { get; set; }
        public int EndNode { get; set; }
        public int StartNode { get; set; }
        public int CompareTo(Edge other)
        {
            int weightCompared = this.Value.CompareTo(other.Value);
            return weightCompared;
        }
        public Edge(int startNode, int endNode, int value)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Value = value;
        }
        public override string ToString()
        {
            return string.Format("({0} {1}) -> {2}",
                this.StartNode, this.EndNode, this.Value);
        }
    }
}


