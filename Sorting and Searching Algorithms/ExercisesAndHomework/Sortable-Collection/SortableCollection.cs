namespace Sortable_Collection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;

    public class SortableCollection<T> where T : IComparable<T>
    {
        static Random _random = new Random();
        public SortableCollection()
        {
        }

        public SortableCollection(IEnumerable<T> items)
        {
            this.Items = new List<T>(items);
        }

        public SortableCollection(params T[] items)
            : this(items.AsEnumerable())
        {
        }

        public List<T> Items { get; } = new List<T>();

        public int Count => this.Items.Count;

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.Items);
        }

        public int BinarySearch(T item)
        {
            return BinarySearchProcedure(item, 0, this.Count-1);
        }
        private int BinarySearchProcedure(T item, int start, int end)
        {
            if (start > end)
                return -1;

            //calculate midpoint
            int mid = start + (end-start)/2;

            if (this.Items[mid].CompareTo(item) < 0) // if item is bigger
            {
                return BinarySearchProcedure(item, mid + 1, end); // look right
            }
            else if (this.Items[mid].CompareTo(item) > 0) // if item is smaller
            {
                return BinarySearchProcedure(item, start, mid-1); // look left
            }


            return mid;
        }

        public int InterpolationSearch(int item)
        {
            /* 2 unit tests don't go through because they're comparing the expected binarySearch 
             * results to the interpolSearch. The interpolSearch DOES return a correct index
             * it's just not the same one that the binarySearch would return 
             * (because of repeating elements and the way each algorithm searches)*/
            if (this.Count == 0)
                return -1;

            int[] array = this.Items.Select(x => int.Parse(x.ToString())).ToArray();
            int low = 0;
            int high = this.Count - 1;

            while(array[low] <= item && array[high] >= item)
            {
                int mid = low + ((item - array[low]) * (high - low)) / (array[high] - array[low]);

                if (array[mid] < item)
                {
                    low = mid + 1;
                }
                else if (array[mid] > item)
                {
                    high = mid - 1;
                }
                else
                    return mid;
            }
            if (array[low] == item)
                return low;
            return -1;
        }

        public void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1
                int r = i + (int)(_random.NextDouble() * (n - i));

                // swaps i with the random index
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public T[] ToArray()
        {
            return this.Items.ToArray();
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", this.Items)}]";
        }        
    }
}