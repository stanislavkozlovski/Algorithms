namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode, Node destinationNode)
        {
            int?[] previous = new int?[graph.Count];
            bool[] visited = new bool[graph.Count];
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();

            foreach (var pair in graph)
            {
                pair.Key.DistanceFromStart = double.PositiveInfinity;
            }
            sourceNode.DistanceFromStart = 0;
            priorityQueue.Enqueue(sourceNode);

            while(priorityQueue.Count > 0)
            {
                Node currentNode = priorityQueue.ExtractMin();

                if (destinationNode == currentNode) // we've reached our destination
                    break;

                foreach (var edge in graph[currentNode])
                {
                    if (!visited[edge.Key.Id])
                    {
                        visited[edge.Key.Id] = true;
                        priorityQueue.Enqueue(edge.Key);
                    }

                    double distance = edge.Key.DistanceFromStart + currentNode.DistanceFromStart;

                    if(distance < edge.Key.DistanceFromStart) // if we've found a shorter way
                    {
                        edge.Key.DistanceFromStart = distance;
                        previous[edge.Key.Id] = currentNode.Id;
                        priorityQueue.DecreaseKey(edge.Key);
                    }
                }
            }

            if (double.IsInfinity(destinationNode.DistanceFromStart))
                return null; // we haven't found a way

            List<int> path = new List<int>();
            int? current = destinationNode.Id;
            while(current!= null)
            {
                path.Add(current.Value);
                current = previous[current.Value];
            }
            path.Reverse();
            return path;
        }
    }
}
