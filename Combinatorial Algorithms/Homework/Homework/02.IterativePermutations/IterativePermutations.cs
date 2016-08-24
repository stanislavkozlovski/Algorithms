using System;
using System.Linq;


class IterativePermutations
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] array = Enumerable.Range(1, n).ToArray();
        int[] free = Enumerable.Range(0, n+1).ToArray();
        int index = 1;
        int j = 0;

        Console.WriteLine(string.Join(" ", array));
        while (index < n)
        {
            free[index]--;

            if(index % 2 == 0)
            {
                j = 0;
            }
            else
            {
                j = free[index];
            }
              
            Swap(ref array[index], ref array[j]);

            Console.WriteLine(string.Join(" ", array));

            index = 1;

            while (free[index] == 0)
            {
                free[index] = index;
                index++;
            }

        }
    }
    static void Swap(ref int a, ref int b)
    {
        if (a == b)
            return;

        int oldA = a;
        a = b;
        b = oldA;
    }
}

