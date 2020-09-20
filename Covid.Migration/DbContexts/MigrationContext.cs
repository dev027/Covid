// <copyright file="MigrationContext.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Covid.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Covid.Migration.DbContexts
{
    /// <summary>
    /// Migration Context.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class MigrationContext : DataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationContext"/> class.
        /// </summary>
        public MigrationContext()
            : base(new DbContextOptions<MigrationContext>())
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            const string connectionString = "data source=WRIGHT1\\SQLEXPRESS01;" +
                                            "initial catalog=Covid;" +
                                            "Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}