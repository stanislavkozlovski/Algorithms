using System;
using System.Collections.Generic;
using System.Linq;

class Sticks
{
    static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
    static void Main()
    {
        BuildGraph();
        Console.WriteLine(string.Join(" ", TopSort()));
    }

    private static List<int> TopSort()
    {
        //Find the predecessors count
        var predecessorsCount = new Dictionary<int, int>();

        foreach (var node in graph)
        {
            if (!predecessorsCount.ContainsKey(node.Key))
            {
                predecessorsCount[node.Key] = 0;
            }
            foreach (var childNode in node.Value)
            {
                if (!predecessorsCount.ContainsKey(childNode))
                {
                    predecessorsCount[childNode] = 0;
                }

                predecessorsCount[childNode]++;
            }
        }

        // Sort through the graph and remove the ones that don't have a parent one by one
        var removedNodes = new List<int>();
        while (true)
        {
            int nodeToRemove = -1;
            foreach (var item in graph.Keys)
            {
                if (predecessorsCount[item] == 0 && item > nodeToRemove)
                    nodeToRemove = item;
            }

            if (nodeToRemove == -1)
                break;

            foreach (var child in graph[nodeToRemove])
            {
                predecessorsCount[child]--;
            }
            graph.Remove(nodeToRemove);
            removedNodes.Add(nodeToRemove);
        }

        if (graph.Count != 0)
            Console.WriteLine("Cannot lift all sticks");

        return removedNodes;
    }
    private static void BuildGraph()
    {
        int nodes = int.Parse(Console.ReadLine());
        int edges = int.Parse(Console.ReadLine());

        // Build graph
        for (int i = 0; i < nodes; i++)
        {
            graph[i] = new List<int>();
        }
        for (int i = 0; i < edges; i++)
        {
            int[] edge = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int nodeA = edge[0];
            int nodeB = edge[1];

            graph[nodeA].Add(nodeB); // add the edge
        }
    }
}

