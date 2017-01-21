using System;
using System.Collections.Generic;
using System.Linq;

class EgyptianFractions
{
    static void Main()
    {
        double[] input = Console.ReadLine().Split('/').Select(x => double.Parse(x)).ToArray();
        List<string> fractions = new List<string>();
        double fraction = input[0] / input[1];
        double denominator = 2;
        double sum = 0;

        if(fraction < 1.00)
        {
            // Define the tolerance for variation in their values when comparing
            double difference = Math.Abs(fraction * 0.000001);

            while (true)
            {
                double newFraction = 1 / denominator;

                if (sum + newFraction <= fraction)
                {
                    fractions.Add("1/" + denominator.ToString()); // store it in a list for printing
                    sum += newFraction;
                }

                denominator++;

                // checks if we've reached the sum
                if (Math.Abs(fraction - sum) <= difference) // used so numbers like 43.3333333331 can be considered equal to 43.3333333333
                    break;
            }

            Console.WriteLine("{0}/{1} = {2}", input[0], input[1], string.Join(" + ", fractions));
        }
        else
            Console.WriteLine("Error (fraction is equal to or greater than 1)");
    }
}

