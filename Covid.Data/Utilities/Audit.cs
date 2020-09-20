// <copyright file="Audit.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Covid.Data.Dtos;
using Covid.Data.Extensions.Reflection;
using Covid.Domain.DomainObjects.AuditDetails;
using Covid.Domain.DomainObjects.AuditHeaders;

namespace Covid.Data.Utilities
{
    /// <summary>
    /// Audit routines.
    /// </summary>
    internal static class Audit
    {
        /// <summary>
        /// Audits the create.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="newObject">New value.</param>
        /// <param name="recordId">Record Id.</param>
        internal static void AuditCreate(
            IAuditHeaderWithAuditDetails? auditHeader,
            BaseDto newObject,
            Guid recordId)
        {
            if (auditHeader == null)
            {
                return;
            }

            Type type = newObject.GetType();

            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(type);

            foreach (PropertyInfo propertyInfo in type.GetProperties().OrderBy(p => p.MetadataToken))
            {
                if (!PropertyUtilities.
                    IsAuditableColumn(propertyDescriptors, propertyInfo))
                {
                    continue;
                }

                string newValue = propertyInfo.GetValueAsString(newObject);

                PropertyDescriptor propertyDescriptor = propertyDescriptors[propertyInfo.Name];

                TableAttribute? tableAttribute =
                    (TableAttribute?)propertyDescriptor.Attributes[typeof(TableAttribute)];
                string tableName = tableAttribute == null
                    ? type.Name
                    : tableAttribute.Name;

                ColumnAttribute? columnAttribute =
                    (ColumnAttribute?)propertyDescriptor.Attributes[typeof(ColumnAttribute)];
                string columnName = columnAttribute == null
                    ? propertyInfo.Name
                    : columnAttribute.Name;

                IAuditDetail auditDetail = AuditDetail.CreateForCreate(
                    auditHeader: auditHeader,
                    tableName: tableName,
                    columnName: columnName,
                    recordId: recordId,
                    newValue: newValue);

                auditHeader.AuditDetails.Add(auditDetail);
            }
        }

        /// <summary>
        /// Audits the update.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="recordId">Record Id.</param>
        /// <param name="oldObject">Old Value.</param>
        /// <param name="newObject">New Value.</param>
        internal static void AuditUpdate(
            IAuditHeaderWithAuditDetails? auditHeader,
            Guid recordId,
            BaseDto oldObject,
            BaseDto newObject)
        {
            if (auditHeader == null)
            {
                return;
            }

            Type type = newObject.GetType();

            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(type);

            foreach (PropertyInfo propertyInfo in type.GetProperties().OrderBy(p => p.MetadataToken))
            {
                // Skip if not auditable field
                if (!PropertyUtilities.IsAuditableColumn(propertyDescriptors, propertyInfo))
                {
                    continue;
                }

                // Get old and new values
                string oldValue = propertyInfo.GetValueAsString(oldObject);
                string newValue = propertyInfo.GetValueAsString(newObject);

                // Skip if value unchanged
                if (CompareNullable.AreEqual(oldValue, newValue))
                {
                    continue;
                }

                PropertyDescriptor propertyDescriptor = propertyDescriptors[propertyInfo.Name];

                TableAttribute? tableAttribute =
                    (TableAttribute?)propertyDescriptor.Attributes[typeof(TableAttribute)];
                string tableName = tableAttribute == null
                    ? type.Name
                    : tableAttribute.Name;

                ColumnAttribute? columnAttribute =
                    (ColumnAttribute?)propertyDescriptor.Attributes[typeof(ColumnAttribute)];
                string columnName = columnAttribute == null
                    ? propertyInfo.Name
                    : columnAttribute.Name;

                IAuditDetail auditDetail = AuditDetail.CreateForUpdate(
                    auditHeader: auditHeader,
                    tableName: tableName,
                    columnName: columnName,
                    recordId: recordId,
                    oldValue: oldValue,
                    newValue: newValue);

                auditHeader.AuditDetails.Add(auditDetail);
            }
        }
    }
}