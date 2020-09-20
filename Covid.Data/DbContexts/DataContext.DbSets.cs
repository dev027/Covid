// <copyright file="DataContext.DbSets.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Covid.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Covid.Data.DbContexts
{
    /// <summary>
    /// Database Context - DB Sets.
    /// </summary>
    /// <seealso cref="DbContext" />
    public partial class DataContext
    {
        /// <summary>
        /// Gets or sets the Audit Headers.
        /// </summary>
        public DbSet<AuditHeaderDto> AuditHeaders { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Audit Details.
        /// </summary>
        public DbSet<AuditDetailDto> AuditDetails { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Locations.
        /// </summary>
        public DbSet<LocationDto> Locations { get; set; } = null!;
    }
}