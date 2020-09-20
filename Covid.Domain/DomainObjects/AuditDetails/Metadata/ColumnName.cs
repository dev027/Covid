// <copyright file="ColumnName.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Covid.Domain.DomainObjects.AuditDetails.Metadata
{
    /// <summary>
    /// Metadata for the <see cref="IAuditDetail.ColumnName"></see> property.
    /// </summary>
    public static class ColumnName
    {
        /// <summary>
        /// The minimum length.
        /// </summary>
        public const int MinLength = 1;

        /// <summary>
        /// The maximum length.
        /// </summary>
        public const int MaxLength = 128;
    }
}