// <copyright file="CovidService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Data;
using Covid.Domain.Constants;
using Covid.Domain.DomainObjects.ActiveCases;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Domain.DomainObjects.Locations;
using Covid.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Covid.Service
{
    /// <summary>
    /// Covid Service.
    /// </summary>
    public class CovidService : ICovidService
    {
        #region Private Members

        private readonly ILogger<ICovidService> logger;
        private readonly ICovidData data;

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CovidService"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="data">Data layer.</param>
        public CovidService(
            ILogger<ICovidService> logger,
            ICovidData data)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }

        #endregion Constructors

        #region Public Methods

        #region Active Case

        /// <inheritdoc />
        public Task CreateActiveCaseAsync(
            IWho who,
            EAuditEvent auditEvent,
            IActiveCase activeCase)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            if (activeCase == null)
            {
                throw new ArgumentNullException(nameof(activeCase));
            }

            return CreateActiveCaseInternalAsync();

            async Task CreateActiveCaseInternalAsync()
            {
                this.logger.LogTrace(
                    "ENTRY {Method}(who, params) {@Who} {@Params}",
                    nameof(this.CreateActiveCaseAsync),
                    who,
                    new { activeCase });

                try
                {
                    IAuditHeaderWithAuditDetails auditHeader = await this.data.BeginTransactionAsync(
                            who, auditEvent)
                        .ConfigureAwait(false);

                    await this.data.ActiveCase.CreateAsync(
                            who: who,
                            auditHeader: auditHeader,
                            activeCase: activeCase)
                        .ConfigureAwait(false);

                    await this.data.CommitTransactionAsync(who, auditHeader)
                        .ConfigureAwait(false);
                }
                catch (Exception)
                {
                    this.data.RollbackTransaction(who);
                    throw;
                }

                this.logger.LogTrace(
                    "EXIT {Method}(who) {@Who}",
                    nameof(this.CreateActiveCaseAsync),
                    who);
            }
        }

        /// <inheritdoc />
        public Task<IList<IActiveCase>> GetActiveCasesByLocationIdBetweenDatesAsync(
            IWho who,
            Guid locationId,
            DateTime fromDate,
            DateTime toDate)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            return GetActiveCasesByLocationIdBetweenDatesInternalAsync();

            async Task<IList<IActiveCase>> GetActiveCasesByLocationIdBetweenDatesInternalAsync()
            {
                this.logger.LogTrace(
                    "ENTRY {Method}(who, params) {@Who} {@Params}",
                    nameof(this.CreateActiveCaseAsync),
                    who,
                    new { locationId, fromDate, toDate });

                IList<IActiveCase> activeCases = await this.data.ActiveCase
                    .GetByLocationIdBetweenDatesInternalAsync(
                        who: who,
                        locationId: locationId,
                        fromDate: fromDate,
                        toDate: toDate)
                    .ConfigureAwait(false);

                this.logger.LogTrace(
                    "ENTRY {Method}(who, return) {@Who} {@Return}",
                    nameof(this.CreateActiveCaseAsync),
                    who,
                    new { activeCases });

                return activeCases;
            }
        }

        #endregion Active Case

        #region Location

        /// <inheritdoc />
        public Task CreateLocationAsync(
            IWho who,
            EAuditEvent auditEvent,
            ILocation location)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return CreateLocationInternalAsync();

            async Task CreateLocationInternalAsync()
            {
                this.logger.LogTrace(
                    "ENTRY {Method}(who, params) {@Who} {@Params}",
                    nameof(this.CreateLocationAsync),
                    who,
                    new { location });

                try
                {
                    IAuditHeaderWithAuditDetails auditHeader = await this.data.BeginTransactionAsync(
                            who, auditEvent)
                        .ConfigureAwait(false);

                    await this.data.Location.CreateAsync(
                            who: who,
                            auditHeader: auditHeader,
                            location: location)
                        .ConfigureAwait(false);

                    await this.data.CommitTransactionAsync(who, auditHeader)
                        .ConfigureAwait(false);
                }
                catch (Exception)
                {
                    this.data.RollbackTransaction(who);
                    throw;
                }

                this.logger.LogTrace(
                    "EXIT {Method}(who) {@Who}",
                    nameof(this.CreateLocationAsync),
                    who);
            }
        }

        /// <inheritdoc />
        public Task<IList<ILocation>> GetAllLocationsAsync(IWho who)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            return GetAllLocationsInternalAsync();

            async Task<IList<ILocation>> GetAllLocationsInternalAsync()
            {
                this.logger.LogTrace(
                    "ENTRY {Method}(who) {@Who}",
                    nameof(this.GetAllLocationsAsync),
                    who);

                IList<ILocation> locations = await this.data.Location.GetAllAsync(
                        who: who)
                    .ConfigureAwait(false);

                this.logger.LogTrace(
                    "EXIT {Method}(who, return) {@Who} {@Return}",
                    nameof(this.GetAllLocationsAsync),
                    who,
                    new { locations });

                return locations;
            }
        }

        #endregion Location

        #endregion Public Methods
    }
}