using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


class CableNetwork
{
    static List<Edge> graphEdges;
    static HashSet<int> graphNodes;
    static Dictionary<int, List<Edge>> possibleGraph = new Dictionary<int, List<Edge>>();
    static int allowedBudget;
    static void Main()
    {
        allowedBudget = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        int originalBudget = allowedBudget;
        int nodes = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        int edges = int.Parse(Regex.Match(Console.ReadLine(), @"\d+").Value);
        ReadInput(); // you need to type "END" for it to stop reading

        // Runs a modification of Prim's algorithm where we add all the possible edges to a list, sort it and check if we can add it
        Prim(possibleGraph, graphNodes, graphEdges);

        Console.WriteLine("Budget used: {0}", originalBudget - allowedBudget);
    }
    private static void Prim(Dictionary<int, List<Edge>> graph,
       HashSet<int> graphNodes,
       List<Edge> graphEdges)
    {
        // Enqueues all of the possible connections in a binary heap
        var priorityQueue = new BinaryHeap<Edge>();
        foreach (var childEdge in graph)
        {
            foreach (var edgeList in childEdge.Value)
            {
                priorityQueue.Enqueue(edgeList);
            }
        }

        while (priorityQueue.Count > 0)
        {
            // gets the edge with the smallest value
            var smallestEdge = priorityQueue.ExtractMin();

            if (graphNodes.Contains(smallestEdge.StartNode) ^
                graphNodes.Contains(smallestEdge.EndNode)) // checks if you can connect it to the graph
            {
                if (allowedBudget - smallestEdge.Value >= 0)             // connects it to the graph if it doesn't go over the budget
                {
                    allowedBudget -= smallestEdge.Value;
                    Console.WriteLine(smallestEdge);

                    // Attaches the smallest edge to graph
                    graphEdges.Add(smallestEdge);
                    var nonTreeNode = graphNodes.Contains(smallestEdge.StartNode) ?
                        smallestEdge.EndNode : smallestEdge.StartNode;
                    graphNodes.Add(nonTreeNode);
                }

                else // if we go over the budget, all of the edges left will go over it too, no need to iterate over them so we break the while loop
                    break;
            }
        }
    }

    public static void ReadInput()
    {
        graphNodes = new HashSet<int>();
        graphEdges = new List<Edge>();
        List<Edge> edges = new List<Edge>();
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

            if (input.Contains("connected"))
            {
                graphNodes.Add(startNode);
                graphNodes.Add(endNode);
            }
            else
            {
                edges.Add(new Edge(startNode, endNode, value));
            }
        }

       // Creates a graph (using the List of Edges) represented through a dictionary
        foreach (var edge in edges)
        {
            if (!possibleGraph.ContainsKey(edge.StartNode))
            {
                possibleGraph.Add(edge.StartNode, new List<Edge>());
            }
            possibleGraph[edge.StartNode].Add(edge);
            if (!possibleGraph.ContainsKey(edge.EndNode))
            {
                possibleGraph.Add(edge.EndNode, new List<Edge>());
            }
            possibleGraph[edge.EndNode].Add(edge);
        }
    }
}

class Edge : IComparable<Edge>
{
    public int Value { get; set; }
    public int EndNode { get; set; }
    public int StartNode { get; set; }
    public int CompareTo(Edge other)
    {
        int weightCompared = this.Value.CompareTo(other.Value);
        return weightCompared;
    }
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
public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public BinaryHeap(T[] elements)
    {
        this.heap = new List<T>(elements);
        for (int i = this.heap.Count / 2; i >= 0; i--)
        {
            HeapifyDown(i);
        }
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public T ExtractMin()
    {
        var min = this.heap[0];
        this.heap[0] = this.heap[heap.Count - 1];
        this.heap.RemoveAt(this.heap.Count - 1);
        if (this.heap.Count > 0)
        {
            HeapifyDown(0);
        }
        return min;
    }

    public T PeekMin()
    {
        var min = this.heap[0];
        return min;
    }

    public void Enqueue(T node)
    {
        this.heap.Add(node);
        HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyDown(int i)
    {
        var left = 2 * i + 1;
        var right = 2 * i + 2;
        var smallest = i;
        if (left < this.heap.Count &&
            this.heap[left].CompareTo(this.heap[smallest]) < 0)
        {
            smallest = left;
        }
        if (right < this.heap.Count &&
            this.heap[right].CompareTo(this.heap[smallest]) < 0)
        {
            smallest = right;
        }
        if (smallest != i)
        {
            T old = this.heap[i];
            this.heap[i] = this.heap[smallest];
            this.heap[smallest] = old;
            HeapifyDown(smallest);
        }
    }

    private void HeapifyUp(int i)
    {
        var parent = (i - 1) / 2;
        while (i > 0 && this.heap[i].CompareTo(this.heap[parent]) < 0)
        {
            T old = this.heap[i];
            this.heap[i] = this.heap[parent];
            this.heap[parent] = old;
            i = parent;
            parent = (i - 1) / 2;
        }
    }
}

