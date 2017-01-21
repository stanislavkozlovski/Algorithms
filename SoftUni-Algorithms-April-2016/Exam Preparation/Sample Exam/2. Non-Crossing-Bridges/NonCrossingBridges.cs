using System;
using System.Collections.Generic;
using System.Linq;

public class NonCrossingBridges
{
    public static void Main()
    {
        int[] seq = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int[] bridgesCount = CalcBridgesCount(seq);
        int maxCount = bridgesCount.Max();
        if (maxCount == 0)
        {
            Console.WriteLine("No bridges found");
        }
        else if (maxCount == 1)
        {
            Console.WriteLine("1 bridge found");
        }
        else
        {
            Console.WriteLine($"{maxCount} bridges found");
        }

        string bridges = ReconstructBridges(seq, bridgesCount);
        Console.WriteLine(bridges);
    }

    static int[] CalcBridgesCount(int[] seq)
    {
        int[] bridgeCounts = new int[seq.Length];
        var prev = new Dictionary<int, int>();
        for (int index = 0; index < seq.Length; index++)
        {
            if (index > 0)
            {
                bridgeCounts[index] = bridgeCounts[index - 1];
            }
            int currentNum = seq[index];
            if (prev.ContainsKey(currentNum))
            {
                int prevIndex = prev[currentNum];
                if (bridgeCounts[prevIndex] + 1 > bridgeCounts[index])
                {
                    // Better bridge count ending at position index found
                    bridgeCounts[index] = bridgeCounts[prevIndex] + 1;
                }
            }
            prev[currentNum] = index;
        }

        return bridgeCounts;            
    }

    static string ReconstructBridges(int[] seq, int[] bridgesCount)
    {
        string[] bridges = new string[seq.Length];
        for (int i = 0; i < seq.Length; i++)
        {
            bridges[i] = "X";
        }
        var prev = new Dictionary<int, int>();
        int bridgeNum = 0;
        for (int i = 0; i < seq.Length; i++)
        {
            if (bridgeNum < bridgesCount[i])
            {
                // Bridge found --> connect it
                int bridgeEnd = i;
                int bridgeStart = prev[seq[bridgeEnd]];
                bridges[bridgeStart] = seq[i].ToString();
                bridges[bridgeEnd] = bridges[bridgeStart];
                bridgeNum = bridgesCount[i];
            }
            prev[seq[i]] = i;
        }

        string allBridges = string.Join(" ", bridges);
        return allBridges;
    }
}
