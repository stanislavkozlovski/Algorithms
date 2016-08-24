namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class HeapSorter<T> : ISorter<T> where T : IComparable<T>
    {
        private List<T> heap;
        public int Count { get { return this.heap.Count; } }
        public void Sort(List<T> collection)
        {
            this.heap = new List<T>(collection);
            int heapSize = collection.Count;
            // builds the heap
            for (int i = collection.Count/2; i >= 0; i--)
            {
                HeapifyDown(collection, i, heapSize);
            }

            for (int i = collection.Count-1; i > 0; i--)
            {
                //Swap
                var temp = collection[i];
                collection[i] = collection[0];
                collection[0] = temp;

                heapSize--;
                HeapifyDown(collection, 0, heapSize);
            }
        }
        private void HeapifyDown(List<T> collection, int i, int heapSize)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int largest = i;

            if(left < heapSize &&
                collection[left].CompareTo(collection[largest]) > 0) // if left is not out of range and if its bigger than its parent
            {
                largest = left;
            }
            if (right < heapSize &&
                collection[right].CompareTo(collection[largest]) > 0) // if right is not out of range and if its bigger than parent
            {
                largest = right;
            }
            if(largest != i) // if one of the above ifs got through
            {
                // swaps
                T old = collection[i];

                collection[i] = collection[largest];
                collection[largest] = old;

                HeapifyDown(collection, largest, heapSize); // recursively checks if child follows the heap property
            }
        }
    }
}
