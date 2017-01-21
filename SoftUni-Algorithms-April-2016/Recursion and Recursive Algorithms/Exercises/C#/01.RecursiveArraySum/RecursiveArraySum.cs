using System;

class RecursiveArraySum
{
    static void Main()
    {
        int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        int index = 0;

        Console.WriteLine(FindSum(arr, index));
    }
    static int FindSum(int[] array, int index)
    {
        if (index == array.Length - 1)
            return array[index];

        return array[index] + FindSum(array, index + 1);
    }
}

