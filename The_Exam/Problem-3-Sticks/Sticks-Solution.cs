using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sticks
{
    class Program
    {
        static HashSet<Tuple<int, int>> edges;

        static void GenerateTests()
        {
            using (var sw = new StreamWriter("output.txt"))
            {
                var nodeCount = 20;
                sw.WriteLine(nodeCount);
                var edgeCount = 40;
                sw.WriteLine(edgeCount);
                edges = new HashSet<Tuple<int, int>>();
                var rnd = new Random();
                for (int i = 0; i < edgeCount; i++)
                {
                    var a = rnd.Next(0, nodeCount);
                    var b = rnd.Next(0, nodeCount);
                    if (a == b || IsDuplicate(a, b))
                    {
                        i--;
                    }
                    else
                    {
                        sw.WriteLine("{0} {1}", a, b);
                    }
                }
            }
        }

        static bool IsDuplicate(int a, int b)
        {
            return edges.Contains(new Tuple<int, int>(a, b)) ||
                edges.Contains(new Tuple<int, int>(b, a));
        }

        const int MinPerRank = 10;
        const int MaxPerRank = 20;
        const int MinRanks = 20;
        const int MaxRanks = 30;
        const int Percent = 40;

        static void GenerateTestsNoCycle()
        {
            var inputNodes = new HashSet<int>();
            var edges = new HashSet<string>();
            using (var sw = new StreamWriter("output.txt"))
            {
                var rand = new Random();
                int ranks = MinRanks + rand.Next(0, MaxRanks - MinRanks + 1);

                var nodes = 0;
                for (int i = 0; i < ranks; i++)
                {
                    var newNodes = MinPerRank + rand.Next(0, MaxPerRank - MinPerRank + 1);

                    var nodesAdded = 0;
                    for (int j = 0; j < nodes; j++)
                    {
                        for (int k = 0; k < newNodes; k++)
                        {
                            if (rand.Next(0, 100) < Percent)
                            {
                                nodesAdded++;
                                inputNodes.Add(j);
                                inputNodes.Add(k + nodes);
                                edges.Add(string.Format("{0} {1}", j, k + nodes));
                            }
                        }
                    }

                    nodes += newNodes;
                }

                sw.WriteLine(inputNodes.Count);
                sw.WriteLine(edges.Count);
                foreach (var edge in edges)
                {
                    sw.WriteLine(edge);
                }
            }
        }

        static void Main(string[] args)
        {
            //GenerateTestsNoCycle();
            ////return;
            //Console.SetIn(new StreamReader("output.txt"));
            var stickCount = int.Parse(Console.ReadLine());
            var graph = new List<int>[stickCount];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            var edgeCount = int.Parse(Console.ReadLine());
            var parentCount = new int[stickCount];
            for (int i = 0; i < edgeCount; i++)
            {
                var data = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                graph[data[0]].Add(data[1]);
                parentCount[data[1]]++;
            }

            var lifted = new bool[stickCount];
            var cycle = false;
            var liftOrder = new List<int>();
            for (int i = 0; i < stickCount; i++)
            {
                var nextStick = -1;

                // Get next stick to lift
                for (int j = 0; j < stickCount; j++)
                {
                    if (!lifted[j] && parentCount[j] == 0 && j > nextStick)
                    {
                        nextStick = j;
                    }
                }

                if (nextStick == -1) // There are still sticks but no stick has 0 parents
                {
                    cycle = true;
                    break;
                }

                foreach (var child in graph[nextStick])
                {
                    parentCount[child]--;
                }

                liftOrder.Add(nextStick);
                lifted[nextStick] = true;
            }

            if (cycle)
            {
                Console.WriteLine("Cannot lift all sticks");
            }

            Console.WriteLine(string.Join(" ", liftOrder));
        }
    }
}
