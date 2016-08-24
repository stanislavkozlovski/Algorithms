using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace _01.Sequences
{
    class Sequences
    {
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc); // used to reduce memory

        static HashSet<string> used = new HashSet<string>();
        static LinkedList<int> numbers = new LinkedList<int>();
        static int n;
        static StringBuilder output = new StringBuilder();
        static void Main(string[] args)
        {
           
            n = int.Parse(Console.ReadLine());
            GenComb();
            MinimizeFootprint();
            Console.Write(output.ToString());
        }
        static void GenComb(byte index = 1, int sum = 0)
        {
            string numbersStr = string.Join(" ", numbers);

            if (!used.Contains(numbersStr) && sum <= n && numbersStr != "")
            {
                output.AppendLine(numbersStr);
                used.Add(numbersStr);
            }
            for (byte i = 1; i <= n; i++)
            {
                if (sum + i > n)
                {
                    return;
                }

                sum += i;
                numbers.AddLast(i);

                GenComb(i, sum);

                sum -= i;
                numbers.RemoveLast();
            }      
        }

        static void MinimizeFootprint()
        {
            // Reduces memory
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }
    }
}
