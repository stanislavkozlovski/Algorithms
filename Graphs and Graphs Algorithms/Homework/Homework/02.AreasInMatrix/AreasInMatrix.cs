using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class AreasInMatrix
{
    static char[,] matrix = new char[,]
    //{
    //    {'a', 'a', 'c', 'c', 'c', 'a', 'a', 'c' },
    //    {'b', 'a', 'a', 'a', 'a', 'c', 'c', 'c' },
    //    {'b', 'a', 'a', 'b', 'a', 'c', 'c', 'c' },
    //    {'b', 'b', 'd', 'a', 'a', 'c', 'c', 'c' },
    //    {'c', 'c', 'd', 'c', 'c', 'c', 'c', 'c' },
    //    {'c', 'c', 'd', 'c', 'c', 'c', 'c', 'c' }
    //};
    {
        {'a','s','s','s','a','a','d','a','s' },
        {'a','d','s','d','a','s','d','a','d' },
        {'s','d','s','d','a','d','s','a','s' },
        {'s','d','a','s','d','s','d','s','a' },
        {'s','s','s','s','a','s','d','d','d' }
    };
    static bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
    static Dictionary<char, int> areas = new Dictionary<char, int>();
    static void Main()
    {
        /*Because of the way DFS works, starting from one point in 'a' will find the whole area that's connected to that certain 'a'
          We mark all of the cells we've been through as visited (we don't go through cells that aren't 'a')
          and we loop through the whole matrix to find another area of 'a' that can't be visited by DFS (an area that's cut off)    */


        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                if (!areas.ContainsKey(matrix[row, col])) // creates a key if there isnt one
                    areas[matrix[row, col]] = 0;

                if (DFS(row, col, matrix[row, col])) // if it starts traversing (if we haven't visited the cell), we increment the number of areas
                    areas[matrix[row, col]]++;
            }
        }
        Print();
    }
    static bool DFS(int row, int col,char searchedCharacter)
    {
        if (row >= matrix.GetLength(0) || col >= matrix.GetLength(1) || col < 0 || row < 0) // makes sure we dont go out of bounds
            return false;
        if (matrix[row, col] != searchedCharacter || visited[row, col]) // returns if it's not the character we're searching for
            return false;

        visited[row, col] = true;
        DFS(row - 1, col, searchedCharacter); // go up
        DFS(row + 1, col, searchedCharacter); // go down
        DFS(row, col - 1, searchedCharacter); // go left
        DFS(row, col + 1, searchedCharacter); // go right
        return true;
    }
    static void Print()
    {
        int totalAreas = 0;
        areas.ToList().ForEach(x => totalAreas += x.Value); // does a quick calculation for the total number of areas
        areas = areas.OrderBy(x => x.Key).ToDictionary(x => x.Key, k => k.Value); // orders the letters alphabetically
        Console.WriteLine("Areas: {0}", totalAreas);

        foreach (var key in areas)
        {
            Console.WriteLine("Letter '{0}' -> {1}", key.Key, key.Value);
        }
    }
}

