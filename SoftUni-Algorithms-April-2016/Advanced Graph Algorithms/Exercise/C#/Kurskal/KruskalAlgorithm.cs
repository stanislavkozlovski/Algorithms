namespace Kurskal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KruskalAlgorithm
    {
        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            //Initialize parents
            var parent = new int[numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
            {
                parent[i] = i; // every node is its own parent for now
            }

            //Kruskal's algorithm
            var minSpanningTree = new List<Edge>();
            var orderedEdges = edges.OrderBy(e => e.Weight);
            foreach (var edge in orderedEdges)
            {
                int rootStartNode = FindRoot(edge.StartNode, parent);
                int rootEndNode = FindRoot(edge.EndNode, parent);

                if(rootStartNode != rootEndNode)
                {
                    minSpanningTree.Add(new Edge(edge.StartNode, edge.EndNode, edge.Weight));
                    parent[rootEndNode] = rootStartNode;
                }
            }
            return minSpanningTree;
        }

        public static int FindRoot(int node, int[] parent)
        {
            int root = node;

            while (parent[root] != root)
                root = parent[root];

            //Optimize the path from node to root by saving the nodes we've traversed's roots as Root
            while(node != root)
            {
                node = parent[node];
                parent[node] = root;
            }

            return root;
        }
    }
}
