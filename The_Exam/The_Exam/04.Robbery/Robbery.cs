using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
class Robbery
{
    static bool[] nodesBlackWhite;
    static bool[] nodesBlackWhiteInverted;
    static int waitingCost;
    static Dictionary<int, Dictionary<int, int>> graph = new Dictionary<int, Dictionary<int, int>>();
    static bool inverted = true;
    static void Main()
    {
        string[] nodes = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        int startingEnergy = int.Parse(Console.ReadLine());
        waitingCost = int.Parse(Console.ReadLine());
        int startNode = int.Parse(Console.ReadLine());
        int endNode = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());
        nodesBlackWhite = new bool[nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
        {
            nodesBlackWhite[i] = nodes[i].Contains('w');
        }
        nodesBlackWhiteInverted = InvertArray();
        for (int i = 0; i < n; i++)
        {
            int[] values = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int nodeA = values[0];
            int nodeB = values[1];
            int value = values[2];

            if (!graph.ContainsKey(nodeA))
                graph[nodeA] = new Dictionary<int, int>();

            graph[nodeA][nodeB] = value;
        }
        DijkstraProsta(startNode, endNode, startingEnergy);
    }
    private static void DijkstraProsta(int startNode, int endNode, int startingEnergy)
    {
        int n = nodesBlackWhite.Length;
        var used = new bool[n];
        var prev = new int?[n];
        var invertedArray = nodesBlackWhite;
        Dictionary<int, bool[]> invertedDic = new Dictionary<int, bool[]>();
        // Initialize the distance
        int[] distance = new int[n];
        for (int i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }
        distance[startNode] = 0;

        while (true)
        {
            // Find the nearest unused node from source
            int minDistance = int.MaxValue;
            int minNode = 0;
            for (int i = 0; i < n; i++)
            {
                if (!used[i] && distance[i] < minDistance)
                {
                    minDistance = distance[i];
                    minNode = i;
                }
            }

            if (minDistance == int.MaxValue) // no minimum distance found, everything's been traversed, we're done
                break;
            used[minNode] = true;
            // Calculate the distance [0..n-1] through minNode
            for (int i = 0; i < n; i++)
            {
                bool haveStepped = false;
                if (graph.ContainsKey(minNode) && graph[minNode].ContainsKey(i))
                {
                    int newDistance = distance[minNode] + graph[minNode][i];
                    if (invertedDic.ContainsKey(minNode))
                    {
                        if (!invertedDic[minNode][i])
                            newDistance += waitingCost;
                    }
                    else if (!invertedArray[i])
                        newDistance += waitingCost;

                    if (distance[i] > newDistance)
                    {
                        distance[i] = newDistance;
                        prev[i] = minNode;
                        haveStepped = true;
                        invertedDic[i] = Invert(!inverted);
                    }
                }
            }

            invertedArray = Invert();
            inverted = !inverted;
        }
        //Reconstruct the shortest path from sourceNode to destinationNode
        var path = new List<int>();
        int? currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode.Value);
            currentNode = prev[currentNode.Value];
        }
        path.Reverse();

        int energyLeft = startingEnergy - distance[endNode];
        if (energyLeft >= 0)
            Console.WriteLine(energyLeft);
        else
            Console.WriteLine("Busted - need {0} more energy", Math.Abs(energyLeft));
    }
    static bool[] Invert()
    {
        if (inverted)
            return nodesBlackWhiteInverted;
        else
            return nodesBlackWhite;
    }
    static bool[] Invert(bool invertedd)
    {
        if (invertedd)
            return nodesBlackWhiteInverted;
        else
            return nodesBlackWhite;
    }
    static bool[] InvertArray()
    {
        bool[] inverted = new bool[nodesBlackWhite.Length];

        for (int i = 0; i < nodesBlackWhite.Length; i++)
        {
            inverted[i] = !nodesBlackWhite[i];
        }

        return inverted;
    }
    static bool[] InvertArray(bool[] nodesBlackWhite)
    {
        bool[] inverted = new bool[nodesBlackWhite.Length];

        for (int i = 0; i < nodesBlackWhite.Length; i++)
        {
            inverted[i] = !nodesBlackWhite[i];
        }

        return inverted;
    }
}

/*
0b 1b 2w 3b 4b
70
10
0
4
7
0 1 25
0 2 30
1 3 12
1 4 15
2 1 3
2 3 15
3 4 18

0w 1b 2b 3b
0
10
0
2
4
0 1 20
0 3 10
1 2 50
3 2 50



0b 1w 2w 3w 4w 5b 6b 7w 8b 9w 10b 11b 12w 13w
99
5
1
11
18
1 0 5
1 2 17
1 3 22
3 4 17
3 5 4
5 6 3
5 7 12
6 4 7
6 8 31
7 2 5
7 10 117
8 9 44
9 10 2
10 6 9
10 12 29
11 9 1
12 13 16
13 11 8
*/
