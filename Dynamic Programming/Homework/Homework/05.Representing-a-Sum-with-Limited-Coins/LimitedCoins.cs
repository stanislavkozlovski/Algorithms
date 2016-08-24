using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LimitedCoins
{
    private static int combinationsCount = 0;
    static void Main(string[] args)
    {
        // cant figure out a way to fix this one
        // in arrays like 1,2,2,5,5,10 where you search for 13
        // it'll go to the first 2 in the array, calculate 2+5+5+1 and get 13
        // but after that it'll go to the second 2 in the array and do the exact same calculation and count that
        // I guess you could eliminate such calculation by storing the numbers used in a list of lists, though that's beyond me
        var s = 13;
        var coins = new int[] { 1,2,2,5,5,10};

        GetAllCombinations(coins, s);
        Console.WriteLine(combinationsCount);
    }

    private static void GetAllCombinations(int[] coins, int desiredSum, int sum = 0, int startNum = 0)
    {
        if (sum == desiredSum)
        {
            combinationsCount++;
            return;
        }

        if (sum > desiredSum)
        {
            return;
        }

        for (int i = startNum; i < coins.Length; i++)
        {
            sum += coins[i];

            GetAllCombinations(coins, desiredSum, sum, i + 1);

            sum -= coins[i];          
        }
    }
}

