using System.IO;

namespace _04Robery
{
    using System;
    using System.Collections.Generic;

    public class Edge
    {
        public Edge(int parent, int child, int distance)
        {
            this.Parent = parent;
            this.Child = child;
            this.Distance = distance;
        }

        public int Parent { get; set; }

        public int Child { get; set; }

        public int Distance { get; set; }
    }

    public class Node : IComparable<Node>
    {
        public Node(int id, bool isBlack)
        {
            this.Id = id;
            this.StartedBlack = isBlack;
            this.TurnsFromStart = 0;
            this.EnergyFromStart = int.MinValue;
            this.Edges = new List<Edge>();
        }

        public int Id { get; set; }

        public bool StartedBlack { get; set; }

        public int TurnsFromStart { get; set; }

        public int EnergyFromStart { get; set; }

        public List<Edge> Edges { get; set; }

        public int CompareTo(Node other)
        {
            return this.EnergyFromStart.CompareTo(other.EnergyFromStart);
        }
    }

    public enum Comparator
    {
        Min,
        Max
    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private Dictionary<T, int> searchCollection;
        private List<T> heap;
        private Comparer<T> comparer;

        public PriorityQueue(Comparator comparator)
        {
            this.SetComparator(comparator);
            this.heap = new List<T>();
            this.searchCollection = new Dictionary<T, int>();
        }

        public PriorityQueue()
            : this(Comparator.Min)
        {
        }

        public int Count
        {
            get
            {
                return this.heap.Count;
            }
        }

        public T ExtractTop()
        {
            var min = this.heap[0];
            var last = this.heap[this.heap.Count - 1];
            this.searchCollection[last] = 0;
            this.heap[0] = last;
            this.heap.RemoveAt(this.heap.Count - 1);
            if (this.heap.Count > 0)
            {
                this.HeapifyDown(0);
            }

            this.searchCollection.Remove(min);

            return min;
        }

        public T PeekMin()
        {
            return this.heap[0];
        }

        public void Enqueue(T element)
        {
            this.searchCollection.Add(element, this.heap.Count);
            this.heap.Add(element);
            this.HeapifyUp(this.heap.Count - 1);
        }

        private void HeapifyDown(int i)
        {
            var left = (2 * i) + 1;
            var right = (2 * i) + 2;
            var smallest = i;

            if (left < this.heap.Count && this.comparer.Compare(this.heap[left], this.heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < this.heap.Count && this.comparer.Compare(this.heap[right], this.heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                T old = this.heap[i];
                this.searchCollection[old] = smallest;
                this.searchCollection[this.heap[smallest]] = i;
                this.heap[i] = this.heap[smallest];
                this.heap[smallest] = old;
                this.HeapifyDown(smallest);
            }
        }

        private void HeapifyUp(int i)
        {
            var parent = (i - 1) / 2;
            while (i > 0 && this.comparer.Compare(this.heap[i], this.heap[parent]) < 0)
            {
                T old = this.heap[i];
                this.searchCollection[old] = parent;
                this.searchCollection[this.heap[parent]] = i;
                this.heap[i] = this.heap[parent];
                this.heap[parent] = old;

                i = parent;
                parent = (i - 1) / 2;
            }
        }

        private void SetComparator(Comparator comparator)
        {
            if (comparator == Comparator.Min)
            {
                this.comparer = Comparer<T>.Default;
            }
            else
            {
                this.comparer = Comparer<T>.Create((a, b) => b.CompareTo(a));
            }
        }

        public void DecreaseKey(T element)
        {
            int index = this.searchCollection[element];
            this.HeapifyUp(index);
        }
    }

    public class Robbery
    {
        public static void Main(string[] args)
        {
            byte[] inputBuffer = new byte[16384];
            Stream inputStream = Console.OpenStandardInput(inputBuffer.Length);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));

            Dictionary<int, Node> graph = new Dictionary<int, Node>();
            string[] parameters = Console.ReadLine().Split();
            foreach (var node in parameters)
            {
                bool isBlack = node.EndsWith("b");
                var number = int.Parse(node.Substring(0, node.Length - 1));
                graph.Add(number, new Node(number, isBlack));
            }

            int startingEnergy = int.Parse(Console.ReadLine());
            int waitingCost = int.Parse(Console.ReadLine());
            int startingNode = int.Parse(Console.ReadLine());
            int endingNode = int.Parse(Console.ReadLine());

            int edgesCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < edgesCount; i++)
            {
                parameters = Console.ReadLine().Split();
                var parent = int.Parse(parameters[0]);
                var child = int.Parse(parameters[1]);
                var distance = int.Parse(parameters[2]);
                graph[parent].Edges.Add(new Edge(parent, child, distance));
            }

            Dijkstra(graph, startingNode, endingNode, waitingCost, startingEnergy);
        }

        private static void Dijkstra(Dictionary<int, Node> graph, int startNode, int endNode, int waitCost, int startingEnergy)
        {
            bool[] visited = new bool[graph.Count];
            int?[]prev = new int?[graph.Count];
            graph[startNode].EnergyFromStart = startingEnergy;
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>(Comparator.Max);
            priorityQueue.Enqueue(graph[startNode]);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.ExtractTop();

                if (current.Id == endNode)
                {
                    break;
                }

                foreach (var edge in current.Edges)
                {
                    if (!visited[edge.Child])
                    {
                        priorityQueue.Enqueue(graph[edge.Child]);
                        visited[edge.Child] = true;
                    }

                    int energy = current.EnergyFromStart - edge.Distance;
                    var turns = 1;

                    if ((current.TurnsFromStart % 2 == 0 && graph[edge.Child].StartedBlack) || (current.TurnsFromStart % 2 == 1 && !graph[edge.Child].StartedBlack))
                    {
                        energy -= waitCost;
                        turns = 2;
                    }

                    if (energy > graph[edge.Child].EnergyFromStart)
                    {
                        graph[edge.Child].EnergyFromStart = energy;
                        graph[edge.Child].TurnsFromStart = current.TurnsFromStart + turns;
                        priorityQueue.DecreaseKey(graph[edge.Child]);
                        prev[edge.Child] = current.Id;
                    }
                }
            }
            if (graph[endNode].EnergyFromStart < 0)
            {
                Console.WriteLine("Busted - need {0} more energy", Math.Abs(graph[endNode].EnergyFromStart));
            }
            else
            {
                Console.WriteLine(graph[endNode].EnergyFromStart);
            }
        }
    }
}
