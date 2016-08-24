using System;
using System.Collections.Generic;
using System.Linq;

class LineInverter
{
    static bool[,] matrix;
    static void Main()
    {
        ReadInput();
        /* A greedy approach where we invert the row or line with the most whites in it */

        for (int iteration = 0; iteration < matrix.GetLength(0)*2; iteration++)
        {
            List<int> whitesInCol = new int[matrix.GetLength(0)].ToList();
            List<int> whitesInRow = new int[matrix.GetLength(0)].ToList();

            // Read and save the number of whites in each row and col
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (!matrix[row, col])
                    {
                        whitesInRow[row]++;
                        whitesInCol[col]++;
                    }
                }
            }

            int maximumWhitesInRow = whitesInRow.Max();
            int maxRow = whitesInRow.IndexOf(maximumWhitesInRow);

            int maximumWhitesInCol = whitesInCol.Max();
            int maxCol = whitesInCol.IndexOf(maximumWhitesInCol);

            if(maximumWhitesInCol == 0 && maximumWhitesInRow == 0)
            {
                // There are no more white spaces, problem solved
                Console.WriteLine(iteration);
                return;
            }
            // See where there are more white cells to invert and invert that
            if(maximumWhitesInRow >= maximumWhitesInCol)
            {
                InvertRow(maxRow);
            }
            else
            {
                InvertCol(maxCol);
            }
        }

        Console.WriteLine(-1);
    }
    static void InvertRow(int row)
    {
        for (int col = 0; col < matrix.GetLength(1); col++)
        {
            matrix[row, col] = !matrix[row, col]; // invert
        }
    }
    static void InvertCol(int col)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            matrix[row, col] = !matrix[row, col]; // invert
        }
    }
    private static void ReadInput()
    {
        /* All white cells are marked as FALSE
           and black cells as TRUE             */
        int n = int.Parse(Console.ReadLine());
        matrix = new bool[n, n];
        for (int row = 0; row < n; row++)
        {
            string rowInput = Console.ReadLine();
            for (int col = 0; col < rowInput.Length; col++)
            {
                if (rowInput[col] == 'B')
                    matrix[row, col] = true;
            }
        }
    }
}

