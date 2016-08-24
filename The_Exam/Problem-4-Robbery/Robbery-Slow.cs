namespace Robbery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Edge
    {
        public int Other { get; set; }

        public int Weight { get; set; }
    }

    public class RobberySolution
    {
        static List<Edge>[] graph;
        static bool[] isBlack;
        static int WaitCost;

        static void Main(string[] args)
        {
            var nodes = Console.ReadLine()
                .Split(' ');

            graph = new List<Edge>[nodes.Length];
            isBlack = new bool[nodes.Length];

            for (int i = 0; i < nodes.Length; i++)
            {
                var lastIndex = nodes[i].Length - 1;
                isBlack[i] = nodes[i][lastIndex] == 'b';
                var value = int.Parse(nodes[i].Substring(0, lastIndex));
                graph[value] = new List<Edge>();
            }

            var energy = int.Parse(Console.ReadLine());
            WaitCost = int.Parse(Console.ReadLine());
            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            for (int i = 0; i < edges; i++)
            {
                var edge = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                graph[edge[0]].Add(
                    new Edge() { Other = edge[1], Weight = edge[2] });
            }

            var energyAtEnd = Dijkstra(start, end, energy);
            if (energyAtEnd < 0)
            {
                Console.WriteLine("Busted - need {0} more energy", -energyAtEnd);
            }
            else
            {
                Console.WriteLine(energyAtEnd);
            }
        }

        static int Dijkstra(int start, int end, int initialEnergy)
        {
            var stepVisited = new int[graph.Length];
            var visited = new bool[graph.Length];
            var distanceFromStart = new int[graph.Length];
            var energy = new int[graph.Length];
            for (int i = 0; i < distanceFromStart.Length; i++)
            {
                distanceFromStart[i] = int.MaxValue;
            }

            distanceFromStart[start] = 0;
            stepVisited[start] = 1;
            energy[start] = initialEnergy;

            while (true)
            {
                var current = -1;
                var distance = int.MaxValue;
                for (int i = 0; i < distanceFromStart.Length; i++)
                {
                    if (!visited[i] && distanceFromStart[i] < distance)
                    {
                        current = i;
                        distance = distanceFromStart[i];
                    }
                }

                if (current == -1)
                {
                    break;
                }

                visited[current] = true;

                foreach (var edge in graph[current])
                {
                    var edgeWeight = edge.Weight;
                    var steps = stepVisited[current] + 1;
                    var willBeWhite = isBlack[edge.Other];
                    if (stepVisited[current] % 2 == 0)
                    {
                        willBeWhite = !willBeWhite;
                    }

                    if (willBeWhite)
                    {
                        edgeWeight += WaitCost;
                        steps++;
                    }

                    var newDistance = distanceFromStart[current] + edgeWeight;
                    if (newDistance < distanceFromStart[edge.Other])
                    {
                        distanceFromStart[edge.Other] = newDistance;
                        stepVisited[edge.Other] = steps;
                        energy[edge.Other] = energy[current] - edgeWeight;
                    }
                }
            }

            return energy[end];
        }
    }
}
