using System;
using System.Linq;
using System.Text;

class GroupPermutations
{
    static char[] letters;
    static int[] letterCount = new int['Z' + 1];
    static StringBuilder result = new StringBuilder();

    static void Main()
    {
        string str = Console.ReadLine();

        letters = str.Distinct().ToArray();

        foreach (char letter in str)
        {
            letterCount[letter]++;
        }

        Permute(0);

        Console.Write(result);
    }

    static void Permute(int index)
    {
        if (index == letters.Length)
        {
            Print();
            return;
        }

        for (int i = index; i < letters.Length; i++)
        {
            Swap(i, index);
            Permute(index + 1);
            Swap(i, index);
        }
    }

    static void Swap(int index1, int index2)
    {
        var old = letters[index1];
        letters[index1] = letters[index2];
        letters[index2] = old;
    }

    static void Print()
    {
        foreach (var l in letters)
        {
            for (int i = 0; i < letterCount[l]; i++)
            {
                result.Append(l);
            }
        }
        result.AppendLine();
    }
}
