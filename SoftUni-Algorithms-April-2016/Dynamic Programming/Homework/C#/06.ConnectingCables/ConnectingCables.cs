using System;
using System.Collections.Generic;

class ConnectingCables
{
        
    static void Main(string[] args)
    {
        List<int> side1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //{ 1,2,3,4};
        //{ 1,2,3};
        List<int> side2 = new List<int>() { 2, 5, 3, 8, 7, 4, 6, 9, 1 };
        //{ 4,3,2,1};
        //{ 1,2,3};
        int[,] matrix = new int[side1.Count, side2.Count];

        // fill first row
        for (int row = 1; row < matrix.GetLength(0); row++)
        {
            int value = matrix[row - 1, 0];

            if (side1[row] == side2[0])
            {
                value++;
            }

            matrix[row, 0] = value;
        }
        // fill first col
        for (int col = 1; col < matrix.GetLength(1); col++)
        {
            int value = matrix[0, col - 1];

            if (side1[0] == side2[col])
                value++;

            matrix[0, col] = value;
        }


        for (int row = 1; row < matrix.GetLength(0); row++)
        {
            for (int col = 1; col < matrix.GetLength(1); col++)
            {
                int value = Math.Max(matrix[row - 1, col], matrix[row, col - 1]);

                if (side1[row] == side2[col])
                    value++;

                matrix[row, col] = value;
            }
        }

        int getRow = matrix.GetLength(0) - 1;
        int getCol = matrix.GetLength(0) - 1;
        List<int> connectedCables = new List<int>();
        while(getRow >= 0 && getCol >= 0)
        {
            if (side1[getRow] == side2[getCol])
            {
                connectedCables.Add(side1[getRow]);
                getRow--;
                getCol--;
            }
            else if (matrix[getRow - 1, getCol] == matrix[getRow, getCol])
            {
                getRow--;
            }
            else
                getCol--;
        }
        connectedCables.Reverse();

        Console.WriteLine("Maximum pairs connected: {0}", matrix[matrix.GetLength(0)-1, matrix.GetLength(1)-1]);
        Console.WriteLine("Connected pairs: {0}", string.Join(" ", connectedCables));
    }
}

