using System;
using System.Collections.Generic;
using System.Linq;

class KnightsTour
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[,] path = new int[n, n];
        int[,] visited = new int[n, n];
        int row = 0;
        int col = 0;
        int step = 1;
        visited[row, col] = step;

        while (true)
        {
            List<Tuple<int, int>> steps = GetPossibleMoves(visited, row, col);

            int minimum = int.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < steps.Count(); i++)
            {
                // Get the possible moves for each position
                int possibleMovesCount = GetPossibleMoves(visited, steps[i].Item1, steps[i].Item2).Count;

                if (possibleMovesCount <= minimum)
                {
                    // saves the one with the lowest possible steps from there
                    minimum = possibleMovesCount;
                    minIndex = i;
                }
            }

            if (minimum == 0) // reached the end
            {
                // add the last cell
                row = steps[minIndex].Item1;
                col = steps[minIndex].Item2;

                visited[row, col] = ++step;
                break;
            }

            row = steps[minIndex].Item1;
            col = steps[minIndex].Item2;

            visited[row, col] = ++step;
        }

        PrintResults(visited);
    }
    static List<Tuple<int,int>> GetPossibleMoves(int[,] visited, int row, int col)
    {
        List<Tuple<int, int>> coordsToVisit = new List<Tuple<int, int>>();

        if(row - 2 >= 0)
        {
            if(col - 1 >= 0)
                // top left
                if (visited[row - 2, col - 1] == 0)
                    coordsToVisit.Add(new Tuple<int, int>(row - 2, col - 1));

            if(col + 1 < visited.GetLength(1))
                // top right
                if (visited[row - 2, col + 1] == 0)
                    coordsToVisit.Add(new Tuple<int, int>(row - 2, col + 1));
        }

        if(row + 2 < visited.GetLength(0))
        {
            if(col - 1 >= 0)
                // bot left
                if (visited[row + 2, col - 1] == 0)
                    coordsToVisit.Add(new Tuple<int, int>(row + 2, col - 1));

            if(col + 1 < visited.GetLength(1))
                // bot right
                if (visited[row + 2, col + 1] == 0)
                    coordsToVisit.Add(new Tuple<int, int>(row + 2, col + 1));
        }

        if(col - 2 >= 0)
        {
            if(row - 1 >= 0)
                // left up
                if (visited[row - 1, col - 2] == 0)
                    coordsToVisit.Add(new Tuple<int, int>(row - 1, col - 2));

            if (row + 1 < visited.GetLength(0))
                // left down
                if(visited[row + 1, col - 2] == 0)
                    coordsToVisit.Add(new Tuple<int, int>(row + 1, col - 2));
        }

        if(col + 2 < visited.GetLength(1))
        {
            if (row - 1 >= 0)
                // right up
                if(visited[row-1,col+2] == 0)
                coordsToVisit.Add(new Tuple<int, int>(row - 1, col + 2));

            if (row + 1 < visited.GetLength(0))
                // right down
                if (visited[row + 1, col + 2] == 0) 
                coordsToVisit.Add(new Tuple<int, int>(row + 1, col + 2));
        }
        return coordsToVisit;
    }
    static void PrintResults(int[,] visited)
    {
        for (int row = 0; row < visited.GetLength(0); row++)
        {
            for (int col = 0; col < visited.GetLength(1); col++)
            {
                Console.Write(visited[row, col].ToString().PadLeft(4));
            }
            Console.WriteLine();
        }
    }
}

