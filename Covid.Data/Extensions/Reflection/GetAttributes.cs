// <copyright file="GetAttributes.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid.Data.Attributes;
using Covid.Data.Models;

namespace Covid.Data.Extensions.Reflection
{
    /// <summary>
    /// Reflection Extension methods.
    /// </summary>
    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="instance">The Instance.</param>
        /// <returns>Property Attributes.</returns>
        public static PropertyAttributeSummary GetAttributes(this PropertyDescriptor instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return new PropertyAttributeSummary(
                (AuditIgnoreAttribute?)instance.Attributes[typeof(AuditIgnoreAttribute)],
                (ForeignKeyAttribute?)instance.Attributes[typeof(ForeignKeyAttribute)],
                (KeyAttribute?)instance.Attributes[typeof(KeyAttribute)],
                (NotMappedAttribute?)instance.Attributes[typeof(NotMappedAttribute)],
                (RangeAttribute?)instance.Attributes[typeof(RangeAttribute)]);
        }
    }
}