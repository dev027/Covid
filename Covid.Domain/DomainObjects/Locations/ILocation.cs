// <copyright file="ILocation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Covid.Domain.DomainObjects.Locations
{
    /// <summary>
    /// Location.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the Location Name.
        /// </summary>
        public string Name { get; }
    }
}