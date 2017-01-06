using System;

class GenVariationsNoRep
{
    static int[] free;
    static int[] array;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        array = new int[k];
        free = GenerateFreeArray(n);

        GenerateVars(n);
    }
    static void GenerateVars(int sizeOfSet, int index = 0)
    {
        if (index >= array.Length)
        {
            Print(array);
        }
        else
        {
            for (int i = index; i < sizeOfSet; i++)
            {
                array[index] = free[i];
                Swap(ref free[i], ref free[index]);
                GenerateVars(sizeOfSet, index + 1);
                Swap(ref free[i], ref free[index]);
            }
        }
    }
    static void Swap(ref int first, ref int second)
    {
        int oldFirst = first;
        first = second;
        second = oldFirst;
    }
    static void Print(int[] array)
    {
        Console.WriteLine("({0})", string.Join(", ", array));
    }
    static int[] GenerateFreeArray(int n)
    {
        int[] freeArray = new int[n];

        for (int i = 1; i <= n; i++)
        {
            freeArray[i-1] = i;
        }

        return freeArray;
    }
}
