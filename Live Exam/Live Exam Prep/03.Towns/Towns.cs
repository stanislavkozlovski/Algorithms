using System;

class Towns
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        int[] array = new int[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = int.Parse(Console.ReadLine().Split(' ')[0]);
        }
        Console.WriteLine(Bitonic(array));
    }
    private static int Bitonic(int[] arr)
    {
        // Get theLongest Increasing Subsequence
        var lis = GetLIS(arr);

        // Get the Longest Decreasing Subsequence
        var lds = GetLDS(arr);

        // Get the length of the longest bitonic sequence
        return GetLengthOfMaxLengthBitonicSeq(arr.Length, lis, lds);
    }
    private static int[] GetLIS(int[] arr)
    {
        // Returns a distance array which holds until which index we had the longest subseq
        /* Example:
        Seq: { 9, 10, 3, 1, 2 }
        LIS: { 1, 2, 1, 1, 2 }   */
        int n = arr.Length;
        int[] lis = new int[n];

        for (int i = 0; i < n; i++)
        {
            lis[i] = 1;

            for (int j = 0; j < i; j++)
            {
                if (arr[i] > arr[j] && lis[i] <= lis[j]) // if it's bigger and its max distance isnt bigger
                    lis[i] = lis[j] + 1;
            }
        }

        return lis;
    }
    private static int[] GetLDS(int[] arr)
    {
        // Returns a distance array which holds from which index onwards the LDS is
        /* Example:
        Seq: { 9, 10, 3, 1, 2 }
        LDS: { 3, 3, 2, 1, 1 } Here we hold the max LDS from the right of said index
        the lds from 10 onwards is 3, from 3 onwards is 2*/
        int n = arr.Length;
        int[] lds = new int[n];

        for (int i = n-1; i >= 0; i--)
        {
            lds[i] = 1;

            for (int j = n-1; j >= i; j--)
            {
                if (arr[i] > arr[j] && lds[i] <= lds[j])
                    lds[i] = lds[j] + 1;
            }
        }

        return lds;
    }
    private static int GetLengthOfMaxLengthBitonicSeq(int n, int[] lis, int[] lds)
    {
        /* We get the maximum of both, 
        combining the longest increasing subseq from 0 to lis[x]
        and the longest decreasing subseq from lds[x] to end*/
        int max = lis[0] + lds[0] - 1;

        for (int i = 1; i < n; i++)
        {
            if (lis[i] + lds[i] - 1 > max)
                max = lis[i] + lds[i] - 1;
        }

        return max;
    }
}

