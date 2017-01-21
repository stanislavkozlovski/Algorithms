namespace SetCover
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SetCover
    {
        public static void Main(string[] args)
        {
            var universe = new[] { 1, 3, 5, 7, 9, 11, 20, 30, 40 };
            var sets = new[]
            {
                new[] { 20 },
                new[] { 1, 5, 20, 30 },
                new[] { 3, 7, 20, 30, 40 },
                new[] { 9, 30 },
                new[] { 11, 20, 30, 40 },
                new[] { 3, 7, 40 }
            };

            var selectedSets = ChooseSets(sets.ToList(), universe.ToList());
            Console.WriteLine($"Sets to take ({selectedSets.Count}):");
            foreach (var set in selectedSets)
            {
                Console.WriteLine($"{{ {string.Join(", ", set)} }}");
            }
        }

        public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
        {
            List<int[]> setsTaken = new List<int[]>();
            while(universe.Count > 0)
            {
                int max = 0;
                int index = 0;

                for (int i = 0; i < sets.Count; i++)
                {
                    int tempMax = 0;
                    int tempIndex = 0;
                    // Iterate through the array and find the one with the most identical pieces
                    for (int j = 0; j < sets[i].Length; j++)
                    {
                        if (universe.Contains(sets[i][j]))
                        {
                            tempMax++;
                            tempIndex = i;
                        }
                    }

                    // Save the set we're about to take from this while loop
                    if(tempMax > max)
                    {
                        max = tempMax;
                        index = tempIndex;
                    }
                }

                // Add the set we've taken
                setsTaken.Add(sets[index]);
                foreach (var item in sets[index])
                {
                    // Remove the ones we've taken
                    if (universe.Contains(item))
                        universe.Remove(item);
                }
                sets.RemoveAt(index);
            }

            return setsTaken;
        }
    }
}
