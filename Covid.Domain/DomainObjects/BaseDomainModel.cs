// <copyright file="BaseDomainModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Covid.Domain.Exceptions;

namespace Covid.Domain.DomainObjects
{
    /// <summary>
    /// Base model for all models in the Domain project.
    /// </summary>
    public class BaseDomainModel
    {
        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        protected static void Validate(BaseDomainModel model)
        {
            ValidationContext context = new ValidationContext(
                instance: model,
                serviceProvider: null,
                items: null);
            IList<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(
                instance: model,
                validationContext: context,
                validationResults: results,
                validateAllProperties: true);

            if (isValid)
            {
                return;
            }

            IList<ValidationResultException> exceptions = results
                .Select(r => new ValidationResultException(r))
                .ToList();

            if (exceptions.Count == 1)
            {
                throw exceptions[0];
            }

            throw new AggregateException(exceptions);
        }
    }
}