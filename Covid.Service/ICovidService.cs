// <copyright file="ICovidService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Covid.Domain.Constants;
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

        #endregion Location
    }
}