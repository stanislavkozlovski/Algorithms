namespace Sortable_Collection.Tests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sortable_Collection.Contracts;
    using Sortable_Collection.Sorters;

    [TestClass]
    public class SortTests
    {
        private static readonly ISorter<int> TestSorter = new HeapSorter<int>();

        private static readonly Random Random = new Random();

        [TestMethod]
        public void TestSortWithNoElements()
        {
            var emptyCollection = new SortableCollection<int>();
            emptyCollection.Sort(TestSorter);

            CollectionAssert.AreEqual(
                new int[0],
                emptyCollection.ToArray(),
                "Sorting empty collection should have no effect.");
        }

        [TestMethod]
        public void TestSortWithOneElement()
        {
            var collection = new SortableCollection<int>(1);
            collection.Sort(TestSorter);

            CollectionAssert.AreEqual(
                new[] { 1 },
                collection.ToArray(),
                "Sorting collection with single element should have no effect.");
        }

        [TestMethod]
        public void TestSortWithTwoElements()
        {
            var collection = new SortableCollection<int>(1, -5);
            collection.Sort(TestSorter);

            CollectionAssert.AreEqual(
                new[] { -5, 1 },
                collection.ToArray(),
                "Sort method should sort the elements in ascending order.");
        }

        [TestMethod]
        public void TestSortWithMultipleElements()
        {
            var collection = new SortableCollection<int>(3, 44, 38, 5, 47, 15, 36, 26, 27, 2, 46, 4, 19, 50, 48);
            var copy = new[] { 3, 44, 38, 5, 47, 15, 36, 26, 27, 2, 46, 4, 19, 50, 48 };

            collection.Sort(TestSorter);
            Array.Sort(copy);

            CollectionAssert.AreEqual(
                copy,
                collection.ToArray(),
                "Sort method should sort the elements in ascending order.");
        }

        [TestMethod]
        public void TestSortWithMultipleElementsMultipleTimes()
        {
            const int NumberOfAttempts = 10000;
            const int MaxNumberOfElements = 1000;

            for (int i = 0; i < NumberOfAttempts; i++)
            {
                var numberOfElements = Random.Next(0, MaxNumberOfElements + 1);

                List<int> originalElements = new List<int>(MaxNumberOfElements);

                for (int j = 0; j < numberOfElements; j++)
                {
                    originalElements.Add(Random.Next(int.MinValue, int.MaxValue));
                }

                var collection = new SortableCollection<int>(originalElements);

                originalElements.Sort();
                collection.Sort(TestSorter);

                CollectionAssert.AreEqual(
                    originalElements, 
                    collection.ToArray(), 
                    "Sort method should sort the elements in ascending order.");
            }
        }
    }
}