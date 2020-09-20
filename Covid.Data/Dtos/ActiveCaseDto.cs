// <copyright file="ActiveCaseDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Covid.Data.DbContexts;
using Covid.Data.Resources;
using Covid.Domain.DomainObjects.ActiveCases;

namespace Covid.Data.Dtos
{
    /// <summary>
    /// Organisation DTO.
    /// </summary>
    [Table(nameof(DataContext.ActiveCases))]
    public class ActiveCaseDto : BaseDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveCaseDto"/> class.
        /// </summary>
        public ActiveCaseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveCaseDto"/> class.
        /// </summary>
        /// <param name="id">Active Case Id.</param>
        /// <param name="locationId">Location Id.</param>
        /// <param name="date">Date.</param>
        /// <param name="caseCount">Case Count.</param>
        public ActiveCaseDto(
            Guid id,
            Guid locationId,
            DateTime date,
            int caseCount)
        {
            this.Id = id;
            this.LocationId = locationId;
            this.Date = date;
            this.CaseCount = caseCount;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Active Case Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        public Guid LocationId { get; private set; }

        /// <summary>
        /// Gets the Date.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the Case Count.
        /// </summary>
        public int CaseCount { get; private set; }

        #endregion Properties

        /// <summary>
        /// Gets the Location.
        /// </summary>
        [ForeignKey(nameof(LocationId))]
        public LocationDto Location { get; private set; } = null!;

        #region Parent Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="activeCase">Active Case.</param>
        /// <returns>Active Case DTO.</returns>
        public static ActiveCaseDto ToDto(IActiveCase activeCase)
        {
            if (activeCase == null)
            {
                throw new ArgumentNullException(nameof(activeCase));
            }

            return new ActiveCaseDto(
                id: activeCase.Id,
                locationId: activeCase.Location.Id,
                date: activeCase.Date,
                caseCount: activeCase.CaseCount);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Active Case.</returns>
        public IActiveCase ToDomain()
        {
            if (this.Location == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(IActiveCase),
                        nameof(this.Location)));
            }

            return new ActiveCase(
                id: this.Id,
                location: this.Location.ToDomain(),
                date: this.Date,
                caseCount: this.CaseCount);
        }

        #endregion
    }
}