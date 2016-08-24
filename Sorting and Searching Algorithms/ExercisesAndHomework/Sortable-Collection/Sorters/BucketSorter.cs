namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;

    public class BucketSorter : ISorter<int>
    {
        public double Max { get; set; }

        public void Sort(List<int> collection)
        {
            var buckets = new List<int>[collection.Count];

            // fill buckets
            foreach (var element in collection)
            {
                int bucketIndex = (int)(element / this.Max * collection.Count);

                if(buckets[bucketIndex] == null)
                {
                    buckets[bucketIndex] = new List<int>();
                }

                buckets[bucketIndex].Add(element);
            }
            
            //sort each bucket
            var sorted = new Quicksorter<int>();
            for (int i = 0; i < buckets.Count(); i++)
            {
                if(buckets[i] != null)
                {
                    sorted.Sort(buckets[i]);
                }
            }

            // copy the elements in the collection
            int index = 0;
            foreach (var bucket in buckets)
            {
                if(bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        collection[index] = item;
                        index++;
                    }
                }
            }
        }
    }
}
