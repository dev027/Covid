// <copyright file="CovidService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Covid.Data;
using Covid.Domain.Constants;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Domain.DomainObjects.Locations;
using Covid.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Covid.Service
{
    /// <summary>
    ///  Covid Service.
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
                    "ENTRY {Method}(who, location) {@Who} {@Location}",
                    nameof(this.CreateLocationAsync),
                    who,
                    location);

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

        #endregion Location

        #endregion Public Methods
    }
}