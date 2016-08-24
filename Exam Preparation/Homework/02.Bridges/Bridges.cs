using System;
using System.Linq;

class Bridges
{
    const int NotCalculated = -1;
    static int[,] maxBridges;
    static int[] northBridge;
    static int[] southBridge;
    static void Main()
    {
        northBridge = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        southBridge = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        FillMatrix();
        Console.WriteLine(FindLength(northBridge.Length - 1, southBridge.Length - 1));
    }
    static int FindLength(int row, int col) // row is each northbridge node and col is each southbridge node
    {
        // Here we iterate through each of their node and find the optimal connection for the one before it
        if (row < 0 || col < 0) // if we're at node -1 the answer is obviously 0 connected bridges because there aren't any nodes
            return 0;

        if (maxBridges[row, col] != NotCalculated) // use dynamic programming and return something if we've already calculated it
            return maxBridges[row, col];

        if(northBridge[row] == southBridge[col]) // if the numbers are identical we make a connection
        {
            maxBridges[row,col] = Math.Max(FindLength(row - 1, col), FindLength(row, col - 1)) + 1; // return the max of both possibilities AND add 1 because of the connection we've just made
        }
        else
        {
            maxBridges[row, col] = Math.Max(FindLength(row - 1, col), FindLength(row, col - 1));
        }

        return maxBridges[row, col]; 
    }
    static void FillMatrix()
    {
        maxBridges = new int[northBridge.Length, southBridge.Length];
        for (int row = 0; row < maxBridges.GetLength(0); row++)
        {
            for (int col = 0; col < maxBridges.GetLength(1); col++)
            {
                maxBridges[row, col] = NotCalculated;
            }
        }
    }
}

