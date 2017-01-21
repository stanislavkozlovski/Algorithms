using System;
using System.Linq;

public class LineInverter
{
	public static void Main()
	{
		int size = int.Parse(Console.ReadLine());

		bool[,] board = new bool[size, size];
		ReadBoard(size, board);

		int rowsToInvert = GetNumberOfRowsToInvert(size, board);
		int columnsToInvert = GetNumberOfColumnsToInvert(size, board);

		int numberOfInversions = 0;
		if (columnsToInvert > size / 2 && columnsToInvert > rowsToInvert)
		{
			InvertRow(0, board);
			numberOfInversions++;
		}
		else if (rowsToInvert > size / 2)
		{
			InvertColumn(0, board);
			numberOfInversions++;
		}

		rowsToInvert = GetNumberOfRowsToInvert(size, board);
		columnsToInvert = GetNumberOfColumnsToInvert(size, board);

		if (rowsToInvert <= columnsToInvert)
		{
			numberOfInversions += InvertRows(size, board);
			numberOfInversions += InvertColumns(size, board);
		}
		else
		{
			numberOfInversions += InvertColumns(size, board);
			numberOfInversions += InvertRows(size, board);
		}

		if (IsBoardBlack(board))
		{
			Console.WriteLine(numberOfInversions);
		}
		else
		{
			Console.WriteLine(-1);
		}
	}

	static void ReadBoard(int size, bool[,] board)
	{
		for (int row = 0; row < size; row++)
		{
			string currentRow = Console.ReadLine();

			for (int col = 0; col < size; col++)
			{
				board[row, col] = currentRow[col] == 'W'; // white == true, black == false
			}
		}
	}

	private static int GetNumberOfRowsToInvert(int size, bool[,] board)
	{
		int rowsToInvert = 0;
		for (int row = 0; row < size; row++)
		{
			if (board[row, 0])
			{
				rowsToInvert++;
			}
		}

		return rowsToInvert;
	}

	private static int GetNumberOfColumnsToInvert(int size, bool[,] board)
	{
		int columnsToInvert = 0;
		for (int col = 0; col < size; col++)
		{
			if (board[0, col])
			{
				columnsToInvert++;
			}
		}

		return columnsToInvert;
	}

	private static int InvertColumns(int size, bool[,] board)
	{
		int numberOfInversions = 0;
		for (int col = 0; col < size; col++)
		{
			if (board[0, col])
			{
				InvertColumn(col, board);
				numberOfInversions++;
			}
		}

		return numberOfInversions;
	}

	private static int InvertRows(int size, bool[,] board)
	{
		int numberOfInversions = 0;
		for (int row = 0; row < size; row++)
		{
			if (board[row, 0])
			{
				InvertRow(row, board);
				numberOfInversions++;
			}
		}

		return numberOfInversions;
	}

	private static void InvertRow(int row, bool[,] board)
	{
		for (int i = 0; i < board.GetLength(1); i++)
		{
			board[row, i] = !board[row, i];
		}
	}

	private static void InvertColumn(int col, bool[,] board)
	{
		for (int i = 0; i < board.GetLength(0); i++)
		{
			board[i, col] = !board[i, col];
		}
	}

	private static bool IsBoardBlack(bool[,] board)
	{
		return board.Cast<bool>().All(cell => !cell);
	}
}
