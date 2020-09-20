// <copyright file="CaseCount.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Covid.Domain.DomainObjects.ActiveCases.Metadata
{
    /// <summary>
    /// Metadata for the <see cref="IActiveCase.CaseCount"></see> property.
    /// </summary>
    public static class CaseCount
    {
        /// <summary>
        /// The minimum value.
        /// </summary>
        public const int MinValue = 0;

        /// <summary>
        /// The maximum value.
        /// </summary>
        public const int MaxValue = 10000;
    }
}