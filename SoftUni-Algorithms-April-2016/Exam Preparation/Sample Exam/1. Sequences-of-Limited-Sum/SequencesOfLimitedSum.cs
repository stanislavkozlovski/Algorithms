using System;
using System.Text;

public class SequencesOfLimitedSum
{
	static StringBuilder allSequences = new StringBuilder();
	static int[] seq;

	static void Main()
	{
		int maxSum = int.Parse(Console.ReadLine());
        seq = new int[maxSum];
        GenerateSequences(maxSum, 0);
		Console.Write(allSequences);
	}

	static void GenerateSequences(int maxSum, int index)
	{
		for (int num = 1; num <= maxSum; num++)
		{
            seq[index] = num;
			if (maxSum >= 0)
			{
                // Print seq[0...index]
                for (int i = 0; i < index; i++)
                {
                    allSequences.Append(seq[i]);
                    allSequences.Append(" ");
                }
                allSequences.Append(seq[index]);
                allSequences.AppendLine();
            }
			GenerateSequences(maxSum - num, index + 1);
		}
	}
}
