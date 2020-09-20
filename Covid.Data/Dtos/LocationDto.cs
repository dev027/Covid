// <copyright file="LocationDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Covid.Data.DbContexts;
using Covid.Domain.DomainObjects.Locations;

namespace Covid.Data.Dtos
{
    /// <summary>
    /// Organisation DTO.
    /// </summary>
    [Table(nameof(DataContext.Locations))]
    public class LocationDto : BaseDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDto"/> class.
        /// </summary>
        public LocationDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDto"/> class.
        /// </summary>
        /// <param name="id">Location Id.</param>
        /// <param name="name">Location Name.</param>
        public LocationDto(
            Guid id,
            string name)
        {
            this.Id = id;
            this.Name = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Location Name.
        /// </summary>
        [Required]
        [MaxLength(Domain.DomainObjects.Locations.Metadata.Name.MaxLength)]
        public string Name { get; private set; } = null!;

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <returns>Location DTO.</returns>
        public static LocationDto ToDto(ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new LocationDto(
                id: location.Id,
                name: location.Name);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Location.</returns>
        public ILocation ToDomain()
        {
            return new Location(
                id: this.Id,
                name: this.Name);
        }

        #endregion
    }
}