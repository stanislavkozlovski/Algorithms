using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.Shortest_Path_In_Matrix
{
    class ShortestPath
    {
        private static int[][] matrix;
        private static int matrixRow;
        private static int matrixCol;
        private static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> graph = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
        static void Main(string[] args)
        {
            BuildMatrix();
            BuildGraph();
            Dijkstra();
        }
        static void Dijkstra()
        {
            long[,] distance = new long[matrix.GetLength(0), matrixCol];
            bool[,] used = new bool[matrix.GetLength(0), matrixCol];
            Tuple<int,int>[,] prev = new Tuple<int,int>[matrix.GetLength(0), matrixCol];

            // initialize distance
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrixCol; j++)
                {
                    distance[i, j] = long.MaxValue;
                }
            }
            distance[0, 0] = 0; // sourceNode

            while (true)
            {
                // Find the nearest unused node from source
                long minDistance = long.MaxValue;
                int minRow = 0, minCol = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrixCol; j++)
                    {
                        if(!used[i,j] && distance[i,j] < minDistance)
                        {
                            minDistance = distance[i, j];
                            minRow = i;
                            minCol = j;
                        }
                    }
                }

                if (minDistance == long.MaxValue)
                    break;

                used[minRow, minCol] = true;

                //Calculate the distance 
                foreach (var neighbour in graph[new Tuple<int, int>(minRow, minCol)])
                {
                    if(distance[neighbour.Item1, neighbour.Item2] > distance[minRow, minCol] + matrix[minRow][minCol])
                    {
                        distance[neighbour.Item1, neighbour.Item2] = minDistance + matrix[minRow][minCol];
                        prev[neighbour.Item1, neighbour.Item2] = new Tuple<int, int>(minRow, minCol);
                    }
                }
            }

            ReconstructAndPrintPath(prev);
        }
        static void ReconstructAndPrintPath(Tuple<int, int>[,] prev)
        {
            var path = new List<int>();
            Tuple<int, int> currentNode = new Tuple<int, int>(matrixRow - 1, matrixCol - 1);

            while(currentNode != null)
            {
                path.Add(matrix[currentNode.Item1][currentNode.Item2]);
                currentNode = prev[currentNode.Item1, currentNode.Item2];
            }
            path.Reverse();

            Console.WriteLine("Length: {0}", path.Sum());
            Console.WriteLine("Path: {0}", string.Join(" ", path));
        }
        static void BuildMatrix()
        {
            matrixRow = int.Parse(Console.ReadLine());
            matrixCol = int.Parse(Console.ReadLine());

            matrix = new int[matrixRow][];
            for (int curRow = 0; curRow < matrixRow; curRow++)
            {
                matrix[curRow] = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            }
        }
        static void BuildGraph()
        {
            int maxRow = matrix.GetLength(0);
            int maxCol = matrixCol;
            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < maxCol; col++)
                {
                    Tuple<int, int> currentNode = new Tuple<int, int>(row, col);
                    graph.Add(currentNode, new List<Tuple<int, int>>());

                    // Add Neighbours
                    if (row - 1 >= 0) // top neighbour
                        graph[currentNode].Add(new Tuple<int, int>(row - 1, col));
                    if (row + 1 < maxRow) // bottom neighbour
                        graph[currentNode].Add(new Tuple<int, int>(row + 1, col));
                    if (col - 1 >= 0) // left neighbour
                        graph[currentNode].Add(new Tuple<int, int>(row, col - 1));
                    if (col + 1 < maxCol) // right neighbour
                        graph[currentNode].Add(new Tuple<int, int>(row, col + 1));

                }
            }
        }
    }
}
