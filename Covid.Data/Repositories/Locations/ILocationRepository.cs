// <copyright file="ILocationRepository.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Domain.DomainObjects.Locations;
using Covid.Utilities.Models.Whos;

namespace Covid.Data.Repositories.Locations
{
    /// <summary>
    /// Organisation Repository.
    /// </summary>
    public interface ILocationRepository
    {
        #region Create

        /// <summary>
        /// Creates the Location.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="location">Location.</param>
        /// <returns>Nothing.</returns>
        Task CreateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocation location);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets all locations.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>List of Locations.</returns>
        Task<IList<ILocation>> GetAllAsync(IWho who);

        /// <summary>
        /// Gets the Location by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="locationId">Location Id.</param>
        /// <returns>Organisation (Null=Not Found).</returns>
        Task<ILocation> GetByIdAsync(
            IWho who,
            Guid locationId);

        /// <summary>
        /// Checks if we have Locations.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>True if Locations exist.</returns>
        Task<bool> HaveAsync(IWho who);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Location.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="location">Location.</param>
        /// <returns>Nothing.</returns>
        Task UpdateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocation location);

        #endregion Update
    }
}