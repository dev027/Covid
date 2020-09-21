// <copyright file="IActiveCaseRepository.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Domain.DomainObjects.ActiveCases;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Utilities.Models.Whos;

namespace Covid.Data.Repositories.ActiveCases
{
    /// <summary>
    /// Organisation Repository.
    /// </summary>
    public interface IActiveCaseRepository
    {
        #region Create

        /// <summary>
        /// Creates the Organisation.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="activeCase">Active Case.</param>
        /// <returns>Nothing.</returns>
        Task CreateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IActiveCase activeCase);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets all active cases.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>List of Active Cases.</returns>
        Task<IList<IActiveCase>> GetAllAsync(IWho who);

        /// <summary>
        /// Gets the Active Case by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="activeCaseId">Active Case Id.</param>
        /// <returns>Active Case (Null=Not Found).</returns>
        Task<IActiveCase> GetByIdAsync(
            IWho who,
            Guid activeCaseId);

        /// <summary>
        /// Checks if we have Active Cases.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>True if Active Cases exist.</returns>
        Task<bool> HaveAsync(IWho who);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Active Case.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="activeCase">Active Case.</param>
        /// <returns>Nothing.</returns>
        Task UpdateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IActiveCase activeCase);

        #endregion Update

        /// <summary>
        /// Gets the active cases by location id between dates internal asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="locationId">Location id.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>List of Active Cases.</returns>
        Task<IList<IActiveCase>> GetByLocationIdBetweenDatesInternalAsync(
            IWho who,
            Guid locationId,
            DateTime fromDate,
            DateTime toDate);
    }
}