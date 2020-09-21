// <copyright file="CovidData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Threading.Tasks;
using Covid.Data.DbContexts;
using Covid.Data.Repositories.AuditHeaders;
using Covid.Data.Repositories.Locations;
using Covid.Domain.Constants;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Covid.Data
{
    /// <summary>
    /// Data access layer.
    /// </summary>
    public class CovidData : ICovidData
    {
        private readonly DataContext context;
        private readonly ILogger<CovidData> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CovidData"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="dataContext">Data Context.</param>
        /// <param name="auditHeaderRepository">Audit Header Repository.</param>
        /// <param name="locationRepository">Location Repository.</param>
        public CovidData(
            ILogger<CovidData> logger,
            DataContext dataContext,
            IAuditHeaderRepository auditHeaderRepository,
            ILocationRepository locationRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.AuditHeader = auditHeaderRepository ?? throw new ArgumentNullException(nameof(auditHeaderRepository));
            this.Location = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        /// <inheritdoc />
        public IAuditHeaderRepository AuditHeader { get; }

        /// <inheritdoc />
        public ILocationRepository Location { get; }

        /// <inheritdoc />
        public Task<IAuditHeaderWithAuditDetails> BeginTransactionAsync(
            IWho who,
            EAuditEvent auditEvent)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            return BeginTransactionInternalAsync();

            async Task<IAuditHeaderWithAuditDetails> BeginTransactionInternalAsync()
            {
                this.logger.LogTrace(
                    "ENTRY {Method}(who, auditEvent) {@Who} {@AuditEvent}",
                    nameof(this.BeginTransactionAsync),
                    who,
                    auditEvent);

                await this.context.Database.BeginTransactionAsync()
                    .ConfigureAwait(false);

                AuditHeaderWithAuditDetails auditHeader = new AuditHeaderWithAuditDetails(
                    auditEvent: auditEvent,
                    username: "Guest",
                    correlationId: who.CorrelationId);

                this.logger.LogTrace(
                    "EXIT {Method}(who, auditHeader) {@Who} {@AuditHeader}",
                    nameof(this.BeginTransactionAsync),
                    who,
                    auditHeader);

                return auditHeader;
            }
        }

        /// <inheritdoc />
        public async Task CommitTransactionAsync(
            IWho who,
            IAuditHeaderWithAuditDetails? auditHeader)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, auditHeader) {@Who} {@AuditHeader}",
                nameof(this.CommitTransactionAsync),
                who,
                auditHeader);

            if (auditHeader != null && auditHeader.AuditDetails.Any())
            {
                await this.AuditHeader.CreateAsync(who, auditHeader)
                    .ConfigureAwait(false);
            }

            this.context.Database.CommitTransaction();

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.CommitTransactionAsync),
                who);
        }

        /// <inheritdoc />
        public void RollbackTransaction(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@Who}",
                nameof(this.RollbackTransaction),
                who);

            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            this.context.Database.RollbackTransaction();

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.RollbackTransaction),
                who);
        }
    }
}