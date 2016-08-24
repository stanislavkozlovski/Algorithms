using System;
using System.Collections.Generic;
using System.Linq;

class DividingPresents
{
    static void Main()
    {
        int[] array = Console.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).OrderBy(x => x).ToArray();
        List<int> alanList = new List<int>();
        List<int> bobList = new List<int>();
        int alanValue = 0;
        int bobValue = 0;

        for (int i = array.Length-1; i >= 0; i--)
        {
            if(alanValue >= bobValue)
            {
                bobValue += array[i];
                bobList.Add(array[i]);
            }
            else
            {
                alanValue += array[i];
                alanList.Add(array[i]);
            }
        }
        int difference = 0;

        while(alanValue > bobValue)
        {
            difference = alanValue - bobValue;

            if (alanList.Min() <= difference)
            {
                bobValue += alanList.Min();

                bobList.Add(alanList.Min());

                alanList.RemoveAt(alanList.IndexOf(alanList.Min()));
            }
            else
                break;
        }

        while(bobValue > alanValue)
        {
            difference = bobValue - alanValue;

            if (bobList.Min() <= difference)
            {
                alanValue += bobList.Min();

                alanList.Add(bobList.Min());

                bobList.RemoveAt(bobList.IndexOf(bobList.Min()));
            }
            else
                break;
        }

        Console.WriteLine("Difference: {0}", difference);
        Console.WriteLine("Alan:{0} Bob:{1}", alanValue, bobValue);
        Console.WriteLine("Alan takes: {0}", string.Join(" ", alanList));
        Console.WriteLine("Bob takes the rest.");
    }
}

