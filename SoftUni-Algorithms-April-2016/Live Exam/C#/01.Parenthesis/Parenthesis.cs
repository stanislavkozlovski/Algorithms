using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Parenthesis
{
    static char[] array;
    static char[] printAr;
    static StringBuilder outputSB = new StringBuilder();
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        GenParenthesis("", 0, 0, n);
        Console.Write(outputSB.ToString());
    }

    private static void GenParenthesis(string output, int open, int close, int pairs)
    {
        if ((open == pairs) && (close == pairs)) // the opening and closing brackets must be the same number for the parenthesis to be valid
        {
            outputSB.AppendLine(output);
        }
        else
        {
            // We add until we get an even pair of opening and closing brackets

            if (open < pairs)
                GenParenthesis(output + "(", open + 1, close, pairs);

            if (close < open)
                GenParenthesis(output + ")", open, close + 1, pairs);
        }
    }
}