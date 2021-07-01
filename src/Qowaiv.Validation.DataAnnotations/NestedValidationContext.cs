using Qowaiv.Validation.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Qowaiv.Validation.DataAnnotations
{
    /// <summary>Represents a nested wrapper for a (sealed) <see cref="ValidationContext"/>.</summary>
    internal class NestedValidationContext : IServiceProvider
    {
        /// <summary>Initializes a new instance of the <see cref="NestedValidationContext"/> class.</summary>
        private NestedValidationContext(string path, object instance, IServiceProvider serviceProvider, IDictionary<object, object> items, ISet<object> done)
        {
            Root = path;
            Instance = instance;
            ServiceProvider = serviceProvider;
            Items = items;
            Annotations = AnnotatedModel.Get(instance.GetType());
            Done = done;
        }

        /// <summary>Keeps track of objects that already have been validated.</summary>
        public ISet<object> Done { get; }

        /// <summary>Gets the (nested) path.</summary>
        public string Root { get; }

        /// <summary>Gets the instance/model.</summary>
        public object Instance { get; }

        /// <summary>Gets the (root) service provider.</summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>Gets the dictionary of key/value pairs that is associated with this context.</summary>
        public IDictionary<object, object> Items { get; }

        /// <summary>Gets the member name.</summary>
        /// <remarks>
        /// Only relevant for testing a property.
        /// </remarks>
        public string MemberName { get; private set; }

        /// <summary>Gets the annotated model for this context.</summary>
        public AnnotatedModel Annotations { get; }

        /// <summary>Gets the enumerable of collected messages.</summary>
        public IReadOnlyCollection<IValidationMessage> Messages { get; private set; } = new List<IValidationMessage>();

        /// <summary>Adds a set of messages.</summary>
        public void AddMessages(IEnumerable<ValidationResult> messages)
        {
            foreach (var message in messages)
            {
                AddMessage(message);
            }
        }

        /// <summary>Adds a message.</summary>
        /// <returns>
        /// True if the message had a severity.
        /// </returns>
        /// <remarks>
        /// Null and <see cref="ValidationMessage.None"/> Messages are not added.
        /// </remarks>
        public bool AddMessage(ValidationResult validationResult)
        {
            var message = ValidationMessage.For(validationResult);

            if (message.Severity <= ValidationSeverity.None) { return false; }
            else
            {
                if (HasNestedPaths(validationResult))
                {
                    ((List<IValidationMessage>)Messages).Add(new ValidationMessage(
                        message.Severity,
                        message.Message,
                        message.MemberNames.Select(name => $"{Root}.{name}").ToArray()));
                }
                return true;
            }
        }

        private bool HasNestedPaths(ValidationResult validationResult)
            => !string.IsNullOrEmpty(Root) && validationResult.MemberNames.Any();

        /// <inheritdoc />
        public object GetService(Type serviceType) => ServiceProvider?.GetService(serviceType);

        /// <summary>Creates context for the property.</summary>
        public NestedValidationContext ForProperty(AnnotatedProperty property)
            => new(Root, Instance, ServiceProvider, Items, Done)
            {
                MemberName = property.Name,
                Messages = Messages,
            };

        /// <summary>Creates a nested context for the property context.</summary>
        /// <param name="value">
        /// The value of the property.
        /// </param>
        /// <param name="index">
        /// The optional index in case of an enumeration.
        /// </param>
        public NestedValidationContext Nested(object value, int? index = null)
            => new(Combine(Root, MemberName, index), value, ServiceProvider, Items, Done)
            {
                Messages = Messages,
            };

        private static string Combine(string root, string path, int? index)
        {
            var combine = string.IsNullOrEmpty(root) ? string.Empty : ".";
            return index.HasValue
                ? $"{root}{combine}{path}[{index}]"
                : $"{root}{combine}{path}";
        }

        /// <summary>Implicitly casts to the (sealed base) <see cref="ValidationContext"/>.</summary>
        public static implicit operator ValidationContext(NestedValidationContext context)
            => context is null
            ? null
            : new ValidationContext(context.Instance, context.ServiceProvider, context.Items)
            {
                MemberName = context.MemberName,
            };

        /// <summary>Creates a root context.</summary>
        public static NestedValidationContext CreateRoot(object instance, IServiceProvider serviceProvider, IDictionary<object, object> items)
            => new NestedValidationContext(
                path: string.Empty,
                Guard.NotNull(instance, nameof(instance)),
                serviceProvider,
                items,
                new HashSet<object>(ReferenceComparer.Instance));
    }
}
