using System;

class ReverseArray
{
    static void Main()
    {
        int[] array = { 1, 2, 3, 4, 5, 6 };

        array = ReverseArrray(array, 0, array.Length-1);
        Console.WriteLine(string.Join(" ", array));
    }
    static int[] ReverseArrray(int[] array, int lowIndex, int highIndex) // does recursion in the array like this --><-- until both indexes meet, exchanging values along every step
    {
        
        if (lowIndex >= highIndex)
            return array;

        //exchanges positions
        var temp = array[lowIndex];
        array[lowIndex] = array[highIndex];
        array[highIndex] = temp;

        return ReverseArrray(array, lowIndex + 1, highIndex - 1);
    }
}

