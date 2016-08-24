using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroupPermutations
{
	public static void Main()
	{
		string input = Console.ReadLine();
		var letters = input
			.Distinct()
			.ToDictionary(c => c, c => input.Count(symbol => symbol == c));

		Permute(letters, letters.Keys.ToArray());
	}

	static void Permute(Dictionary<char, int> letters, char[] keys, int startIndex = 0)
	{
		if (startIndex >= keys.Length)
		{
			PrintLetters(keys, letters);
		}

		for (int i = startIndex; i < keys.Length; i++)
		{
			Swap(keys, startIndex, i);
			Permute(letters, keys, startIndex + 1);
			Swap(keys, startIndex, i);
		}
	}

	static void Swap(char[] keys, int startIndex, int index)
	{
		char oldStart = keys[startIndex];
		keys[startIndex] = keys[index];
		keys[index] = oldStart;
	}

	static void PrintLetters(char[] keys, Dictionary<char, int> letters)
	{
		var result = new StringBuilder();

		foreach (var key in keys)
		{
			for (int i = 0; i < letters[key]; i++)
			{
				result.Append(key);
			}
		}

		Console.WriteLine(result);
	}
}
