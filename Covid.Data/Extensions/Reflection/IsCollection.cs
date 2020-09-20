// <copyright file="IsCollection.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Reflection;

namespace Covid.Data.Extensions.Reflection
{
    /// <summary>
    /// Reflection Extension methods.
    /// </summary>
    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// Determines whether this instance is a collection.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified instance is collection; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCollection(this PropertyInfo instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            // If it's not IEnumerable then it cannot be a collection
            if (instance.PropertyType.GetInterface(nameof(IEnumerable)) == null)
            {
                return false;
            }

            // Strings are IEnumerable but they don't count as collections
            return instance.PropertyType != typeof(string);
        }
    }
}