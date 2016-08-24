using System;
using System.Collections.Generic;

class StronglyConnectedComponents
{
    // Kosaraju–Sharir algorithm for finding the strongly-connected
    // components in directed graph, implemented with DFS + reverse DFS.
    // Running time: O(N + M)
    // Learn more at: https://en.wikipedia.org/wiki/Kosaraju%27s_algorithm

    static int n;
    static List<int>[] graph;
    static List<int>[] reverseGraph;
    static bool[] visited;
    static Stack<int> dfsNodesStack;

    static void BuildReverseGraph()
    {
        reverseGraph = new List<int>[n];
        for (int node = 0; node < n; node++)
        {
            reverseGraph[node] = new List<int>();
        }

        for (int node = 0; node < n; node++)
        {
            foreach (var childNode in graph[node])
            {
                // Add the reverse edge {childNode -> node}
                reverseGraph[childNode].Add(node);
            }
        }
    }

    static void DFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            foreach (var childNode in graph[node])
            {
                DFS(childNode);
            }
            dfsNodesStack.Push(node);
        }
    }

    static void ReverseDFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            Console.Write(" {0}", node);
            foreach (var childNode in reverseGraph[node])
            {
                ReverseDFS(childNode);
            }
        }
    }

    static void FindStronglyConnectedComponents()
    {
        n = graph.Length;
        BuildReverseGraph();

        // Traverse the graph with DFS and push all nodes in the stack
        // in post-order (on return from recursion)
        visited = new bool[n];
        dfsNodesStack = new Stack<int>();
        for (int node = 0; node < n; node++)
        {
            DFS(node);
        }

        // Traverse the nodes from the stack and perform reverse DFS
        // to find the strongly-connected components
        visited = new bool[n];
        while (dfsNodesStack.Count > 0)
        {
            var node = dfsNodesStack.Pop();
            if (!visited[node])
            {
                Console.Write("{");
                ReverseDFS(node);
                Console.WriteLine(" }");
            }
        }
    }
    static void Main()
    {
        graph = new List<int>[]
        {
            new List<int>() {1, 11, 13}, // children of node 0
            new List<int>() {6},         // children of node 1
            new List<int>() {0},         // children of node 2
            new List<int>() {4},         // children of node 3
            new List<int>() {3, 6},      // children of node 4
            new List<int>() {13},        // children of node 5
            new List<int>() {0, 11},     // children of node 6
            new List<int>() {12},        // children of node 7
            new List<int>() {6, 11},     // children of node 8
            new List<int>() {0},         // children of node 9
            new List<int>() {4, 6, 10},  // children of node 10
            new List<int>() {},          // children of node 11
            new List<int>() {7},         // children of node 12
            new List<int>() {2, 9},      // children of node 13
        };

        FindStronglyConnectedComponents();
    }
}
