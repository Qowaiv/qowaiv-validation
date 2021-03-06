﻿using Qowaiv.Validation.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Qowaiv.Validation.DataAnnotations
{
    /// <summary>A validator to validate models based on their data annotations.</summary>
    /// <typeparam name="TModel">
    /// The type of the annotated model to validate.
    /// </typeparam>
    public class AnnotatedModelValidator<TModel> : IValidator<TModel>
    {
        /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
        public AnnotatedModelValidator() : this(null, null) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
        /// <param name="serviceProvider">
        /// The object that implements the System.IServiceProvider interface. This parameter is optional.
        /// </param>
        public AnnotatedModelValidator(IServiceProvider serviceProvider)
            : this(serviceProvider, null) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
        /// <param name="items">
        /// A dictionary of key/value pairs to make available to the service consumers. This parameter is optional.
        /// </param>
        public AnnotatedModelValidator(IDictionary<object, object> items)
            : this(null, items) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
        /// <param name="serviceProvider">
        /// The object that implements the System.IServiceProvider interface. This parameter is optional.
        /// </param>
        /// <param name="items">
        /// A dictionary of key/value pairs to make available to the service consumers. This parameter is optional.
        /// </param>
        public AnnotatedModelValidator(IServiceProvider serviceProvider, IDictionary<object, object> items)
        {
            ServiceProvider = serviceProvider;
            Items = items;
        }

        /// <summary>Gets the <see cref="IServiceProvider"/>.</summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>Gets the <see cref="IServiceProvider"/>.</summary>
        protected IDictionary<object, object> Items { get; }

        /// <summary>Validates the model.</summary>
        /// <returns>
        /// A result including the model and the <see cref="ValidationResult"/>s.
        /// </returns>
        public Result<TModel> Validate(TModel model)
        {
            var context = NestedValidationContext.CreateRoot(model, ServiceProvider, Items);
            ValidateModel(context);

            return Result.For(model, context.Messages);
        }

        private void ValidateModel(NestedValidationContext context)
        {
            // instance has already been validated.
            if (!context.Done.Add(context.Instance))
            {
                return;
            }
            ValidateProperties(context);
            ValidateType(context);
            ValidateValidatableObject(context);
        }

        /// <summary>Gets the results for validating the (annotated )properties.</summary>
        private void ValidateProperties(NestedValidationContext context)
        {
            foreach (var property in context.Annotations.Properties)
            {
                ValidateProperty(property, context);
            }
        }

        /// <summary>Gets the results for validating a single annotated property.</summary>
        /// <remarks>
        /// It creates a sub validation context.
        /// </remarks>
        private void ValidateProperty(AnnotatedProperty property, NestedValidationContext context)
        {
            var model = context.Instance;
            var value = property.GetValue(model);

            var propertyContext = context.ForProperty(property);

            // Only validate the other properties if the required condition was not met.
            if (propertyContext.AddMessage(property.RequiredAttribute.GetValidationMessage(value, propertyContext)))
            {
                return;
            }

            foreach (var attribute in property.ValidationAttributes)
            {
                propertyContext.AddMessage(attribute.GetValidationMessage(value, propertyContext));
            }

            if (value != null)
            {
                if (property.IsEnumerable)
                {
                    var index = 0;
                    foreach (var item in (IEnumerable)value)
                    {
                        var nestedContext = propertyContext.Nested(item, index++);
                        ValidateModel(nestedContext);
                    }
                }
                else if (property.IsNestedModel)
                {
                    var nestedContext = propertyContext.Nested(value);
                    ValidateModel(nestedContext);
                }
                else
                {
                    // Else we can skip further validation.
                }
            }
        }

        /// <summary>Gets the results for validating the attributes declared on the type of the model.</summary>
        private static void ValidateType(NestedValidationContext context)
        {
            context.AddMessages(context.Annotations.TypeAttributes.Select(attr => attr.GetValidationMessage(context.Instance, context)));
        }

        /// <summary>Gets the results for validating <see cref="IValidatableObject.Validate(ValidationContext)"/>.</summary>
        /// <remarks>
        /// If the model is not <see cref="IValidatableObject"/> nothing is done.
        /// </remarks>
        private static void ValidateValidatableObject(NestedValidationContext context)
        {
            if (context.Annotations.IsIValidatableObject)
            {
                context.AddMessages(((IValidatableObject)context.Instance).Validate(context));
            }
        }
    }
}
