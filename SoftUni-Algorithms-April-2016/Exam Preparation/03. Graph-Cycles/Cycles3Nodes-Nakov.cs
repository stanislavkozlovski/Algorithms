using System;
using System.Collections.Generic;
using System.Linq;

class Cycles3Nodes
{
    static int n;
    static List<int>[] graph;
    static bool[,] hasEdge;

    static void Main()
    {
        ReadGraph();
        FindCycles();
    }

    static void ReadGraph()
    {
        n = int.Parse(Console.ReadLine());
        graph = new List<int>[n];
        hasEdge = new bool[n, n];
        for (int i = 0; i < n; i++)
        {
            int[] nums = Console.ReadLine().Split(' ')
                .Where(p => p != "->").Select(int.Parse).ToArray();
            int sourceNode = nums[0];
            graph[sourceNode] = new List<int>();
            for (int childIndex = 1; childIndex < nums.Length; childIndex++)
            {
                int destNode = nums[childIndex];
                graph[sourceNode].Add(destNode);
                hasEdge[sourceNode, destNode] = true;
            }
            graph[sourceNode] = graph[sourceNode].Distinct()
                .OrderBy(p => p).ToList();
        }
    }

    static void FindCycles()
    {
        int cyclesCount = 0;
        for (int u = 0; u < n; u++)
        {            
            foreach (var v in graph[u])
            {
                if (v > u)
                {
                    foreach (var w in graph[v])
                    {
                        if (w > u && v != w)
                        {
                            if (hasEdge[w, u])
                            {
                                Console.WriteLine(
                                    "{" + $"{u} -> {v} -> {w}" +"}");
                                cyclesCount++;
                            }
                        }
                    }
                }
            }
        }

        if (cyclesCount == 0)
        {
            Console.WriteLine("No cycles found");
        }
    }
}
