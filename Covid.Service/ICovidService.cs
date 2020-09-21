// <copyright file="ICovidService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Domain.Constants;
using Covid.Domain.DomainObjects.ActiveCases;
using Covid.Domain.DomainObjects.Locations;
using Covid.Utilities.Models.Whos;

namespace Covid.Service
{
    /// <summary>
    /// Covid Service.
    /// </summary>
    public interface ICovidService
    {
        #region Location

        /// <summary>
        /// Creates the Active Case asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditEvent">Audit event.</param>
        /// <param name="activeCase">Active Case.</param>
        /// <returns>Nothing.</returns>
        Task CreateActiveCaseAsync(
            IWho who,
            EAuditEvent auditEvent,
            IActiveCase activeCase);

        /// <summary>
        /// Gets all active cases for location between dates asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="locationId">Location Id.</param>
        /// <param name="fromDate">From Date.</param>
        /// <param name="toDate">To Date.</param>
        /// <returns>List of Active Cases.</returns>
        Task<IList<IActiveCase>> GetActiveCasesByLocationIdBetweenDatesAsync(
            IWho who,
            Guid locationId,
            DateTime fromDate,
            DateTime toDate);

        #endregion Location

        #region Location

        /// <summary>
        /// Creates the Location asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditEvent">Audit event.</param>
        /// <param name="location">Location.</param>
        /// <returns>Nothing.</returns>
        Task CreateLocationAsync(
            IWho who,
            EAuditEvent auditEvent,
            ILocation location);

        /// <summary>
        /// Gets all locations asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>List of Locations.</returns>
        Task<IList<ILocation>> GetAllLocationsAsync(
            IWho who);

        #endregion Location
    }
}