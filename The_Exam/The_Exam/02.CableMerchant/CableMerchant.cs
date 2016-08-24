using System;
using System.Linq;

class CableMerchant
{
    static void Main()
    {
        int[] prices = ("0 " + Console.ReadLine()).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int[] bestPrices = new int[prices.Length];

        int connectorPrice = int.Parse(Console.ReadLine());


        for (int i = 1; i < prices.Length; i++) // iterate through every element 
        {
            int maxPrice = prices[i];

            bestPrices[i] = maxPrice;

            for (int j = 1; j < i; j++) // tries to find a better price from the others
            {
                maxPrice = Math.Max(maxPrice, bestPrices[j] + bestPrices[i - j] - 2 * connectorPrice);
            }

            bestPrices[i] = maxPrice;
        }

        Console.WriteLine(string.Join(" ", bestPrices).Substring(2));
    }
}


