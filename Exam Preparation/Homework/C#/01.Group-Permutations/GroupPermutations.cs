using System;
using System.Text;
using System.Linq;

class GroupPermutations
{
    static int[] restoreString = new int['Z' + 1]; // holds the number of times a certain char is repeated
    static char[] toPermute;
    static StringBuilder result = new StringBuilder();
    static void Main()
    {
        string input = Console.ReadLine();
        FillArray(input); // we convert the input to an array holding one of each character, and at printing restore each character's length
        toPermute = input.Distinct().ToArray();
        Permute(0);
        Console.Write(result.ToString());
    }
    static void Permute(int index)
    {
        if(index == toPermute.Length)
        {
            Print();
            return;
        }
        else
        {
            for (int i = index; i < toPermute.Length; i++)
            {
                Swap(i, index);
                Permute(index + 1);
                Swap(i, index);
            }
        }
    }
    static void Print()
    {
        for (int i = 0; i < toPermute.Length; i++)
        {
            char currentChar = toPermute[i];
            for (int j = 0; j < restoreString[toPermute[i]]; j++)
            {
                result.Append(currentChar); // apparently I need to append the characters one by one otherwise I go over the memory limit
            }
        }
        result.AppendLine();
    }
    static void Swap(int index1, int index2)
    {
        // Swap the values in the array
        char oldIndex1 = toPermute[index1];
        toPermute[index1] = toPermute[index2];
        toPermute[index2] = oldIndex1;
    }
    static void FillArray(string input)
    {
        // Save the times we repeat a character in an array
        foreach (char letter in input)
        {
            restoreString[letter]++;
        }
    }
}

