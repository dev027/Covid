// <copyright file="Location.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Covid.Domain.ValidationAttributes;

namespace Covid.Domain.DomainObjects.Locations
{
    /// <summary>
    /// Location.
    /// </summary>
    /// <seealso cref="ILocation" />
    public class Location : BaseDomainModel, ILocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="id">Location Id.</param>
        /// <param name="name">Location Name.</param>
        public Location(
            Guid id,
            string name)
        {
            this.Id = id;
            this.Name = name;

            Validate(this);
        }

        /// <inheritdoc/>
        [ValidId]
        public Guid Id { get; }

        /// <inheritdoc/>
        [Required]
        [StringLength(
            maximumLength: Metadata.Name.MaxLength,
            MinimumLength = Metadata.Name.MinLength)]
        public string Name { get; }
    }
}