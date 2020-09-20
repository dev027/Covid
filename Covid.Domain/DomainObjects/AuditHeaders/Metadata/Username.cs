// <copyright file="Username.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Covid.Domain.DomainObjects.AuditHeaders.Metadata
{
    /// <summary>
    /// Metadata for the <see cref="IAuditHeader.Username"></see> property.
    /// </summary>
    public static class Username
    {
        /// <summary>
        /// The minimum length.
        /// </summary>
        public const int MinLength = 1;

        /// <summary>
        /// The maximum length.
        /// </summary>
        public const int MaxLength = 100;
    }
}