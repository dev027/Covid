// <copyright file="GetValueAsString.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Reflection;

namespace Covid.Data.Extensions.Reflection
{
    /// <summary>
    /// Reflection Extension methods - GetValueAsString.
    /// </summary>
    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// Gets the Property Value from the Object as a string.
        /// </summary>
        /// <param name="propertyInfo">Property Information.</param>
        /// <param name="o">Object.</param>
        /// <returns>Property Value as a sting.</returns>
        public static string GetValueAsString(
            this PropertyInfo propertyInfo,
            object o)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            if (o is null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            MethodInfo getMethod = propertyInfo.GetGetMethod();

            object propertyValue = getMethod.Invoke(o, null);

            return propertyValue == null
                ? string.Empty
                : propertyValue.ToString();
        }
    }
}