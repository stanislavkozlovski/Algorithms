using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DistanceBetweenVertices
{
    static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>()
    {
        { 1, new List<int>() { 4} },
        {2, new List<int>() {4 } },
        {3,  new List<int>() {4,5 } },
        {4, new List<int>() {6 } },
        {5, new List<int>() {3,7,8 } },
        {6, new List<int>() { } },
        {7, new List<int>() {8 } },
        {8, new List<int>() { } }
    };
    static Dictionary<int, List<int>> toFind = new Dictionary<int, List<int>>()
    {
        {1, new List<int>() {6, 5} },
        {5, new List<int>() {6,8 } }
    };
   

    static void Main()
    {
        foreach (var node in toFind.Keys)
        {
            foreach (var searchedNode in toFind[node])
            {
                Console.WriteLine("{{{0}, {1}}} -> {2}", node, searchedNode, BFS(node, searchedNode));
            }
        }
    }
    public static int BFS(int node, int searchedNode)
    {
        int[] dist = new int[(graph.Keys.Count + 1) * 2];
        int[] prev = new int[(graph.Keys.Count + 1) * 2];
        var nodes = new Queue<int>();
        var visited = new bool[graph.Keys.Count + 1];
        bool found = false;
        var parents = new Dictionary<int, int>(); // keep a dictionary of type <Child, Parent> to traverse back from

        // Enqueue the start node to the queue
        visited[node] = true;
        nodes.Enqueue(node);

        // BFS and fills the dictionary with a link leading to the searched node(if it exists)
        while(nodes.Count != 0)
        {
            int currentNode = nodes.Dequeue();

            foreach (var childNode in graph[currentNode])
            {
                if (!parents.ContainsKey(childNode)) // always add to the dictionary
                {
                    parents.Add(childNode, currentNode);
                }
                if(childNode == searchedNode) // if we've found it there's no reason to continue traversing
                {
                    found = true;
                    break;
                }
                if (!visited[childNode]) // standard BFS algorithm
                {
                    nodes.Enqueue(childNode);
                    visited[childNode] = true;
                }
            }
        }

        if (!found)
            return -1;
        else
        {
            // calculates the distance by traversing back from the searchedNode to its parent to its parent and so on
            int currentNode = searchedNode;
            int distance = 0;

            while (currentNode != node) // iterate back to the node we started from
            {
                distance++;
                currentNode = parents[currentNode]; // returns the parent of currentNode, iterates until we reach the node we started from
            }

            return distance;
        }
    }
}

