using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CyclesInGraph
{
    static Dictionary<string, List<string>> graph;
    static void Main()
    {
        graph = new Dictionary<string, List<string>>()


        {
            {"A", new List<string>() { "F" } },
            {"F", new List<string>() { "D" } },
            {"D", new List<string>() { "A" } }
        };
        //{
        //    // I think that this is an acycling graph, if you can show me it's not, please leave a message on the homework evaluation
        //    { "K", new List<string>() {"X"} },
        //    { "X", new List<string>() {"Y", "N" } },
        //    { "N", new List<string>() { "J"} },
        //    { "M", new List<string>() { "N", "I"} },
        //    {"A",  new List<string>() {"Z", "Y" } },
        //    {"B",  new List<string>() {"P" } },
        //    {"I",  new List<string>() {"F" } },
        //    {"Y",  new List<string>() {"L" } },
        //    {"F",  new List<string>() {"P" } },
        //    {"Z",  new List<string>() {"E" } },
        //    {"P",  new List<string>() {"E" } },
        //};

        //{
        //    {"K",new List<string>() {"J" } },
        //    {"J",new List<string>() {"N" } },
        //    {"N",new List<string>() {"L", "M" } },
        //    {"M",new List<string>() {"I" } }
        //};




        bool isAsyclic = TopSort();

        Console.Write("Asyclic: ");
        if(isAsyclic)
            Console.WriteLine("Yes");
        else
            Console.WriteLine("No");
    }
    // Source Removal Topological Sorting
    public static bool TopSort()
    {
        bool IsAsyclic = true;
        //Find the predecessors count
        var predecessorsCount = new Dictionary<string, int>();

        foreach (var node in graph)
        {
            if (!predecessorsCount.ContainsKey(node.Key))
            {
                predecessorsCount[node.Key] = 0;
            }
            foreach (var childNode in node.Value)
            {
                if (!predecessorsCount.ContainsKey(childNode))
                {
                    predecessorsCount[childNode] = 0;
                }

                predecessorsCount[childNode]++;
            }
        }

        var removedNodes = new List<string>();
        while (true)
        {
            string nodeToRemove = graph.Keys.FirstOrDefault(n => predecessorsCount[n] == 0);

            if (nodeToRemove == null)
                break;

            foreach (var child in graph[nodeToRemove])
            {
                predecessorsCount[child]--;
            }
            graph.Remove(nodeToRemove);
            removedNodes.Add(nodeToRemove);
        }

        if (graph.Count != 0)
            IsAsyclic = false;

        return IsAsyclic;
    }
}

