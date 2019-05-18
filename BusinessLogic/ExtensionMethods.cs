// <copyright file="ExtensionMethods.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class contains extension methods for LINQ.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Removes all selected item from collection.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="coll">Collection.</param>
        /// <param name="condition">Condition.</param>
        /// <returns>Collection without the selected items.</returns>
        public static int Remove<T>(
        this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                coll.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }

        /// <summary>
        /// Casts LINQ result to ObservableCollection.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="source">Source.</param>
        /// <returns>Source casted to ObservableCollection.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return new ObservableCollection<T>(source);
        }
    }
}
