namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithoutQueue
    {
        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);
            var used = new bool[n];
            var prev = new int?[n];

            //Initialzie the distance
            int[] distance = new int[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
            }
            distance[sourceNode] = 0;

            while (true)
            {
                //Find the nearest unused node fom source
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

                // Calculate the distance[0..n-1] through MinNode
                for (int i = 0; i < n; i++)
                {
                    if(graph[minNode,i] > 0)
                    {
                        if(distance[i] > distance[minNode] + graph[minNode, i])
                        {
                            distance[i] = minDistance + graph[minNode, i];
                            prev[i] = minNode;
                        }
                    }
                }
            }

            if (distance[destinationNode] == int.MaxValue) // if so, no path has been found
                return null;

            //Reconstruct the shortest path from sourceNode to destinationNode
            var path = new List<int>();
            int? currentNode = destinationNode;
            while(currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = prev[currentNode.Value];
            }
            path.Reverse();

            return path;
        }
    }
}
