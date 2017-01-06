namespace _04.UnlimitedCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class UnlimtedCoins
    {
        private static int combinationsCount = 0;

        static void Main(string[] args)
        {
            var s = 100;
            var coins = new int[] { 1, 2, 5, 10, 20, 50, 100 };

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
                GetAllCombinations(coins, desiredSum, sum, i);
                sum -= coins[i];
            }
        }
    }
}