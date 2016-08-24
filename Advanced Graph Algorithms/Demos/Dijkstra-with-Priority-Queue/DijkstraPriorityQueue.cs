using System;
using System.Collections.Generic;
using System.Linq;

public class DijkstraPriorityQueue
{
    // Dijkstra's shortest paths algorithm, implemented
    // with priority queue. Running time: O(M * log M)
    // Learn more at: https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm#Using_a_priority_queue

    public static void DijkstraAlgorithm(
        Dictionary<Node, List<Edge>> graph, Node sourceNode)
    {
        var queue = new PriorityQueue<Node>();

        foreach (var node in graph)
        {
            node.Key.Distance = double.PositiveInfinity;
        }
        sourceNode.Distance = 0.0d;
        queue.Enqueue(sourceNode);

        while (queue.Count != 0)
        {
            var currentNode = queue.Dequeue();

            if (double.IsPositiveInfinity(currentNode.Distance))
            {
                // All nodes processed --> algorithm finished
                break;
            }

            foreach (var childEdge in graph[currentNode])
            {
                var newDistance = currentNode.Distance + childEdge.Distance;
                if (newDistance < childEdge.Node.Distance)
                {
                    childEdge.Node.Distance = newDistance;
                    childEdge.Node.PreviousNode = currentNode;
                    queue.Enqueue(childEdge.Node);
                }
            }
        }
    }

    public static void Main()
    {
        int n = 12;
        var nodes = new Node[n];
        for (int i = 0; i < n; i++)
        {
            nodes[i] = new Node(i);
        }

        var graph = new Dictionary<Node, List<Edge>>
        {
            { nodes[0], new List<Edge> {
                new Edge(nodes[6], 10),
                new Edge(nodes[8], 12),
            } },
            { nodes[1], new List<Edge> {
                new Edge(nodes[4], 20),
                new Edge(nodes[7], 26),
                new Edge(nodes[9], 5),
                new Edge(nodes[11], 6),
            } },
            { nodes[2], new List<Edge> {
                new Edge(nodes[7], 15),
                new Edge(nodes[8], 14),
                new Edge(nodes[11], 9),
            } },
            { nodes[3], new List<Edge> {
                new Edge(nodes[10], 7),
            } },
            { nodes[4], new List<Edge> {
                new Edge(nodes[1], 20),
                new Edge(nodes[5], 5),
                new Edge(nodes[6], 17),
                new Edge(nodes[11], 11),
            } },
            { nodes[5], new List<Edge> {
                new Edge(nodes[4], 5),
                new Edge(nodes[6], 6),
                new Edge(nodes[8], 3),
                new Edge(nodes[11], 33),
            } },
            { nodes[6], new List<Edge> {
                new Edge(nodes[0], 10),
                new Edge(nodes[4], 17),
                new Edge(nodes[5], 6),
            } },
            { nodes[7], new List<Edge> {
                new Edge(nodes[1], 26),
                new Edge(nodes[2], 15),
                new Edge(nodes[9], 3),
                new Edge(nodes[11], 20),
            } },
            { nodes[8], new List<Edge> {
                new Edge(nodes[0], 12),
                new Edge(nodes[2], 14),
                new Edge(nodes[5], 3),
            } },
            { nodes[9], new List<Edge> {
                new Edge(nodes[1], 5),
                new Edge(nodes[7], 3),
            } },
            { nodes[10], new List<Edge> {
                new Edge(nodes[3], 7),
            } },
            { nodes[11], new List<Edge> {
                new Edge(nodes[1], 6),
                new Edge(nodes[2], 9),
                new Edge(nodes[4], 11),
                new Edge(nodes[5], 33),
                new Edge(nodes[7], 20),
            } }
        };

        Node source = nodes[0];

        DijkstraAlgorithm(graph, source);

        for (int i = 0; i < nodes.Length; i++)
        {
            Console.Write("Shortest distance [{0} -> {1}]: ", source.Id, i);
            double distance = nodes[i].Distance;
            if (double.IsPositiveInfinity(distance))
            {
                Console.WriteLine("no path");
            }
            else
            {
                var path = FindPath(graph, nodes[i]);
                var pathStr = string.Join("->", path.Select(node => node.Id));
                Console.WriteLine("{0} (Path = {1})", distance, pathStr);
            }
        }
    }

    static List<Node> FindPath(
        Dictionary<Node, List<Edge>> graph, Node node)
    {
        var path = new List<Node>();
        while (node != null)
        {
            path.Add(node);
            node = node.PreviousNode;
        }
        path.Reverse();
        return path;
    }
}
