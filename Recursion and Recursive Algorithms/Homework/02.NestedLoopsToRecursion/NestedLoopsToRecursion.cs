using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class NestedLoopsToRecursion
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] array = new int[n];
        PrintLoops(array, 0);

    }
    static void PrintLoops(int[] array, int index)
    {
        if (index == array.Length)
        {
            Console.WriteLine(string.Join(" ", array));
        }
        else
        {
            for (int i = 1; i <= array.Length; i++)
            {
                array[index] = i; 
                PrintLoops(array, index + 1);
            }
        }
    }
}

