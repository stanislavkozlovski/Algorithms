using System;
using System.Collections.Generic;

class Zigzag
{

    static void Main()
    {
        int[] array = new int[] { 8, 3, 5, 7, 0, 8, 9, 10, 20, 20, 20, 12, 19, 11 };
       // int[] array = new int[] { 1, 3, 2 };
        //int[] array = new int[] { 24, 5, 31, 3, 3, 342, 51, 114, 52, 55, 56, 58 };
        //int[] array = new int[] { 1, 2, 3 };
        //int[] array = new int[] { 1,3,2,6,3,9,2,10};

        Console.WriteLine(string.Join(", ", LongestZigZag(array)));
        //Honestly, I barely have an idea how any of this works
    }

    public static List<int> LongestZigZag(int[] sequence)
    {
        var seq = new List<int>();
        int[] diff = new int[sequence.Length - 1];

        //stores the differences between objects [i] - [i-1]
        for (int i = 1; i < sequence.Length; i++)
        {
            diff[i - 1] = sequence[i] - sequence[i - 1];
        }

        int prevsign = Sign(diff[0]);
        seq.Add(sequence[0]);

        for (int i = 1; i < diff.Length; i++)
        {
            int sign = Sign(diff[i]);
            if (prevsign * sign == -1)
            {
                prevsign = sign;
                if (seq.Count == 1)
                {
                    seq.Add(sequence[i]);
                }
                seq.Add(sequence[i + 1]);
            }
        }

        return seq;
    }

    public static int Sign(int a)
    {
        if (a == 0)
        {
            return 0;
        }

        return a / Math.Abs(a);
    }
}

