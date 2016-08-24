using System;
using System.Linq;

public class Bridges
{
	public static void Main()
	{
		int[] northSide = Console.ReadLine()
			.Split()
			.Select(int.Parse)
			.ToArray();

		int[] southSide = Console.ReadLine()
			.Split()
			.Select(int.Parse)
			.ToArray();

		int[,] maxBridges = new int[northSide.Length, southSide.Length];

		maxBridges[0, 0] = northSide[0] == southSide[0] ? 1 : 0;

		// Initialize first row
		for (int col = 1; col < maxBridges.GetLength(1); col++)
		{
			maxBridges[0, col] = maxBridges[0, col - 1];
			if (northSide[0] == southSide[col])
			{
				maxBridges[0, col]++;
			}
		}

		// Initialize first column
		for (int row = 1; row < maxBridges.GetLength(0); row++)
		{
			maxBridges[row, 0] = maxBridges[row - 1, 0];
			if (northSide[row] == southSide[0])
			{
				maxBridges[row, 0]++;
			}
		}

		// Calculate maxBridges row by row and column by column
		for (int row = 1; row < maxBridges.GetLength(0); row++)
		{
			for (int col = 1; col < maxBridges.GetLength(1); col++)
			{
				maxBridges[row, col] = Math.Max(maxBridges[row - 1, col], maxBridges[row, col - 1]);

				if (northSide[row] == southSide[col])
				{
					maxBridges[row, col]++;
				}
			}
		}

		Console.WriteLine(maxBridges.Cast<int>().Max());
	}
}
