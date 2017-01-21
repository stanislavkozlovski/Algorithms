using System;
using System.Text.RegularExpressions;

/* Inputs:
#1

Nodes: 4
Edges: 5
0 2 10
0 1 12
1 2 10
1 3 3
2 3 6
END

#2

Nodes: 10
Edges: 17
0 6 10
0 8 12
1 4 20
1 5 6
1 7 26
1 9 5
2 5 9
2 7 15
2 8 14
3 4 5
3 5 33
3 6 6
3 8 3
4 5 11
4 6 17
5 7 20
7 9 3
END

*/
class ShortestPaths
{
    // This doesn't use the Floyd-Warshall algorithm, but rather runs Dijkstra on every node and saves nodes we've calculated
    // This implementation is faster than the Floyd-Warshall algorithm ONLY WHEN the edges are equal or less than the nodes.
    // I use it because I think that it's an interesting solution
    static int[,] graph;
    static int[,] shortestPaths;
    
    static void Main()
    {
        int nodes = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        graph = new int[nodes, nodes];
        shortestPaths = new int[nodes, nodes];
        int edges = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);

        ReadInput(); // you need to type "END" for it to stop reading

        for (int row = 0; row < graph.GetLength(0); row++)
        {
            for (int col = 0; col < graph.GetLength(1); col++)
            {
                if(shortestPaths[row,col] == 0) // using Dijkstra for each connection we haven't made
                {
                    int[] shortestPathsArray = Dijkstra(row, col);
                    // using dynamic programming we add all the shortest paths that were calculated
                    // because if we know the shortest path to X, we know all the shortest paths for the nodes leading up to X
                    for (int i = 0; i < shortestPathsArray.Length; i++)
                    {
                        if(shortestPathsArray[i] != 0)
                        {
                            shortestPaths[row, i] = shortestPathsArray[i];
                            shortestPaths[i, row] = shortestPathsArray[i];
                        }
                    }
                }

            }
        }

        PrintResults();
    }
    private static void PrintResults()
    {
        Console.WriteLine("Shortest path matrix:");
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < graph.GetLength(0); j++)
            {
                if (i == 0)
                    Console.Write(" {0} ", j);
                else
                    Console.Write("---");
            }
            Console.WriteLine();
        }

        for (int row = 0; row < shortestPaths.GetLength(0); row++)
        {
            for (int col = 0; col < shortestPaths.GetLength(1); col++)
            {
                Console.Write(" {0}", shortestPaths[row, col]);
            }
            Console.WriteLine();
        }
    }
    private static int[] Dijkstra(int sourceNode, int destinationNode)
    {
        int n = graph.GetLength(0);
        bool[] used = new bool[n];

        // Initialize the distances
        int[] distance = new int[n];
        for (int i = 0; i < n; i++)
        {
            distance[i] = int.MaxValue;
        }
        distance[sourceNode] = 0; // set it to 0 so we can start the loop

        while (true)
        {

            int minDistance = int.MaxValue;
            int minNode = 0;
            // Find the nearest unused node
            for (int i = 0; i < n; i++)
            {
                if(!used[i] && distance[i] < minDistance)
                {
                    minDistance = distance[i];
                    minNode = i;
                }
            }

            if (minDistance == int.MaxValue) // no minimum distance found, everything's been traversed and we stop the loop
                break;
            used[minNode] = true;

            // Calculate the distance [0...n-1] using minNode
            for (int i = 0; i < n; i++)
            {
                if(graph[minNode, i] > 0) // checks if there's a connection between the nodes
                {
                    if(distance[i] > distance[minNode] + graph[minNode, i]) // checks if we've found a shorter distance
                    {
                        distance[i] = minDistance + graph[minNode, i];
                    }
                }
            }
        }

        if (distance[destinationNode] == int.MaxValue) // if so, not path was found
            return null;
        else
            return distance;
    }
    private static void ReadInput()
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


