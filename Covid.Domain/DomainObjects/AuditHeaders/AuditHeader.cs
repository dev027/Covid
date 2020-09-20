// <copyright file="AuditHeader.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Covid.Domain.Constants;
using Covid.Domain.ValidationAttributes;

namespace Covid.Domain.DomainObjects.AuditHeaders
{
    /// <summary>
    /// Audit Header.
    /// </summary>
    /// <seealso cref="IAuditHeader" />
    public class AuditHeader : BaseDomainModel, IAuditHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeader"/> class.
        /// </summary>
        /// <param name="id">Audit Header Id.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="timeStamp">Time Stamp.</param>
        /// <param name="username">Username.</param>
        /// <param name="correlationId">Correlation Id.</param>
        public AuditHeader(
            Guid id,
            EAuditEvent auditEvent,
            DateTime timeStamp,
            string username,
            Guid correlationId)
        {
            this.Id = id;
            this.AuditEvent = auditEvent;
            this.TimeStamp = timeStamp;
            this.Username = username;
            this.CorrelationId = correlationId;

            if (this.GetType() == typeof(AuditHeader))
            {
                Validate(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeader"/> class.
        /// </summary>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="username">Username.</param>
        /// <param name="correlationId">Correlation Id.</param>
        public AuditHeader(
            EAuditEvent auditEvent,
            string username,
            Guid correlationId)
        {
            this.Id = Guid.NewGuid();
            this.AuditEvent = auditEvent;
            this.TimeStamp = DateTime.Now;
            this.Username = username;
            this.CorrelationId = correlationId;

            if (this.GetType() == typeof(AuditHeader))
            {
                Validate(this);
            }
        }

        /// <summary>
        /// Gets Audit Header Id.
        /// </summary>
        [ValidId]
        public Guid Id { get; }

        /// <summary>
        /// Gets the Audit Event.
        /// </summary>
        public EAuditEvent AuditEvent { get; }

        /// <summary>
        /// Gets the Time Stamp.
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the Username.
        /// </summary>
        [Required]
        [StringLength(
            maximumLength: Metadata.Username.MaxLength,
            MinimumLength = Metadata.Username.MinLength)]
        public string Username { get; }

        /// <summary>
        /// Gets the Correlation Id.
        /// </summary>
        [ValidId]
        public Guid CorrelationId { get; }
    }
}