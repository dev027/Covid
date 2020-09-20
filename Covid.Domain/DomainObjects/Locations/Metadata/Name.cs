// <copyright file="Name.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Covid.Domain.DomainObjects.Locations.Metadata
{
    /// <summary>
    /// Metadata for the <see cref="ILocation.Name"></see> property.
    /// </summary>
    public static class Name
    {
        /// <summary>
        /// The minimum length.
        /// </summary>
        public const int MinLength = 1;

        /// <summary>
        /// The maximum length.
        /// </summary>
        public const int MaxLength = 20;
    }
}