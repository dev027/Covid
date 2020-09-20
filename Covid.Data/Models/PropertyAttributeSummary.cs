// <copyright file="PropertyAttributeSummary.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid.Data.Attributes;

namespace Covid.Data.Models
{
    /// <summary>
    /// Class detailing the known property attributes.
    /// </summary>
    public class PropertyAttributeSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAttributeSummary"/> class.
        /// </summary>
        /// <param name="auditIgnore">Audit Ignore attribute.</param>
        /// <param name="foreignKey">Foreign Key attribute.</param>
        /// <param name="key">Key attribute.</param>
        /// <param name="notMapped">Not Mapped attribute.</param>
        /// <param name="range">Range attribute.</param>
        public PropertyAttributeSummary(
            AuditIgnoreAttribute? auditIgnore,
            ForeignKeyAttribute? foreignKey,
            KeyAttribute? key,
            NotMappedAttribute? notMapped,
            RangeAttribute? range)
        {
            this.AuditIgnore = auditIgnore;
            this.ForeignKey = foreignKey;
            this.Key = key;
            this.NotMapped = notMapped;
            this.Range = range;
        }

        /// <summary>
        /// Gets the Audit Ignore attribute.
        /// </summary>
        public AuditIgnoreAttribute? AuditIgnore { get; }

        /// <summary>
        /// Gets the Foreign Key attribute.
        /// </summary>
        public ForeignKeyAttribute? ForeignKey { get; }

        /// <summary>
        /// Gets the key attribute.
        /// </summary>
        public KeyAttribute? Key { get; }

        /// <summary>
        /// Gets the Not Mapped attribute.
        /// </summary>
        public NotMappedAttribute? NotMapped { get; }

        /// <summary>
        /// Gets the range attribute.
        /// </summary>
        public RangeAttribute? Range { get; }
    }
}