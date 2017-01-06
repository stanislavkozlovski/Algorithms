namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class Quicksorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            int start = 0;
            int end = collection.Count - 1;

            this.QuickSort(collection, start, end);
        }       
        private void QuickSort(List<T> array, int start, int end)
        {
            if (start >= end)
                return;

            T pivot = array[start];
            int storeIndex = start + 1;

            for (int i = storeIndex; i <= end; i++)
            {
                if(array[i].CompareTo(pivot) < 0)
                {
                    // swap
                    T old = array[i];
                    array[i] = array[storeIndex];
                    array[storeIndex] = old;

                    storeIndex++;
                }
            }
            storeIndex--;

            // swap
            T oldT = array[start];
            array[start] = array[storeIndex];
            array[storeIndex] = oldT;

            QuickSort(array, start, storeIndex); // left recursion
            QuickSort(array, storeIndex + 1, end); // right recursion
        }
    }
}
