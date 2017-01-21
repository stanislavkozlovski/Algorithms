using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class FractionalKnapsack
{
    static void Main()
    {
        double capacity = 0;
        List<Tuple<double, double>> list = new List<Tuple<double, double>>();
        ReadInput(ref capacity, ref list);
        int index = 0;
        double price = 0;
        list = list.OrderByDescending(x => x.Item1 / x.Item2).ToList();

        while(capacity > 0 && index < list.Count)
        {
            if(capacity - list[index].Item2 >= 0)
            {
                // take 100%
                capacity -= list[index].Item2;
                price += list[index].Item1;
                Console.WriteLine("Take 100% of item with price {0:0.00} and weight {1:0.00}", list[index].Item1, list[index].Item2);
            }
            else
            {
                // take less
                double percentageToTake = capacity / list[index].Item2;
                double weightTaken = percentageToTake * list[index].Item2;
                double priceTaken = percentageToTake * list[index].Item1;
                capacity -= weightTaken;
                price += priceTaken;
                Console.WriteLine("Take {0:0.00}% of item with price {1:0.00} and weight {2:0.00}", percentageToTake * 100, list[index].Item1, list[index].Item2);
            }
            index++;
        }
        Console.WriteLine("Total price: {0:0.00}", price);
    }
    static void ReadInput(ref double capacity, ref List<Tuple<double,double>> list)
    {
        capacity = double.Parse(Regex.Match(Console.ReadLine(), @".+?(\d+)").Groups[1].Value);
        double items = double.Parse(Regex.Match(Console.ReadLine(), @".+?(\d+)").Groups[1].Value);

        for (double i = 0; i < items; i++)
        {
            Match match = Regex.Match(Console.ReadLine(), @"(\d+)\s->\s(\d+)");
            list.Add(new Tuple<double, double>(double.Parse(match.Groups[1].Value), double.Parse(match.Groups[2].Value)));
        }
    }
}

