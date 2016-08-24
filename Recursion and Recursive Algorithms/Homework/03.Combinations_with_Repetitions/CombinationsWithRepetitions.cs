using System;

class CombinationsWithRepetitions
{
    static void Main()
    {
        Console.Write("n = ");
        int n = int.Parse(Console.ReadLine());
        Console.Write("k = ");
        int k = int.Parse(Console.ReadLine());

        int[] array = new int[k];
        Recurse(array, 0, 1, n);
    }
    static void Recurse(int[] array, int index, int startNum, int endNum)
    {
        if (index >= array.Length)
            Console.WriteLine("(" + string.Join(" ", array) + ")");

        else
        {
            for (int i = startNum; i <= endNum; i++)
            {
                array[index] = i;

                Recurse(array, index + 1, i, endNum);
            }
        }
    }
}

