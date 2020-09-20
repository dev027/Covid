// <copyright file="ActiveCase.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Covid.Domain.DomainObjects.Locations;
using Covid.Domain.ValidationAttributes;

namespace Covid.Domain.DomainObjects.ActiveCases
{
    /// <summary>
    /// Active Case.
    /// </summary>
    /// <seealso cref="BaseDomainModel" />
    /// <seealso cref="IActiveCase" />
    public class ActiveCase : BaseDomainModel, IActiveCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveCase"/> class.
        /// </summary>
        /// <param name="id">Active Case Id.</param>
        /// <param name="location">Location.</param>
        /// <param name="date">Date.</param>
        /// <param name="caseCount">Case Count.</param>
        public ActiveCase(
            Guid id,
            ILocation location,
            DateTime date,
            int caseCount)
        {
            this.Id = id;
            this.Location = location;
            this.Date = date;
            this.CaseCount = caseCount;

            Validate(this);
        }

        /// <inheritdoc/>
        [ValidId]
        public Guid Id { get; }

        /// <inheritdoc/>
        [Required]
        public ILocation Location { get; }

        /// <inheritdoc />
        public DateTime Date { get; }

        /// <inheritdoc />
        [Range(
            Metadata.CaseCount.MinValue,
            Metadata.CaseCount.MaxValue)]
        public int CaseCount { get; }
    }
}