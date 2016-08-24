using System;
using System.Collections.Generic;

class PermutationWithRep
{
    static void Main()
    {
        int[] array = new int[] { 1, 1, 3, 5 };
        GeneratePermutations(array);
    }
    static void GeneratePermutations(int[] array, int index = 0)
    {
        if (index >= array.Length)
            Print(array);
        else
        {
            var usedNums = new HashSet<int>();
            for (int i = index; i < array.Length; i++)
            {
                if (!usedNums.Contains(array[i]))
                {
                    Swap(ref array[index], ref array[i]);
                    GeneratePermutations(array, index + 1);
                    Swap(ref array[index], ref array[i]);

                    usedNums.Add(array[i]);
                }
            }
        }
    }
    static void Print(int[] array)
    {
       Console.WriteLine("{{ {0} }}", string.Join(", ", array));
    }
    static void Swap(ref int a, ref int b)
    {
        int oldA = a;
        a = b;
        b = oldA;
    }
}

