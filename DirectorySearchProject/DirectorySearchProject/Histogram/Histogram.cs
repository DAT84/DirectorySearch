using System.Collections.Generic;

namespace DirectorySearchProject.Histogram
{
    /// <summary>
    /// Representation of a Histogram data structure
    /// </summary>
    /// <typeparam name="T">The type of object that the histogram is keeping track of.</typeparam>
    public class Histogram<T> : SortedDictionary<T, int>
    {
        /// <summary>
        /// Increments an item's count if already present or inserts if not.
        /// </summary>
        /// <param name="item">The item to insert or increment the value of</param>
        public void Increment(T item)
        {
            if (this.ContainsKey(item))
            {
                this[item]++;
            }
            else
            {
                this.Add(item, 1);
            }
        }
    }
}
