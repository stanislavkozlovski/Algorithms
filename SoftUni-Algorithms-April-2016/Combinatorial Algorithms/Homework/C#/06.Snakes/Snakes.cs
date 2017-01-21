using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Snakes
{
    static int snakesCount = 0;
    static List<string> moves = new List<string>();
    static HashSet<string> usedCombinations = new HashSet<string>();
    static bool[,] matrix;
    static bool printed = false;
    static int matrixBounds;
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        matrixBounds = n * 2;
        matrix = new bool[matrixBounds, matrixBounds]; // create a matrix two times the size so we have space to move
        
        moves.Add("S");
        matrix[n, n] = true; // start at the center
        GenSnake(n, "R", n, n+1);
        

        Console.WriteLine("Snakes count = {0}", snakesCount);
    }
    static void GenSnake(int n, string dir,int row = 0, int col = 0,int index = 1)
    {
        if(index >= n)
        {
            if (!GenerateFlips())
            {
                Console.WriteLine(string.Join("", moves));
                snakesCount++;
                printed = true;
            }  
        }
        else
        {
            if (matrix[row, col] == false)
            {
                matrix[row, col] = true;
                moves.Add(dir);

                if (dir != "L" && col + 1 < matrixBounds && !printed) GenSnake(n, "R", row, col + 1, index + 1);
                if (dir != "U" && row + 1 < matrixBounds && !printed) GenSnake(n, "D", row + 1, col, index + 1);
                if (dir != "R" && col - 1 >= 0 && !printed) GenSnake(n, "L", row, col - 1, index + 1);
                if (dir != "D" && row - 1 >= 0 && !printed) GenSnake(n, "U", row - 1, col, index + 1);

                printed = false;
                matrix[row, col] = false;
                moves.RemoveAt(moves.Count() - 1);
            }

            else return;

        }
    }
    static bool GenerateFlips()
    {
        bool isUsed = false;
        string move = string.Join("", moves);
        string horizontalMove = FlipHorizontal(move);

        string reversed = Reverse(move);
        string horizontalReversed = Reverse(horizontalMove);
        for (int i = 0; i < 1; i++)
        {
            if (usedCombinations.Contains(move))
                isUsed = true;
            else
                usedCombinations.Add(move);

            if (horizontalMove != move && usedCombinations.Contains(horizontalMove))
                isUsed = true;
            else
                usedCombinations.Add(horizontalMove);

            if (usedCombinations.Contains(reversed) && reversed != move && reversed != horizontalMove)
                isUsed = true;
            else
                usedCombinations.Add(reversed);

            if (usedCombinations.Contains(horizontalReversed) && horizontalReversed != horizontalMove && horizontalReversed != move)
                isUsed = true;
            else
                usedCombinations.Add(horizontalReversed);

            move = TurnClockwise(move);
            reversed = Reverse(move);

            horizontalMove = TurnClockwise(horizontalMove);
            horizontalReversed = Reverse(horizontalMove);
        }

        return isUsed;
    }
    static string FlipHorizontal(string move)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < move.Length; i++)
        {
            if (move[i] == 'U')
                sb.Append('D');
            else if (move[i] == 'D')
                sb.Append('U');
            else
                sb.Append(move[i]);
        }

        return sb.ToString();
    }
    static string Reverse(string move) 
    {
        /* some snakes are the same but we can't get to them using only clockwise turns and flips
           that's why we reverse a snake, starting from its last position to its first
           and then rotate it until we start with SR again
           Example: SRRU - Reversed to SDLL - Turned Clockwise to SRDD                          */

        StringBuilder sb = new StringBuilder();

        sb.Append('S');
        for (int i = move.Length-1; i >= 1; i--)
        {
            switch (move[i])
            {
                case 'R':
                    sb.Append('L');
                    break;
                case 'L':
                    sb.Append('R');
                    break;
                case 'U':
                    sb.Append('D');
                    break;
                case 'D':
                    sb.Append('U');
                    break;
            }
        }
        string toRotate = sb.ToString();

        while(toRotate[1] != 'R') // turn clockwise until we start with SR
        {
            toRotate = TurnClockwise(toRotate);
        }
        return toRotate;
    }
    static string TurnClockwise(string move)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < move.Length; i++)
        {
            switch (move[i])
            {
                case 'U':
                    sb.Append('R');
                    break;
                case 'D':
                    sb.Append('L');
                    break;
                case 'L':
                    sb.Append('U');
                    break;
                case 'R':
                    sb.Append('D');
                    break;
                default:
                    sb.Append(move[i]);
                    break;
            }
        }

        return sb.ToString();
    }
}

