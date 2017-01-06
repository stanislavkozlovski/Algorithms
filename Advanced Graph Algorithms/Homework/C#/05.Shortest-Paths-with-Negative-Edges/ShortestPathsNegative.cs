using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/* Input #1
 Nodes: 10
Path: 0 - 9
Edges: 19
0 3 -4
0 6 10
0 8 12
1 9 -50
2 5 12
2 7 -7
3 2 -9
3 5 15
3 6 6
3 8 -3
4 1 20
4 3 -5
5 1 -6
5 4 11
5 7 -20
6 4 17
7 1 26
7 9 3
8 2 15
END

    Input #2

Nodes: 4
Path: 0 - 3
Edges: 5
0 2 10
0 1 12
2 1 -10
1 3 3
3 2 6
END
*/

class ShortestPathsNegative
{
    static List<Edge> edges = new List<Edge>();
    static void Main()
    {
        int nodes = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        string pathInput = Console.ReadLine();
        int sourceNode = int.Parse(Regex.Match(pathInput, @"Path: (\d+)\s").Groups[1].Value);
        int targetNode = int.Parse(Regex.Match(pathInput, @"Path: \d+\s-\s(\d+)").Groups[1].Value);
        int edges = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        double[] distance = new double[nodes];
        int[] prev = new int[nodes];

        ReadInputAndStoreEdges(); // you need to type "END" for it to stop reading

        bool hasCycle = BellmanFord(ref distance, ref prev, sourceNode, nodes);

        PrintResults(hasCycle, distance, prev, sourceNode, targetNode);
    }
    private static bool BellmanFord(ref double[] distance, ref int[] prev, int sourceNode, int nodes)
    {
        bool hasCycle = false;

        for (int i = 0; i < nodes; i++)
        {
            distance[i] = double.PositiveInfinity;
            prev[i] = -1;
        }

        distance[sourceNode] = 0;

        // Relax edges repeatedly
        for (int i = 0; i < nodes; i++)
        {
            foreach (Edge edge in edges)
            {
                if(distance[edge.StartNode] + edge.Value < distance[edge.EndNode]) // checks if we can improve the current way
                {
                    distance[edge.EndNode] = distance[edge.StartNode] + edge.Value; // we set the better distance
                    prev[edge.EndNode] = edge.StartNode; // we set the node we came from
                }
            }
        }

        // Check for cycle
        foreach (var edge in edges)
        {
            if(distance[edge.StartNode] + edge.Value < distance[edge.EndNode]) // if the way can still be improved after relaxing all the edges of every node, then there is a cycle
            {
                hasCycle = true;
            }
        }

        return hasCycle;
    }
    private static void PrintResults(bool hasCycle, double[] distance, int[] prev, int sourceNode, int targetNode)
    {
        if (hasCycle)
        {
            Console.Write("Negative cycle detected: ");

            // Build the path for printing
            List<int> path = new List<int>();
            int tempNode = targetNode;

            path.Add(tempNode);
            while (true)
            {
                path.Add(prev[tempNode]);
                tempNode = prev[tempNode];

                if (path.Contains(prev[tempNode])) // checks if we've went through that node
                    break;
            }
            path.Reverse();
            Console.WriteLine(string.Join(" -> ", path));
        }
        else
        {
            Console.WriteLine("Distance [{0} -> {1}]: {2}", sourceNode, targetNode, distance[targetNode]);

            // Build the path for easy printing
            List<int> path = new List<int>();
            int tempNode = targetNode;
            path.Add(tempNode);
            while (true)
            {
                path.Add(prev[tempNode]);
                if (prev[tempNode] == sourceNode)
                    break;
                tempNode = prev[tempNode];
            }
            path.Reverse();
            Console.WriteLine("Path: {0}", string.Join(" -> ", path));
        }
    }
    private static void ReadInputAndStoreEdges()
    {
        while (true)
        {
            // Read the input and fill the arrays
            string input = Console.ReadLine();

            if (input == "END")
                break;
            Match match = Regex.Match(input, @"(-?\d+)\s(-?\d+)\s(-?\d+)");
            int startNode = int.Parse(match.Groups[1].Value);
            int endNode = int.Parse(match.Groups[2].Value);
            int value = int.Parse(match.Groups[3].Value);


            edges.Add(new Edge(startNode, endNode, value));
        }
    }
}
class Edge
{
    public int Value { get; set; }
    public int EndNode { get; set; }
    public int StartNode { get; set; }
    public Edge(int startNode, int endNode, int value)
    {
        this.StartNode = startNode;
        this.EndNode = endNode;
        this.Value = value;
    }
    public override string ToString()
    {
        return string.Format("({0} {1}) -> {2}",
            this.StartNode, this.EndNode, this.Value);
    }
}

