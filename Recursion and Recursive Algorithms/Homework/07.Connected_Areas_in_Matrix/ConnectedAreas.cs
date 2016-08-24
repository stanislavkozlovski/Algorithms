using _07.Connected_Areas_in_Matrix;
using System;
using System.Collections.Generic;
using System.Linq;

class ConnectedAreas
{
    static int size = 0;
    static void Main()
    {       
        //char[,] matrix =
        //{
        //    {'1', ' ', ' ', '*', '2', ' ', ' ', '*', '3' },
        //    {' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ' },
        //    {' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ' },
        //    {' ', ' ', ' ', ' ', '*', ' ', '*', ' ', ' ' },
        //};

        char[,] matrix =
        {
            {'*', '1', ' ', '*', '3', ' ', ' ', '*', '2', ' ' },
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' ' },
            {'*', ' ', ' ', '*', '*', '*', '*', '*', ' ', ' ' },
            {'*', ' ', ' ', '*', '4', ' ', ' ', '*', ' ', ' ' },
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' ' },
        };
        List<Area> areas = new List<Area>(); // list of a class that hold the necessary parameters
        List<Tuple<int, int>> startCoords = FindCoords(matrix); // gets all the start positions

        foreach (Tuple<int,int> tuple in startCoords)
        {
            MoveInMatrix(matrix, tuple.Item1, tuple.Item2);

            areas.Add(new Area { cordX = tuple.Item1, cordY = tuple.Item2, areaSize = size }); // saves them in a list

            size = 0;
        }

        var areasQuery = SortAreas(areas);

        PrintResults(areas.Count(), areasQuery);
    }
    static void PrintResults(int areasFound, IOrderedEnumerable<Area> areasQuery)
    {
        Console.WriteLine("Total areas found: {0}", areasFound);
        int areaNum = 1;

        foreach (var area in areasQuery)
        {
            Console.WriteLine("Area #{0} at ({1}, {2}), size: {3}", areaNum, area.cordX, area.cordY, area.areaSize);
            areaNum++;
        }
    }
    static IOrderedEnumerable<Area> SortAreas(List<Area> areas)
    {
        var areasQuery = // use linq query to sort them easily
            from area in areas
            orderby area.areaSize descending
            select area;

        return areasQuery;
    }
    static void MoveInMatrix(char[,] matrix, int row, int col)
    {
        if (Collides(matrix, row, col))
            return;

        else
        {
            matrix[row, col] = '.';
            size++;

            MoveInMatrix(matrix, row + 1, col); // move down
            MoveInMatrix(matrix, row - 1, col); // move up
            MoveInMatrix(matrix, row, col + 1); // move right
            MoveInMatrix(matrix, row, col - 1); // move left
        }
    }
    static bool Collides(char[,] matrix, int row, int col)
    {
        bool outOfBounds = row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1);
        bool collides = false;
        if (!outOfBounds) collides = matrix[row, col] == '*' || matrix[row, col] == '.';

        return collides || outOfBounds;
    }
    static List<Tuple<int, int>> FindCoords(char[,] matrix)
    {
        List<Tuple<int, int>> startCoords = new List<Tuple<int, int>>();

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                if (matrix[row, col] != ' ' && matrix[row, col] != '*')
                    startCoords.Add(new Tuple<int, int>(row, col));
            }
        }

        return startCoords;
    }
}

