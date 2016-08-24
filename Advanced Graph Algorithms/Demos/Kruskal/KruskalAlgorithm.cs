using System;
using System.Collections.Generic;
using System.Linq;

class KruskalAlgorithm
{
    // Kruskal's mimimum spanning tree (MST) algorithm, implemented
    // with disjoint sets. Running time: O(M * log M)
    // Learn more at: https://en.wikipedia.org/wiki/Kruskal%27s_algorithm

    static void Main()
    {
        int n = 9;
        var graphEdges = new List<Edge>()
        {
            new Edge(0, 3, 9),
            new Edge(0, 5, 4),
            new Edge(0, 8, 5),
            new Edge(1, 4, 8),
            new Edge(1, 7, 7),
            new Edge(2, 6, 12),
            new Edge(3, 5, 2),
            new Edge(3, 6, 8),
            new Edge(3, 8, 20),
            new Edge(4, 7, 10),
            new Edge(6, 8, 7)
        };

        var minimumSpanningForest = Kruskal(n, graphEdges);

        Console.WriteLine("Minimum spanning forest weight: " +
            minimumSpanningForest.Sum(e => e.Weight));
        foreach (var edge in minimumSpanningForest)
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
}
