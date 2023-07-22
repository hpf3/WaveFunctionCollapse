using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaveFunction.Extensions.Collections
{
    public static class Methods
    {
        public static void ReSortItem<T>(this SortedSet<T> set, T item)
        {
            set.Remove(item);
            set.Add(item);
        }
    }
}