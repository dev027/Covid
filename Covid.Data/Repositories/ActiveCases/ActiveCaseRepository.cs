// <copyright file="ActiveCaseRepository.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Data.DbContexts;
using Covid.Data.Dtos;
using Covid.Data.Utilities;
using Covid.Domain.DomainObjects.ActiveCases;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Covid.Data.Repositories.ActiveCases
{
    /// <summary>
    /// Organisation Header Repository.
    /// </summary>
    public class ActiveCaseRepository : RepositoryBase, IActiveCaseRepository
    {
        private readonly DataContext context;
        private readonly ILogger<ActiveCaseRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveCaseRepository"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="dataContext">Data context.</param>
        public ActiveCaseRepository(
            ILogger<ActiveCaseRepository> logger,
            DataContext dataContext)
            : base(nameof(ActiveCaseRepository))
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        /// <inheritdoc/>
        public async Task CreateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IActiveCase activeCase)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, activeCase) {@Who} {@ActiveCase}",
                nameof(this.CreateAsync),
                who,
                activeCase);

            ActiveCaseDto dto = ActiveCaseDto.ToDto(activeCase);

            this.context.ActiveCases.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.CreateAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task<IList<IActiveCase>> GetAllAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@Who}",
                nameof(this.GetAllAsync),
                who);

            IList<ActiveCaseDto> dtos = await this.context.ActiveCases
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetAllAsync)))
                .ToListAsync()
                .ConfigureAwait(false);

            IList<IActiveCase> activeCases = dtos.Select(ac => ac.ToDomain())
                .ToList();

            this.logger.LogTrace(
                "EXIT {Method}(who, activeCases) {@Who} {@ActiveCases}",
                nameof(this.GetAllAsync),
                who,
                activeCases);

            return activeCases;
        }

        /// <inheritdoc/>
        public async Task<IActiveCase> GetByIdAsync(IWho who, Guid activeCaseId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, activeCaseId) {@Who} {ActiveCaseId}",
                nameof(this.GetByIdAsync),
                who,
                activeCaseId);

            IActiveCase activeCase = (await this.context.ActiveCases
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetByIdAsync)))
                    .SingleAsync(ac => ac.Id == activeCaseId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, activeCase) {@Who} {@ActiveCase}",
                nameof(this.GetByIdAsync),
                who,
                activeCase);

            return activeCase;
        }

        /// <inheritdoc/>
        public async Task<bool> HaveAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@Who}",
                nameof(this.HaveAsync),
                who);

            bool haveActiveCases = await this.context.ActiveCases
                .TagWith(this.Tag(who, nameof(this.HaveAsync)))
                .AnyAsync()
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, haveActiveCases) {@Who} {HaveActiveCases}",
                nameof(this.HaveAsync),
                who,
                haveActiveCases);

            return haveActiveCases;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IActiveCase activeCase)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, activeCase) {@Who} {@ActiveCase}",
                nameof(this.UpdateAsync),
                who,
                activeCase);

            ActiveCaseDto dto = ActiveCaseDto.ToDto(activeCase);
            ActiveCaseDto original = await this.context.FindAsync<ActiveCaseDto>(activeCase.Id)
                .ConfigureAwait(false);
            Audit.AuditUpdate(auditHeader, dto.Id, original, dto);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.UpdateAsync),
                who);
        }
    }
}