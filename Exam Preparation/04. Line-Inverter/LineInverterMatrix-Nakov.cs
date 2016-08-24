using System;
using System.Collections.Generic;
using System.Linq;

class LineInverterMatrix
{
    static int n;
    static bool[,] matrix;

    static void Main()
    {
        ReadMatrix();

        // Greedy algorithm: at each iteration invert a row or column
        // holding a maximum number of white cells
        for (int iteration = 0; iteration < 2*n+1; iteration++)
        {
            // Calculate the white cells in each row and each column
            List<int> whiteCellsInRow = (new int[n]).ToList();
            List<int> whiteCellsInColumn = (new int[n]).ToList();
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (matrix[row, col])
                    {
                        whiteCellsInColumn[col]++;
                        whiteCellsInRow[row]++;
                    }
                }
            }

            int maxRowWhiteCells = whiteCellsInRow.Max();
            int maxColWhiteCells = whiteCellsInColumn.Max();
            if (maxColWhiteCells == 0 && maxRowWhiteCells == 0)
            {
                // No white cells -> solution found
                Console.WriteLine(iteration);
                return;
            }

            if (maxRowWhiteCells >= maxColWhiteCells)
            {
                // Invert the row holding maximum white cells
                int rowIndex = whiteCellsInRow.IndexOf(maxRowWhiteCells);
                InvertRow(rowIndex);
            }
            else
            {
                // Invert the column holding maximum white cells
                int colIndex = whiteCellsInColumn.IndexOf(maxColWhiteCells);
                InvertColumn(colIndex);
            }
        }

        // No solution (white cells survived after 2*n+1 iterations)
        Console.WriteLine(-1);
    }

    static void ReadMatrix()
    {
        n = int.Parse(Console.ReadLine());
        matrix = new bool[n, n];
        for (int row = 0; row < n; row++)
        {
            string line = Console.ReadLine();
            for (int col = 0; col < n; col++)
            {
                matrix[row, col] = (line[col] == 'W');
            }
        }
    }

    static void InvertRow(int rowNum)
    {
        for (int col = 0; col < n; col++)
        {
            matrix[rowNum, col] = !matrix[rowNum, col];
        }
    }

    static void InvertColumn(int colNum)
    {
        for (int row = 0; row < n; row++)
        {
            matrix[row, colNum] = !matrix[row, colNum];
        }
    }
}
