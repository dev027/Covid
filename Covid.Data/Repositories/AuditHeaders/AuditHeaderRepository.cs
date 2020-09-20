// <copyright file="AuditHeaderRepository.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Covid.Data.DbContexts;
using Covid.Data.Dtos;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Covid.Data.Repositories.AuditHeaders
{
    /// <summary>
    /// Audit Header Repository.
    /// </summary>
    public class AuditHeaderRepository : IAuditHeaderRepository
    {
        private readonly DataContext context;
        private readonly ILogger<AuditHeaderRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderRepository"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="dataContext">Data context.</param>
        public AuditHeaderRepository(
            ILogger<AuditHeaderRepository> logger,
            DataContext dataContext)
        {
            this.logger = logger;
            this.context = dataContext;
        }

        /// <inheritdoc />
        public async Task CreateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, auditHeader) {@Who} {@AuditHeader}",
                nameof(this.CreateAsync),
                who,
                auditHeader);

            AuditHeaderDto auditHeaderDto =
                AuditHeaderDto.ToDtoWithAuditDetails(auditHeader);

            this.context.AuditHeaders.Add(auditHeaderDto);
            await this.context.SaveChangesAsync()
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.CreateAsync),
                who);
        }
    }
}