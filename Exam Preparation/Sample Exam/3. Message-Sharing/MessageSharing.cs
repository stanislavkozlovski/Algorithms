using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class MessageSharing
{
    static string[] graphNodes;
    static Dictionary<string, List<string>> childNodes;
    static string[] startNodes;

    static void Main()
    {
        ReadGraph();
        SolveMessageSharingProblem();
    }

    static void ReadGraph()
    {
        // Read the graph nodes
        graphNodes = Regex.Split(Console.ReadLine(), @"\W+").Skip(1).ToArray();

        // Initialize the childNodes structure to hold the graph edges
        childNodes = new Dictionary<string, List<string>>();
        foreach (var node in graphNodes)
        {
            childNodes[node] = new List<string>();
        }

        // Read the edges and build the adjacency list for each node
        string[] connections = Regex.Split(Console.ReadLine(), @"\W+").Skip(1).ToArray();
        for (int i = 0; i < connections.Length; i+=2)
        {
            string firstNode = connections[i];
            string secondNode = connections[i+1];
            childNodes[firstNode].Add(secondNode);
            childNodes[secondNode].Add(firstNode);
        }

        // Read the start node names and map them to node numbers
        startNodes = Regex.Split(Console.ReadLine(), @"\W+").Skip(1).ToArray();
    }

    static void SolveMessageSharingProblem()
    {
        // Initialize the visitedStep to hold at which step each node is visited
        int visitedCount = 0;
        var visitedStep = new Dictionary<string, int>();

        // Enqueue all startNodes in the BFS queue
        var queue = new Queue<string>();
        foreach (var startNode in startNodes)
        {
            queue.Enqueue(startNode);
            visitedStep[startNode] = 0;
            visitedCount++;
        }

        // Performs BFS traversal, starting from all startNodes simultaneously
        while (queue.Count > 0 && visitedCount < graphNodes.Length)
        {
            string currentNode = queue.Dequeue();
            foreach (var childNode in childNodes[currentNode])
            {
                if (!visitedStep.ContainsKey(childNode))
                {
                    queue.Enqueue(childNode);
                    visitedStep[childNode] = visitedStep[currentNode] + 1;
                    visitedCount++;
                }
            }
        }

        // Print the solution
        if (visitedCount == graphNodes.Length)
        {
            // All nodes were visited -> print the number of steps + people at last step
            int maxStep = visitedStep.Values.Max();
            Console.WriteLine("All people reached in {0} steps", maxStep);
            var lastStepNodes = visitedStep.Keys
                .Where(v => visitedStep[v] == maxStep)
                .OrderBy(name => name);
            Console.WriteLine("People at last step: {0}", string.Join(", ", lastStepNodes));
        }
        else
        {
            // Some nodes are unreacheable --> print them alphabetically
            var unreacheableNodes = graphNodes
                .Where(v => ! visitedStep.ContainsKey(v))
                .OrderBy(name => name);
            Console.WriteLine("Cannot reach: {0}", string.Join(", ", unreacheableNodes));
        }
    }
}
