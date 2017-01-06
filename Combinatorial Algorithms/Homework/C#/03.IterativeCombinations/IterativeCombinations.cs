using System;
using System.Collections.Generic;

class IterativeCombinations
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        Combinations(k, n);       
    }
    public static void Combinations(int m, int n)
    {
        int[] result = new int[m];
        Stack<int> stack = new Stack<int>();
        stack.Push(1);

            while (stack.Count > 0)
        {
            int index = stack.Count - 1;
            int value = stack.Pop(); // removes last value

            while (value <= n) // checks if the value is out of range n
            {
                result[index] = value;

                // increment values
                index++;
                value++;

                stack.Push(value); // adds last value

                if (index == m) // checks if we've reached end of array
                {
                    Console.WriteLine(string.Join(" ", result));       
                    break;
                }
            }
        }
    }
}

