using System;
using System.Collections.Generic;
using System.Linq;

class PrimAlgorithmAdjacencyMatrix
{
    // Prim's mimimum spanning tree (MTS) algorithm, implemented
    // with adjacency matrix. Running time: O(N * N)
    // Learn more at: https://en.wikipedia.org/wiki/Prim%27s_algorithm

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

        var minimumSpanningForest = Prim(n, graphEdges);

        Console.WriteLine("Minimum spanning forest weight: " +
            minimumSpanningForest.Sum(e => e.Weight));
        foreach (var edge in minimumSpanningForest)
        {
            Console.WriteLine(edge);
        }
    }

    static List<Edge> Prim(int n, List<Edge> graphEdges)
    {
        // Build the graph adjacency matrix
        var graphMatrix = new int?[n, n];
        foreach (var edge in graphEdges)
        {
            graphMatrix[edge.StartNode, edge.EndNode] = edge.Weight;
            graphMatrix[edge.EndNode, edge.StartNode] = edge.Weight;
        }

        // Start Prim's algorithm from each node not still in the spanning tree
        var usedNodes = new bool[n];
        var spannngTreeEdges = new List<Edge>();
        for (int startNode = 0; startNode < n; startNode++)
        {
            if (!usedNodes[startNode])
            {
                Prim(n, graphMatrix, startNode, usedNodes, spannngTreeEdges);
            }
        }

        return spannngTreeEdges;        
    }

    static void Prim(int n, int?[,] graphMatrix, int startNode,
        bool[] usedNodes, List<Edge> spannngTreeEdges)
    {
        usedNodes[startNode] = true;
        var edgeNode = new int[n];
        var nearest = new int[n];

        // Compute the nearest nodes from the start node
        for (int i = 0; i < n; i++)
        {
            nearest[i] = int.MaxValue;
            if (graphMatrix[startNode, i] != null)
            {
                nearest[i] = graphMatrix[startNode, i].Value;
                edgeNode[i] = startNode;
            }
        }

        // Prim's algorithm body
        while (true)
        {
            // Find the next minimal edge to be added to the spanning tree
            int minDist = int.MaxValue;
            int nearestNode = 0;
            for (int i = 0; i < n; i++)
            {
                if (!usedNodes[i] && nearest[i] < minDist)
                {
                    minDist = nearest[i];
                    nearestNode = i;
                }
            }
            if (minDist == int.MaxValue)
            {
                // No next minimal edge to add t the spanning tree --> stop
                return;
            }

            usedNodes[nearestNode] = true;
            spannngTreeEdges.Add(
                new Edge(edgeNode[nearestNode], nearestNode, minDist));

            // Update the nearest[0...n-1] distances through nearestNode
            for (int i = 0; i < n; i++)
            {
                if (!usedNodes[i] && graphMatrix[nearestNode, i] != null &&
                    graphMatrix[nearestNode, i] < nearest[i])
                {
                    nearest[i] = graphMatrix[nearestNode, i].Value;
                    edgeNode[i] = nearestNode;
                }
            }
        }
    }
}
