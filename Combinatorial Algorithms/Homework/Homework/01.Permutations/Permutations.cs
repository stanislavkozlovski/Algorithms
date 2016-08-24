using System;
using System.Linq;

class Permutations
{
    private static int countOfPermutations = 0;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        int[] array = Enumerable.Range(1, n).ToArray();
        int[] free = Enumerable.Range(1, n).ToArray();

        GeneratePermutations(array, 0);

        Console.WriteLine("Total permutations: {0}", countOfPermutations);
    }
    static void GeneratePermutations(int[] array, int index)
    {
        if (index >= array.Length)
        {
            Print(array);
            countOfPermutations++;
        }
        else
        {
            for (int k = index; k < array.Length; k++)
            {
                Swap(ref array[index], ref array[k]);
                GeneratePermutations(array, index + 1);
                Swap(ref array[index], ref array[k]);
            }
        }
    }
    static void Swap(ref int num1, ref int num2)
    {
        int oldNum1 = num1;
        num1 = num2;
        num2 = oldNum1;
    }
    static void Print(int[] array)
    {
        Console.WriteLine("({0})", string.Join(", ", array));
    }
}

