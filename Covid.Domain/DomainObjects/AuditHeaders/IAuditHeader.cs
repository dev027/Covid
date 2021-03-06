﻿// <copyright file="IAuditHeader.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Covid.Domain.Constants;

namespace Covid.Domain.DomainObjects.AuditHeaders
{
    /// <summary>
    /// Audit Header.
    /// </summary>
    public interface IAuditHeader
    {
        /// <summary>
        /// Gets Audit Header Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Audit Event.
        /// </summary>
        EAuditEvent AuditEvent { get; }

        /// <summary>
        /// Gets the Time Stamp.
        /// </summary>
        DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the Username.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the Correlation Id.
        /// </summary>
        Guid CorrelationId { get; }
    }
}