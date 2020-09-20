// <copyright file="IActiveCase.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Covid.Domain.DomainObjects.Locations;

namespace Covid.Domain.DomainObjects.ActiveCases
{
    /// <summary>
    /// Location.
    /// </summary>
    public interface IActiveCase
    {
        /// <summary>
        /// Gets the Active Case Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        ILocation Location { get; }

        /// <summary>
        /// Gets the Date.
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Gets the Case Count.
        /// </summary>
        int CaseCount { get; }
    }
}