using System;
using System.Collections.Generic;

class EightQueens
{
    static int solutionsFound = 0;
    const int Size = 8;
    static bool[,] chessboard = new bool[Size, Size];
    static HashSet<int> attackedColumns = new HashSet<int>();
    static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
    static HashSet<int> attackedRightDiagonals = new HashSet<int>();
    static void Main()
    {
        PutQueens(0);
        Console.WriteLine(solutionsFound);
    }
    static void PutQueens(int row)
    {
        if (row == Size)
            PrintSolution();
        else
        {
            for (int col = 0; col < Size; col++)
            {
                if(CanPlaceQueen(row, col))
                {
                    MarkAllAttackedPositions(row, col);
                    PutQueens(row + 1);
                    UnmarkAllAttackedPositions(row, col);
                }
            }
        }
    }
    static void UnmarkAllAttackedPositions(int row, int col)
    {
        attackedColumns.Remove(col);
        attackedLeftDiagonals.Remove(col - row);
        attackedRightDiagonals.Remove(col + row);

        chessboard[row, col] = false;
    }
    static void MarkAllAttackedPositions(int row, int col)
    {
        attackedColumns.Add(col);
        attackedLeftDiagonals.Add(col - row);
        attackedRightDiagonals.Add(col + row);

        chessboard[row, col] = true;
    }
    static bool CanPlaceQueen(int row, int col)
    {
        bool canPlaceQueen = 
            attackedColumns.Contains(col) || 
            attackedLeftDiagonals.Contains(col - row) || 
            attackedRightDiagonals.Contains(col + row); 

        return !canPlaceQueen;
    }
    static void PrintSolution()
    {
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                if (chessboard[row, col])
                {
                    Console.Write("|");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("*");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("|");
                }
                else
                {
                    Console.Write("|-|");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        solutionsFound++;
    }
}

