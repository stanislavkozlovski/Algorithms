namespace Blocks
{
    using System;
    using System.Collections.Generic;

    public class Blocks
    {
        private static readonly HashSet<string> usedCombinations = new HashSet<string>();
        private const int LettersToChoose = 4;
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var results = FindBlocks(n);
            PrintBlocks(results);
        }

        public static HashSet<string> FindBlocks(int n)
        {
            char[] letters = FillLetters(n);
            char[] currentCombination = new char[LettersToChoose];
            bool[] used = new bool[n];
            HashSet<string> results = new HashSet<string>();

            GenerateVariations(letters, currentCombination, used, results);

            return results;
        }

        private static char[] FillLetters(int numberOfLetters)
        {
            char[] letters = new char[numberOfLetters];

            for (int index = 0, i = 65;  i < 65 + numberOfLetters;  i++, index++)
            {
                letters[index] = Convert.ToChar(i);
            }

            return letters;
        }

        private static void GenerateVariations(
            char[] letters, 
            char[] currentCombination, 
            bool[] used, 
            HashSet<string> results, 
            int index = 0)
        {

            if(index >= currentCombination.Length)
            {
                AddResult(currentCombination, results);
            }
            else
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        currentCombination[index] = letters[i];
                        GenerateVariations(letters, currentCombination, used, results, index + 1);
                        used[i] = false;
                    }
                }
            }
        }

        private static void AddResult(char[] result, HashSet<string> results)
        {
            string currentCombination = new string(result);

            if (!usedCombinations.Contains(currentCombination))
            {
                results.Add(currentCombination);

                // Add the combination and all it's rotated variations to the used hashset
                usedCombinations.Add(currentCombination);
                usedCombinations.Add(new string(new[] { result[3], result[0], result[2], result[1] }));
                usedCombinations.Add(new string(new[] { result[2], result[3], result[1], result[0] }));
                usedCombinations.Add(new string(new[] { result[1], result[2], result[0], result[3] }));
            }
        }

        private static void PrintBlocks(HashSet<string> results)
        {
            Console.WriteLine("Number of blocks: {0}", results.Count);
            foreach (var combination in results)
            {
                Console.WriteLine(combination);
            }
        }
    }
}