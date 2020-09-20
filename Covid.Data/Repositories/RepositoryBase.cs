// <copyright file="RepositoryBase.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Covid.Utilities.Models.Whos;

namespace Covid.Data.Repositories
{
    /// <summary>
    /// Base class for all repositories.
    /// </summary>
    public abstract class RepositoryBase
    {
        private readonly string repositoryName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="repositoryName">Repository Name.</param>
        protected RepositoryBase(string repositoryName)
        {
            this.repositoryName = repositoryName;
        }

        /// <summary>
        /// Returns tag for use with .TagWith().
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="methodName">Method Name.</param>
        /// <returns>Tag.</returns>
        protected string Tag(IWho who, string methodName)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            string username = "Guest";

            return $"{this.repositoryName}.{methodName}.{who.CorrelationId}.{username}";
        }
    }
}