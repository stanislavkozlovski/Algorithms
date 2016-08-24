using System;
using System.Linq;

class Bridges
{
    static int[,] maxBridges;
    static int[] north;
    static int[] south;

    const int NotCalculated = -1;

    static void Main()
    {
        north = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        south = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

        maxBridges = new int[north.Length, south.Length];

        for (int x = 0; x < north.Length; x++)
        {
            for (int y = 0; y < south.Length; y++)
            {
                maxBridges[x, y] = NotCalculated;
            }
        }

        CalcMaxBridges(north.Length - 1, south.Length - 1);

        Console.WriteLine(maxBridges[north.Length - 1, south.Length - 1]);
    }

    static int CalcMaxBridges(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return 0;
        }

        if (maxBridges[x,y] != NotCalculated)
        {
            return maxBridges[x, y];
        }

        int northLeft = CalcMaxBridges(x - 1, y);
        int southLeft = CalcMaxBridges(x, y - 1);

        if (north[x] == south[y])
        {
            maxBridges[x, y] = 1 + Math.Max(northLeft, southLeft);
        }
        else
        {
            maxBridges[x, y] = Math.Max(northLeft, southLeft);
        }

        return maxBridges[x, y];
    }
}
