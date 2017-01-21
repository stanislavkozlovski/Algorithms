using System;
using System.Collections.Generic;
using System.Linq;

class MessageSharingTestGenerator
{
    const int nodesCount = 500;
    const int edgesCount = 10000;
    const int startNodesCount = 499;

    static void Main()
    {
        Random rnd = new Random();

        // Generate nodes
        string[] nodes = Enumerable.Range(1, nodesCount)
            .Select(n => "Person" + n)
            .OrderBy(n => rnd.Next())
            .ToArray();
        Console.WriteLine("People: " + string.Join(", ", nodes));

        // Generate edges
        var edges = new HashSet<string>();
        while (edges.Count < edgesCount)
        {
            string startNode = nodes[rnd.Next(nodesCount)];
            string endNode = nodes[rnd.Next(nodesCount)];
            string edge = startNode + " - " + endNode;
            string backEdge = endNode + " - " + startNode;
            if (! (startNode == endNode || edges.Contains(edge) || edges.Contains(backEdge)))
            {
                edges.Add(edge);
            }
        }
        Console.WriteLine("Connections: " + string.Join(", ", edges));

        // Generate start nodes
        string[] startNodes = nodes.OrderBy(n => rnd.Next())
            .Take(startNodesCount)
            .ToArray();
        Console.WriteLine("Start: " + string.Join(", ", startNodes));
    }
}
