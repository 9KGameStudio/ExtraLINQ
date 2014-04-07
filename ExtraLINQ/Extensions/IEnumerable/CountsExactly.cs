﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified collection contains exactly the specified number of items.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count.</param>
        /// <param name="expectedItemCount">The number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static bool CountsExactly<TSource>(this IEnumerable<TSource> source, int expectedItemCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedItemCount < 0)
            {
                throw new ArgumentOutOfRangeException("expectedItemCount", "The expected item count must not be negative.");
            }

            ICollection collection = source as ICollection;
            if (collection != null)
            {
                return collection.Count == expectedItemCount;
            }

            int itemCount = 0;
            foreach (TSource item in source)
            {
                itemCount++;

                if (itemCount > expectedItemCount)
                {
                    return false;
                }
            }

            return itemCount == expectedItemCount;
        }

        /// <summary>
        /// Determines whether the specified collection contains exactly the specified number of items satisfying the specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying items.</param>
        /// <param name="expectedItemCount">The number of satisfying items the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expectedItemCount"/> is negative.</exception>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        /// </exception>
        public static bool CountsExactly<TSource>(this IEnumerable<TSource> source, int expectedItemCount, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedItemCount < 0)
            {
                throw new ArgumentOutOfRangeException("expectedItemCount", "The expected item count must not be negative.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return source.Count(predicate) == expectedItemCount;
        }
    }
}