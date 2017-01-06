using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*  There isn't consistency with the way the "Path: 0 - 6" is shown. Here is the input, copy and paste it from here.
   
    #1 
Nodes: 7
Path: 0 – 6
Edges: 10
0 3 85
0 4 88
3 1 95
3 5 98
4 5 99
4 2 14
5 1 5
5 6 90
1 6 100
2 6 95
END

    #2
Nodes: 4
Path: 0 – 1
Edges: 4
0 1 94
0 2 97
2 3 99
1 3 98
END

*/

class ReliablePath
{
    static int[,] graph;
    static void Main()
    {
        int nodes = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        graph = new int[nodes, nodes];
        string pathInput = Console.ReadLine();
        int sourceNode = int.Parse(Regex.Match(pathInput, @"Path: (\d+)\s").Groups[1].Value);
        int targetNode = int.Parse(Regex.Match(pathInput, @"Path: \d+\s-\s(\d+)").Groups[1].Value);
        int edges = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);

        ReadInput(); // you need to type "END" for it to stop reading

        List<int> path = DijkstraAlgorithm(sourceNode, targetNode);
        Console.WriteLine(string.Join(" -> ", path));
    }
    public static List<int> DijkstraAlgorithm(int sourceNode, int destinationNode)
    {
        int n = graph.GetLength(0);
        var used = new bool[n]; // tracks where we've been
        var prev = new int[n];

        //Initialzie the distance
        double[] distance = new double[n];
        for (int i = 0; i < n; i++)
        {
            distance[i] = double.MinValue;
        }
        distance[sourceNode] = 0;

        while (true)
        {
            //Find the nearest unused node fom source
            double maxDistance = double.MinValue;
            int maxNode = 0;
            for (int i = 0; i < n; i++)
            {
                if (!used[i] && distance[i] > maxDistance)
                {
                    maxDistance = distance[i];
                    maxNode = i;
                }
            }

            if (maxDistance == double.MinValue) // no minimum distance found, everything's been traversed, we're done
                break;
            used[maxNode] = true;

            // Calculate the distance[0..n-1] through MinNode
            for (int i = 0; i < n; i++)
            {
                if (graph[maxNode, i] > 0)
                {
                    if (distance[i] < ((distance[maxNode]/100.00) * (graph[maxNode, i]/100.00))*100)
                    {
                        if (maxDistance <= 0)
                            maxDistance = 1;

                        distance[i] = (maxDistance/100.00 * graph[maxNode, i]/100.00)*100;
                        prev[i] = maxNode;
                    }
                }
            }
        }

        if (distance[destinationNode] == double.MinValue) // if so, no path has been found
            return null;


        //Reconstruct the most reliable path from sourceNode to destinationNode
        var path = new List<int>();
        int currentNode = destinationNode;

        while (true)
        {
            path.Add(currentNode);

            if (currentNode == sourceNode)
                break;

            currentNode = prev[currentNode];
        }

        path.Reverse();

        Console.WriteLine("Most reliable path reliability: {0:F2}%", distance[destinationNode]*100);
        return path;
    }
    public static void ReadInput()
    {
        while (true)
        {
            // Read the input and fill the arrays
            string input = Console.ReadLine();

            if (input == "END")
                break;
            Match match = Regex.Match(input, @"(\d+)\s+(\d+)\s+(\d+)");
            int startNode = int.Parse(match.Groups[1].Value);
            int endNode = int.Parse(match.Groups[2].Value);
            int value = int.Parse(match.Groups[3].Value);


            graph[startNode, endNode] = value;
            graph[endNode, startNode] = value;
        }
    }
}