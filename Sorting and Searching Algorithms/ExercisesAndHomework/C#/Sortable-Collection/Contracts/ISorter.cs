namespace Sortable_Collection.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ISorter<T> where T : IComparable<T>
    {
        void Sort(List<T> collection);
    }
}
