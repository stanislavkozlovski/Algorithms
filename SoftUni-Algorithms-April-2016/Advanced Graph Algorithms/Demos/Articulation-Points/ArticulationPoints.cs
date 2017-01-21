using System;
using System.Collections.Generic;

class ArticulationPoints
{
    // Hopcroft-Tarjan algorithm for finding articulation points in
    // undirected connected graph, implemented with DFS + calculations.
    // Running time: O(N + M)
    // Learn more at: https://en.wikipedia.org/wiki/Biconnected_component

    static List<int>[] graph = new List<int>[]
    {
        new List<int>() {1, 2, 6, 7, 9},      // children of node 0
        new List<int>() {0, 6},               // children of node 1
        new List<int>() {0, 7},               // children of node 2
        new List<int>() {4},                  // children of node 3
        new List<int>() {3, 6, 10},           // children of node 4
        new List<int>() {7},                  // children of node 5
        new List<int>() {0, 1, 4, 8, 10, 11}, // children of node 6
        new List<int>() {0, 2, 5, 9},         // children of node 7
        new List<int>() {6, 11},              // children of node 8
        new List<int>() {0, 7},               // children of node 9
        new List<int>() {4, 6},               // children of node 10
        new List<int>() {6, 8},               // children of node 11
    };

    static bool[] visited = new bool[graph.Length];
    static int?[] parent = new int?[graph.Length];
    static int[] depth = new int[graph.Length];
    static int[] lowpoint = new int[graph.Length];
    static List<int> articulationPoints = new List<int>();

    static void Main()
    {
        FindArticulationPoints(0, 0);
        Console.WriteLine("Articulation points: " +
            string.Join(", ", articulationPoints));
    }

    static void FindArticulationPoints(int node, int d)
    {
        visited[node] = true;
        depth[node] = d;
        lowpoint[node] = d;
        int childCount = 0;
        bool isArticulation = false;
        foreach (var childNode in graph[node])
        {
            if (!visited[childNode])
            {
                parent[childNode] = node;
                FindArticulationPoints(childNode, d + 1);
                childCount = childCount + 1;
                if (lowpoint[childNode] >= depth[node])
                {
                    isArticulation = true;
                }
                lowpoint[node] = Math.Min(lowpoint[node], lowpoint[childNode]);
            }
            else if (childNode != parent[node])
            {
                lowpoint[node] = Math.Min(lowpoint[node], depth[childNode]);
            }
        }
        if ((parent[node] != null && isArticulation) || 
            (parent[node] == null && childCount > 1))
        {
            articulationPoints.Add(node);
        }
    }
}
