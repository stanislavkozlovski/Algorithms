using System;
using System.Collections.Generic;
using System.Linq;

class Guitar
{
    static void Main()
    {
        /* Here we branch out and take all the possible values we can make. We save the newest values in
        HashSet to then continue coming up with all the possible values

        Example: Array is 2 1 3, pretend we don't have startingDB
        2 + 1 = 3 2-1 = 1 | Iterate through the possible calculations, we don't know which to do so we do both. 
        Save (3) and (1) in the list. Now go to index 2 and calculate all the possible values with '3'
        3 - 3 = 0   3 + 3 = 6 
        3 - 1 = 2   3 + 1 = 4 | The answer is 6. If the array went on we would have 0,2,4,6 in the list and calcualte further      
        */
        
        int[] array = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int startingDB = int.Parse(Console.ReadLine());
        int maxDB = int.Parse(Console.ReadLine());

        HashSet<int> maximumResults = new HashSet<int>();
        HashSet<int> temporaryResults = new HashSet<int>();
        int lastDB = startingDB;
        int currentDB = array[0];

        if (currentDB + lastDB <= maxDB)
            maximumResults.Add((currentDB + lastDB));

        if (lastDB - currentDB >= 0)
            maximumResults.Add((lastDB - currentDB));

        for (int i = 1; i < array.Length; i++)
        {
            currentDB = array[i];

            foreach (var result in maximumResults) // goes through all current results and saves the new ones in temporaryResults
            {
                int subtractionResult = (result - currentDB);
                int sumResult = (currentDB + result);

                if (subtractionResult >= 0)
                    temporaryResults.Add(subtractionResult);

                if (sumResult <= maxDB)
                    temporaryResults.Add(sumResult);
            }

            // We save the newest results and remove the old ones
            maximumResults = new HashSet<int>(temporaryResults); // reset it to make sure we only have the newest results, we can't use the old ones
            temporaryResults.Clear(); // reset it       
        }

        if (maximumResults.Any())
            Console.WriteLine(maximumResults.Max());
        else
            Console.WriteLine(-1);
    }
}

