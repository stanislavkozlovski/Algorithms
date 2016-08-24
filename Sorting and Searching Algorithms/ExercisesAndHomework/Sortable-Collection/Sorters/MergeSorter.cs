namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class MergeSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            var t = new T[collection.Count];
            MergeSort(collection, t, 0, collection.Count - 1);
        }
        private void MergeSort(List<T> array, T[] tempArray, int start, int end)
        {
            if(start < end)
            {
                int mid = (end + start) / 2;

                MergeSort(array, tempArray, start, mid);
                MergeSort(array, tempArray, mid+1, end);

                tempArray = Merge(array, tempArray, start, mid, end);

                //copy from temp to list
                int tempIndex = 0;
                int leftMinIndex = start;

                while(tempIndex <= tempArray.Length && leftMinIndex <= end)
                {
                    array[leftMinIndex++] = tempArray[tempIndex++];
                }
            }
        }
        private T[] Merge(List<T> array, T[] tempArray, int start, int mid, int end)
        {
            int leftMinIndex = start;
            int rightMinIndex = mid + 1;
            int tempIndex = 0;

            while(leftMinIndex <= mid && rightMinIndex <= end)
            {
                if(array[leftMinIndex].CompareTo(array[rightMinIndex]) < 0)
                {
                    tempArray[tempIndex++] = array[leftMinIndex++];
                }
                else if (array[leftMinIndex].CompareTo(array[rightMinIndex]) > 0)
                {
                    tempArray[tempIndex++] = array[rightMinIndex++];
                }
            }

            while(leftMinIndex <= mid)
            {
                tempArray[tempIndex++] = array[leftMinIndex++];
            }
            while(rightMinIndex <= end)
            {
                tempArray[tempIndex++] = array[rightMinIndex++];
            }

            return tempArray;
        }
    }
}
