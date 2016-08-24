using System;

class GenerateVariations
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        int[] array = new int[k];

        GenerateVars(array, n);
    }
    static void GenerateVars(int[] array, int sizeOfSet, int index = 0)
    {
        if(index >= array.Length)
        {
            Print(array);
        }
        else
        {
            for (int i = 1; i <= sizeOfSet; i++)
            {
                array[index] = i;
                GenerateVars(array, sizeOfSet, index + 1);
            }
        }
    }
    static void Print(int[] array)
    {
        Console.WriteLine("({0})",string.Join(", ", array));
    }
}

