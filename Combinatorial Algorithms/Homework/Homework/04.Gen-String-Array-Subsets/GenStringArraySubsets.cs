using System;

class GenStringArraySubsets // Combinations
{
    public static string[] set;
    public static string[] arr;
    static void Main()
    {
        set = new[] { "test", "rock", "fun" }; // i could use regex to read input and store it in an array but come on, why bother for homework

        int k = 2;
        arr = new string[k]; // array to hold combinations

        Variations(0,0,k);
    }
    static void Variations(int index, int start,int k)
    {
        if(index >= k)
            Console.WriteLine(string.Join(" ", arr));
        else
        {
            for (int i = start; i < set.Length; i++)
            {               
                arr[index] = set[i];
                Variations(index + 1, i + 1, k); // increment start so it doesnt repeat
            }
        }
    }
}

