namespace Zigzag_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ZigzagMatrix
    {
        public static void Main(string[] args)
        {
            int numberOfRows = int.Parse(Console.ReadLine());
            int numberOfColumns = int.Parse(Console.ReadLine());

            int[][] matrix = new int[numberOfRows][];
            int[,] maxPaths = new int[numberOfRows, numberOfColumns];
            int[,] previousRowIndex = new int[numberOfRows, numberOfColumns];

            ReadMatrix(numberOfRows, matrix);

            // Initialize the first column
            for (int row = 1; row < numberOfRows; row++)
            {
                maxPaths[row, 0] = matrix[row][0];
            }

            // Fill max paths
            for (int col = 1; col < numberOfColumns; col++)
            {
                for (int row = 0; row < numberOfRows; row++)
                {
                    // Search for the best way to the cell we're on at the moment
                    int previousMax = 0;

                    if(col % 2 == 0) // On even columns we check cells that are above
                    {
                        for (int i = 0; i <= row-1; i++)
                        {
                            if(maxPaths[i, col-1] > previousMax)
                            {
                                previousMax = maxPaths[i, col - 1];
                                previousRowIndex[row, col] = i;
                            }
                        }
                    }

                    else // On odd columns we check cells that are below
                    {
                        for (int i = row + 1; i < numberOfRows; i++)
                        {
                            if(maxPaths[i, col-1] > previousMax)
                            {
                                previousMax = maxPaths[i, col - 1];
                                previousRowIndex[row, col] = i;  
                            }
                        }
                    }

                    maxPaths[row, col] = previousMax + matrix[row][col];

                }
            }

            var maxRow = GetLastRowIndexOfPath(maxPaths, numberOfColumns);
            var path = RecoverMaxPath(numberOfColumns, matrix, maxRow, previousRowIndex);

            Console.WriteLine("{0} = {1}", path.Sum(), string.Join(" + ", path));
        }

        private static void ReadMatrix(int numberOfRows, int[][] matrix)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
            }
        }

        private static int GetLastRowIndexOfPath(int[,] maxPaths, int numberOfColumns)
        {
            int maxRow = 0;

            for (int row = 0; row < maxPaths.GetLength(0); row++)
            {
                if (maxPaths[row, numberOfColumns - 1] > maxPaths[maxRow, numberOfColumns - 1])
                    maxRow = row;
            }

            return maxRow;
        }

        private static List<int> RecoverMaxPath(
            int numberOfColumns, 
            int[][] matrix, 
            int rowIndex, 
            int[,] previousRowIndex)
        {
            List<int> path = new List<int>();

            for (int col = numberOfColumns-1; col >= 0; col--)
            {
                path.Add(matrix[rowIndex][col]);

                rowIndex = previousRowIndex[rowIndex, col];
            }

            path.Reverse();

            return path;
        }
    }
}