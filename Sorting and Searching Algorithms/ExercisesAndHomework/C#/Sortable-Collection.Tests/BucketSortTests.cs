namespace Sortable_Collection.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sortable_Collection.Sorters;

    [TestClass]
    public class BucketSortTests
    {
        private const int NumberOfTests = 5000;

        private const int MinNumberOfElementsToSort = 1;
        private const int MaxNumberOfElementsToSort = 10000;

        private const int MaxValueCeiling = int.MaxValue;

        private static readonly Random Random = new Random();

        [TestMethod]
        public void TestSortWithMultipleRandomElements()
        {
            for (int i = 0; i < NumberOfTests; i++)
            {
                int numberOfElements = Random.Next(MinNumberOfElementsToSort, MaxNumberOfElementsToSort + 1);
                int maxValue = Random.Next(MaxValueCeiling);

                int[] elements = new int[numberOfElements];

                for (int j = 0; j < numberOfElements; j++)
                {
                    elements[j] = Random.Next(maxValue);
                }

                var collection = new SortableCollection<int>(elements);

                Array.Sort(elements);
                collection.Sort(new BucketSorter { Max = maxValue });

                CollectionAssert.AreEqual(elements, collection.ToArray());
            }
        }
    }
}
