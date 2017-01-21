using System;
using System.Collections.Generic;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;
    private HashSet<string> visitedNodes;
    private LinkedList<string> sortedNodes;
    private HashSet<string> currentVisitedNodes;
    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.visitedNodes = new HashSet<string>();
        this.sortedNodes = new LinkedList<string>();
        this.currentVisitedNodes = new HashSet<string>();
        this.graph = graph;
    }
    // DFS Algorithm
    public ICollection<string> TopSort()
    {
        foreach (var node in this.graph.Keys)
        {
            TopSortDFS(node);
        }
        return sortedNodes;
    }
    public void TopSortDFS(string node)
    {
        if (currentVisitedNodes.Contains(node))
            throw new InvalidOperationException("THERE IS A CYCLE :O");

        if (!visitedNodes.Contains(node))
        {
            visitedNodes.Add(node);
            currentVisitedNodes.Add(node);

            if(graph.ContainsKey(node)) // check if it has keys
            {
                foreach (var child in graph[node])
                {
                    TopSortDFS(child);
                }
            }

            sortedNodes.AddFirst(node);
            currentVisitedNodes.Remove(node);
        }
    }

    // Source Removal algorithm

    //public ICollection<string> TopSort()
    //{
    //    //Find the predecessors count
    //    var predecessorsCount = new Dictionary<string, int>();

    //    foreach (var node in graph)
    //    {
    //        if (!predecessorsCount.ContainsKey(node.Key))
    //        {
    //            predecessorsCount[node.Key] = 0;
    //        }
    //        foreach (var childNode in node.Value)
    //        {
    //            if (!predecessorsCount.ContainsKey(childNode))
    //            {
    //                predecessorsCount[childNode] = 0;
    //            }

    //            predecessorsCount[childNode]++;
    //        }
    //    }



    //    var removedNodes = new List<string>();
    //    while (true)
    //    {
    //        string nodeToRemove = graph.Keys.FirstOrDefault(n => predecessorsCount[n] == 0);

    //        if (nodeToRemove == null)
    //            break;

    //        foreach (var child in graph[nodeToRemove])
    //        {
    //            predecessorsCount[child]--;
    //        }
    //        graph.Remove(nodeToRemove);
    //        removedNodes.Add(nodeToRemove);
    //    }

    //    if (graph.Count != 0)
    //        throw new InvalidOperationException("A cycle detected in the graph");

    //    return removedNodes;
    //}
}
