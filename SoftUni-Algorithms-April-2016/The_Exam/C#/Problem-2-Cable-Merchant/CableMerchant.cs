namespace _02CableMerchant
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class CableMerchant
    {
        public static void Main(string[] args)
        {
            List<int> prices = Console.ReadLine().Split().Select(int.Parse).ToList();
            prices.Insert(0,0);
            int connectorPrice = int.Parse(Console.ReadLine());

            List<int> bestPrices = new List<int>();
            bestPrices.Add(0);

            for (int i = 1; i < prices.Count; i++)
            {
                var max = prices[i];
                for (int j = 1; j < i; j++)
                {
                    max = Math.Max(max, bestPrices[j] + bestPrices[i - j] - 2 * connectorPrice);
                }
                bestPrices.Add(max);
            }

            bestPrices.RemoveAt(0);
            Console.WriteLine(string.Join(" ", bestPrices));
        }
    }
}
