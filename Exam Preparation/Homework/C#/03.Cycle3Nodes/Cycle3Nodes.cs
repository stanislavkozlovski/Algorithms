using System;
using System.Collections.Generic;
using System.Linq;

class Cycle3Nodes
{
    static List<int>[] graph;
    static int n;
    static void Main()
    {
        n = int.Parse(Console.ReadLine());
        graph = new List<int>[n];

        for (int i = 0; i < n; i++)
        {
            ReadInputandFillGraph();
        }

        SearchForGraph();
    }
    static void SearchForGraph()
    {
        HashSet<string> knownCycles = new HashSet<string>();
        bool foundCycle = false;

        /* Here we iterate through all possible connections of three nodes and find the cycles
           To avoid the same cycle starting from a different node like 3-2-1 and 1-2-3 and to sort them
           lexicographically we take the cycle where the smallest node is the parent, parent < firstChild && parent < secondChild
           1-2-3 */

        for (int parent = 0; parent < graph.Length; parent++)
        {
            for (int firstChildIndex = 0; firstChildIndex < graph[parent].Count; firstChildIndex++)
            {
                if(parent < graph[parent][firstChildIndex])
                {
                    int firstChild = graph[parent][firstChildIndex];
                    for (int secondChildIndex = 0; secondChildIndex < graph[firstChild].Count; secondChildIndex++)
                    {
                        if (parent < graph[firstChild][secondChildIndex] && firstChild != graph[firstChild][secondChildIndex]) // check for repeats like 1-2-2
                        {
                            int secondChild = graph[firstChild][secondChildIndex];
                            if (graph[secondChild].Contains(parent)) // If the secondChild is connected to the parent it's a loop
                            {
                                string cycle = string.Format("{{{0} -> {1} -> {2}}}", parent, firstChild, secondChild);
                                if (!knownCycles.Contains(cycle))
                                {
                                    Console.WriteLine(cycle);
                                    knownCycles.Add(cycle);
                                    foundCycle = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        if(!foundCycle)
            Console.WriteLine("No cycles found");
    }
    static void ReadInputandFillGraph()
    {
        int[] input = Console.ReadLine().Split(new char[] { ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int parent = input[0];
        graph[parent] = new List<int>();
        input[0] = int.MaxValue;
        
        Array.Sort(input);
        for (int i = 0; i < input.Length-1; i++)
        {
            graph[parent].Add(input[i]);
        }
    }
}

