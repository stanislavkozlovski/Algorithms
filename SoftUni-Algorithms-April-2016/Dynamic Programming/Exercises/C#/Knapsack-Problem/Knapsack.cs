namespace Knapsack_Problem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Knapsack
    {
        public static void Main()
        {
            var items = new[]
            {
                new Item { Weight = 5, Price = 30 },
                new Item { Weight = 8, Price = 120 },
                new Item { Weight = 7, Price = 10 },
                new Item { Weight = 0, Price = 20 },
                new Item { Weight = 4, Price = 50 },
                new Item { Weight = 5, Price = 80 },
                new Item { Weight = 2, Price = 10 }
            };

            var knapsackCapacity = 20;

            var itemsTaken = FillKnapsack(items, knapsackCapacity);

            Console.WriteLine("Knapsack weight capacity: {0}", knapsackCapacity);
            Console.WriteLine("Take the following items in the knapsack:");
            foreach (var item in itemsTaken)
            {
                Console.WriteLine(
                    "  (weight: {0}, price: {1})",
                    item.Weight,
                    item.Price);
            }

            Console.WriteLine("Total weight: {0}", itemsTaken.Sum(i => i.Weight));
            Console.WriteLine("Total price: {0}", itemsTaken.Sum(i => i.Price));
        }

        public static Item[] FillKnapsack(Item[] items, int capacity)
        {
            int[,] maxPrices = new int[items.Length, capacity + 1];
            bool[,] isItemTaken = new bool[items.Length, capacity + 1];

            //Calculate maxPrice[0, 0..capacity]
            for (int c = 0; c <= capacity; c++)
            {
                if(items[0].Weight <= c)
                {
                    maxPrices[0, c] = items[0].Price;
                    isItemTaken[0, c] = true;
                }
            }

            // Calculate maxPrice[1... , 0...]
            for (int row = 1; row < maxPrices.GetLength(0); row++)
            {
                for (int col = 0; col < maxPrices.GetLength(1); col++)
                {
                    maxPrices[row, col] = maxPrices[row - 1, col]; // in case we dont take item
                    int remainingCapacity = col - items[row].Weight;

                    if(remainingCapacity >= 0
                        && items[row].Price + maxPrices[row-1, remainingCapacity] > maxPrices[row, col])
                    {
                        // take item
                        maxPrices[row,col] = items[row].Price + maxPrices[row-1, remainingCapacity];
                        isItemTaken[row, col] = true;
                    }
                }
            }

            List<Item> itemsTaken = new List<Item>();
            int itemIndex = items.Length - 1;

            while(itemIndex >= 0)
            {
                if(isItemTaken[itemIndex, capacity])
                {
                    itemsTaken.Add(items[itemIndex]);
                    capacity -= items[itemIndex].Weight;
                }
                itemIndex--;
            }
            itemsTaken.Reverse();

            return itemsTaken.ToArray();
        }
    }
}
