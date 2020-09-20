// <copyright file="LocationRepository.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Data.DbContexts;
using Covid.Data.Dtos;
using Covid.Data.Utilities;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Domain.DomainObjects.Locations;
using Covid.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Covid.Data.Repositories.Locations
{
    /// <summary>
    /// Organisation Header Repository.
    /// </summary>
    public class LocationRepository : RepositoryBase, ILocationRepository
    {
        private readonly DataContext context;
        private readonly ILogger<LocationRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRepository"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="dataContext">Data context.</param>
        public LocationRepository(
            ILogger<LocationRepository> logger,
            DataContext dataContext)
            : base(nameof(LocationRepository))
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        /// <inheritdoc/>
        public async Task CreateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocation location)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, location) {@Who} {@Location}",
                nameof(this.CreateAsync),
                who,
                location);

            LocationDto dto = LocationDto.ToDto(location);

            this.context.Locations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.CreateAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task<IList<ILocation>> GetAllAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@Who}",
                nameof(this.GetAllAsync),
                who);

            IList<LocationDto> dtos = await this.context.Locations
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetAllAsync)))
                .ToListAsync()
                .ConfigureAwait(false);

            IList<ILocation> locations = dtos.Select(o => o.ToDomain())
                .ToList();

            this.logger.LogTrace(
                "EXIT {Method}(who, locations) {@Who} {@Locations}",
                nameof(this.GetAllAsync),
                who,
                locations);

            return locations;
        }

        /// <inheritdoc/>
        public async Task<ILocation> GetByIdAsync(IWho who, Guid locationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationId) {@Who} {LocationId}",
                nameof(this.GetByIdAsync),
                who,
                locationId);

            ILocation location = (await this.context.Locations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetByIdAsync)))
                    .SingleAsync(l => l.Id == locationId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, location) {@Who} {@Location}",
                nameof(this.GetByIdAsync),
                who,
                location);

            return location;
        }

        /// <inheritdoc/>
        public async Task<bool> HaveAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@Who}",
                nameof(this.HaveAsync),
                who);

            bool haveLocations = await this.context.Locations
                .TagWith(this.Tag(who, nameof(this.HaveAsync)))
                .AnyAsync()
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, haveLocations) {@Who} {HvaveLocations}",
                nameof(this.HaveAsync),
                who,
                haveLocations);

            return haveLocations;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocation location)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, location) {@Who} {@Location}",
                nameof(this.UpdateAsync),
                who,
                location);

            LocationDto dto = LocationDto.ToDto(location);
            LocationDto original = await this.context.FindAsync<LocationDto>(location.Id)
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