// <copyright file="AuditDetail.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Covid.Domain.Constants;
using Covid.Domain.DomainObjects.AuditHeaders;
using Covid.Domain.ValidationAttributes;

namespace Covid.Domain.DomainObjects.AuditDetails
{
    /// <summary>
    /// Audit Detail.
    /// </summary>
    /// <seealso cref="IAuditDetail" />
    public class AuditDetail : BaseDomainModel, IAuditDetail
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditDetail"/> class.
        /// </summary>
        /// <param name="id">Audit Detail Id.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="tableName">Table Name.</param>
        /// <param name="columnName">Column Name.</param>
        /// <param name="recordId">Record Id.</param>
        /// <param name="oldValue">Old Value.</param>
        /// <param name="newValue">New Value.</param>
        /// <param name="databaseAction">Database Action.</param>
        public AuditDetail(
            Guid id,
            IAuditHeader auditHeader,
            string tableName,
            string columnName,
            Guid recordId,
            string? oldValue,
            string? newValue,
            EDatabaseAction databaseAction)
        {
            this.Id = id;
            this.TableName = tableName;
            this.ColumnName = columnName;
            this.RecordId = recordId;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.DatabaseAction = databaseAction;
            this.AuditHeader = auditHeader;

            Validate(this);
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        [ValidId]
        public Guid Id { get; }

        /// <inheritdoc/>
        [Required]
        [StringLength(
            maximumLength: Metadata.TableName.MaxLength,
            MinimumLength = Metadata.TableName.MinLength)]
        public string TableName { get; }

        /// <inheritdoc/>
        [Required]
        [StringLength(
            maximumLength: Metadata.ColumnName.MaxLength,
            MinimumLength = Metadata.ColumnName.MinLength)]
        public string ColumnName { get; }

        /// <inheritdoc/>
        [ValidId]
        public Guid RecordId { get; }

        /// <inheritdoc/>
        public string? OldValue { get; }

        /// <inheritdoc/>
        public string? NewValue { get; }

        /// <inheritdoc/>
        public EDatabaseAction DatabaseAction { get; }

        #endregion

        #region Parent Properties

        /// <inheritdoc/>
        [Required]
        public IAuditHeader AuditHeader { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Factory method for creating for Create event.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="tableName">Table Name.</param>
        /// <param name="columnName">Column Name.</param>
        /// <param name="recordId">Record Id.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>Audit Detail.</returns>
        public static IAuditDetail CreateForCreate(
            IAuditHeader auditHeader,
            string tableName,
            string columnName,
            Guid recordId,
            string newValue)
        {
            return new AuditDetail(
                id: Guid.NewGuid(),
                auditHeader: auditHeader,
                tableName: tableName,
                columnName: columnName,
                recordId: recordId,
                oldValue: null,
                newValue: newValue,
                databaseAction: EDatabaseAction.Create);
        }

        /// <summary>
        /// Factory method for creating for Update event.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="tableName">Table Name.</param>
        /// <param name="columnName">Column Name.</param>
        /// <param name="recordId">Record Id.</param>
        /// <param name="oldValue">Old Value.</param>
        /// <param name="newValue">New Value.</param>
        /// <returns>Audit Detail.</returns>
        public static IAuditDetail CreateForUpdate(
            IAuditHeader auditHeader,
            string tableName,
            string columnName,
            Guid recordId,
            string oldValue,
            string newValue)
        {
            return new AuditDetail(
                id: Guid.NewGuid(),
                auditHeader: auditHeader,
                tableName: tableName,
                columnName: columnName,
                recordId: recordId,
                oldValue: oldValue,
                newValue: newValue,
                databaseAction: EDatabaseAction.Update);
        }

        /// <summary>
        /// Factory method for creating for Delete event.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="tableName">Table Name.</param>
        /// <param name="recordId">Record Id.</param>
        /// <returns>Audit Detail.</returns>
        public static IAuditDetail CreateForDelete(
            IAuditHeader auditHeader,
            string tableName,
            Guid recordId)
        {
            return new AuditDetail(
                id: Guid.NewGuid(),
                auditHeader: auditHeader,
                tableName: tableName,
                columnName: "Id",
                recordId: recordId,
                oldValue: recordId.ToString(),
                newValue: null,
                databaseAction: EDatabaseAction.Delete);
        }

        #endregion
    }
}