using System;
using System.Linq;


class Bridges
{
    static void Main()
    {
        /* Greedy algorithm where we start from 0 and always look backwards for a connection
           we make a connection at the first possibility                                     */


        int[] array = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        string[] printArray = new string[array.Length].Select(x => x = "X").ToArray(); // fill the array that we'll be printing
        int bridgesFound = 0;
        int lastBridgeIndex = 0; // save the last bridge index so we don't look for bridges before it

        for (int index = 0; index < array.Length; index++)
        {
            int currentNumber = array[index];

            for (int j = index-1; j >= lastBridgeIndex; j--) // look back to find a connection
            {
                if(array[j] == currentNumber)
                {
                    lastBridgeIndex = index;

                    printArray[index] = array[index].ToString();
                    printArray[j] = array[j].ToString();

                    bridgesFound++;
                    break; // if we've found a connection there's no need to search for more
                }
            }
        }

        if(bridgesFound == 0)
            Console.WriteLine("No bridges found");
        else if (bridgesFound == 1)
            Console.WriteLine("1 bridge found");
        else
            Console.WriteLine("{0} bridges found", bridgesFound);

        Console.WriteLine(string.Join(" ", printArray));
    }
}

