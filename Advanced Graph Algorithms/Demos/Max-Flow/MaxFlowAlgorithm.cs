using System;

class MaxFlowAlgorithm
{
    // Ford-Fulkerson algorithm for finding max flow in graph
    // with DFS for finding the augmenting paths.
    // Running time: O(m * f) where m = number of edges, f = max flow value
    // Learn more at: https://en.wikipedia.org/wiki/Ford%E2%80%93Fulkerson_algorithm

    const int n = 12; // Number of graph nodes
    const int sourceNode = 0; // Source node
    const int destinationNode = 9; // Destination node

    static int[,] graph = new int[n, n]
    {
        //0   1   2   3   4   5   6   7   8   9  10  11
        { 0,  0,  0,  0,  0,  0, 30,  0, 22,  0,  0,  0 }, // 0
        { 0,  0,  0,  0, 20,  0,  0, 26,  0, 25,  0,  6 }, // 1
        { 0,  0,  0,  0,  0,  0,  0, 15, 14,  0,  0,  9 }, // 2
        { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  7,  0 }, // 3
        { 0, 20,  0,  0,  0,  5, 17,  0,  0,  0,  0, 11 }, // 4
        { 0,  0,  0,  0,  5,  0,  6,  0,  3,  0,  0, 33 }, // 5
        {30,  0,  0,  0, 17,  6,  0,  0,  0,  0,  0,  0 }, // 6
        { 0, 26, 15,  0,  0,  0,  0,  0,  0, 23,  0, 20 }, // 7
        {22,  0, 14,  0,  0,  3,  0,  0,  0,  0,  0,  0 }, // 8
        { 0, 25,  0,  0,  0,  0,  0, 23,  0,  0,  0,  0 }, // 9
        { 0,  0,  0,  7,  0,  0,  0,  0,  0,  0,  0,  0 }, // 10
        { 0,  6,  9,  0, 11, 33,  0, 20,  0,  0,  0,  0 }, // 11
    };

    static int[,] flow = new int[n, n];
    static int[] path = new int[n];
    static bool[] visited = new bool[n];
    static bool pathFound = false;

    static void UpdateFlow(int pathLength)
    {
        // Calculate the flow increment size (minimum of all path edges)
        int flowIncrementSize = int.MaxValue;
        Console.Write("Augmenting path found: ");
        for (int i = 0; i < pathLength; i++)
        {
            int node = path[i];
            int nextNode = path[i + 1];
            Console.Write("{0} ", node);
            if (graph[node, nextNode] < flowIncrementSize)
            {
                flowIncrementSize = graph[node, nextNode];
            }
        }
        Console.Write(path[pathLength]);
        Console.WriteLine(" (flow increment = {0})", flowIncrementSize);

        // Increment the max-flow 
        for (int i = 0; i < pathLength; i++)
        {
            int node = path[i];
            int nextNode = path[i + 1];
            flow[node, nextNode] += flowIncrementSize;
            flow[nextNode, node] -= flowIncrementSize;
            graph[node, nextNode] -= flowIncrementSize;
            graph[nextNode, node] += flowIncrementSize;
        }
    }

    static void FindAugmentingPathDFS(int node, int level)
    {
        if (pathFound)
        {
            return;
        }

        if (node == destinationNode)
        {
            pathFound = true;
            UpdateFlow(level - 1);
        }
        else
        {
            for (int nextNode = 0; nextNode < n; nextNode++)
            {
                if (!visited[nextNode] && graph[node, nextNode] > 0)
                {
                    visited[nextNode] = true;
                    path[level] = nextNode;
                    FindAugmentingPathDFS(nextNode, level + 1);
                    if (pathFound)
                    {
                        return;
                    }
                }
            }
        }
    }

    static void Main()
    {
        // While augmenting path exists, find it and update the flow
        do
        {
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
            }
            pathFound = false;
            visited[sourceNode] = true;
            path[0] = sourceNode;
            FindAugmentingPathDFS(sourceNode, 1);
        } while (pathFound);

        int maxFlowSize = 0;
        for (int i = 0; i < n; i++)
        {
            maxFlowSize += flow[i, destinationNode];
        }
        Console.WriteLine("Max flow found: {0}", maxFlowSize);

        Console.WriteLine("Max flow matrix: ");
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                Console.Write("{0,4}", flow[row, col]);
            }
            Console.WriteLine();
        }
    }
}
