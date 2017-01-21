namespace Longest_Common_Subsequence
{
    using System;
    using System.Collections.Generic;

    public class LongestCommonSubsequence
    {
        public static void Main()
        {
            var firstStr = "tree";
            var secondStr = "team";

            var lcs = FindLongestCommonSubsequence(firstStr, secondStr);

            Console.WriteLine("Longest common subsequence:");
            Console.WriteLine("  first  = {0}", firstStr);
            Console.WriteLine("  second = {0}", secondStr);
            Console.WriteLine("  lcs    = {0}", lcs);
        }

        public static string FindLongestCommonSubsequence(string firstStr, string secondStr)
        {
            int[,] lcs = new int[firstStr.Length + 1, secondStr.Length + 1];

            for (int row = 1; row < lcs.GetLength(0); row++)
            {
                for (int col = 1; col < lcs.GetLength(1); col++)
                {
                    if(firstStr[row-1] == secondStr[col - 1])
                    {
                        lcs[row, col] = lcs[row - 1, col - 1] + 1;
                    }
                    else
                    {
                        lcs[row, col] = Math.Max(lcs[row - 1, col], lcs[row, col - 1]);
                    }
                }
            }
            return RetrieveLCS(firstStr, secondStr, lcs);
        }
        public static string RetrieveLCS(string firstStr, string secondStr, int[,] lcs)
        {
            List<char> sequence = new List<char>();
            int i = firstStr.Length;
            int j = secondStr.Length;

            while(i > 0 && j > 0)
            {
                if (firstStr[i - 1] == secondStr[j - 1])
                {
                    sequence.Add(firstStr[i - 1]);
                    i--;
                    j--;
                }
                else if (lcs[i, j] == lcs[i - 1, j])
                {
                    i--;
                }
                else
                {
                    j--;
                }
            }
            sequence.Reverse();

            return new string(sequence.ToArray());
        }
    }
}
