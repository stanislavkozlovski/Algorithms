using System;
using System.Collections.Generic;
using System.Linq;

class CellPaths
{
    static List<string> steps = new List<string>();
    static void Main()
    {
        // For some reason it doesn't save the steps correctly
        // it's because it doesn't delete ALL of the previous steps, but I don't know how to fix it
        // otherwise the logic runs well, debug and see
        char[,] matrix =
        {
            { 's',' ', ' ', ' '},
            {' ', '*', '*', ' ' },
            {' ', '*', '*', ' ' },
            {' ', '*', 'e', ' ' },
            {' ', ' ', ' ', ' ' }
        };

        MoveInMatrix(matrix, 0, 0, "");
    }
    static void MoveInMatrix(char[,] matrix, int row, int col, string direction)
    {
        if (Collides(matrix, row, col))
        {
            return;
        }
        else if (matrix[row, col] == 'e')// REACHES END
        {
            steps.Add(direction);
            Console.WriteLine(string.Join("", steps));
            return;
        }
        else
        {
            matrix[row, col] = '.';

            steps.Add(direction);

            MoveInMatrix(matrix, row - 1, col, "U"); // move up
            MoveInMatrix(matrix, row, col + 1, "R"); // move right
            MoveInMatrix(matrix, row + 1, col, "D"); // move down
            MoveInMatrix(matrix, row, col - 1, "L"); // move left

            steps.RemoveAt(steps.Count() - 1);

            matrix[row, col] = ' ';
        }
    }
    static bool Collides(char[,] matrix, int row, int col)
    {
        bool outOfBounds = row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1);
        bool collides = false;
        if(!outOfBounds) collides = matrix[row, col] == '*' || matrix[row, col] == '.';

        return collides || outOfBounds;
    }
}

