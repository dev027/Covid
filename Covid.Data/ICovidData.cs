// <copyright file="ICovidData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Covid.Data.Repositories.ActiveCases;
using Covid.Data.Repositories.AuditHeaders;
using Covid.Data.Repositories.Locations;
using Covid.Domain.Constants;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Utilities.Models.Whos;

namespace Covid.Data
{
    /// <summary>
    /// Data Access Layer - Transactions.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ICovidData
    {
        /// <summary>
        /// Gets the Active Case Repository.
        /// </summary>
        IActiveCaseRepository ActiveCase { get; }

        /// <summary>
        /// Gets the Audit Header Repository.
        /// </summary>
        IAuditHeaderRepository AuditHeader { get; }

        /// <summary>
        /// Gets the Location Repository.
        /// </summary>
        ILocationRepository Location { get; }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <returns>Audit Header.</returns>
        public Task<IAuditHeaderWithAuditDetails> BeginTransactionAsync(
            IWho who,
            EAuditEvent auditEvent);

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <returns>Nothing.</returns>
        public Task CommitTransactionAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader);

        /// <summary>
        /// Rollbacks the Transaction.
        /// </summary>
        /// <param name="who">Who details.</param>
        public void RollbackTransaction(IWho who);
    }
}