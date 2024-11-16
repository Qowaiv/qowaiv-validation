using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents a nested wrapper for a (sealed) <see cref="ValidationContext"/>.</summary>
internal sealed class NestedValidationContext
{
    /// <summary>Initializes a new instance of the <see cref="NestedValidationContext"/> class.</summary>
    private NestedValidationContext(
        string root,
        object instance,
        IServiceProvider serviceProvider,
        IDictionary<object, object?> items,
        ISet<object> done,
        List<IValidationMessage> messages)
    {
        Root = root;
        Instance = instance;
        ServiceProvider = serviceProvider;
        Items = items;
        Annotations = AnnotatedModel.Get(instance.GetType());
        Done = done;
        collection = messages;
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
    public IDictionary<object, object?> Items { get; }

    /// <summary>Gets the member name.</summary>
    /// <remarks>
    /// Only relevant for testing a property.
    /// </remarks>
    public string? MemberName { get; private set; }

    /// <summary>Gets the annotated model for this context.</summary>
    public AnnotatedModel Annotations { get; }

    /// <summary>Gets the enumerable of collected messages.</summary>
    public IReadOnlyCollection<IValidationMessage> Messages => collection;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly List<IValidationMessage> collection;

    /// <summary>Adds a set of messages.</summary>
    public void AddMessages(IEnumerable<ValidationResult> messages, bool violationOnType = false)
    {
        foreach (var message in messages)
        {
            AddMessage(message, violationOnType);
        }
    }

    /// <summary>Adds a message.</summary>
    /// <returns>
    /// True if the message had a severity.
    /// </returns>
    /// <remarks>
    /// Null and <see cref="ValidationMessage.None"/> Messages are not added.
    /// </remarks>
    [Impure]
    public bool AddMessage(ValidationResult validationResult, bool violationOnType = false)
    {
        var message = ValidationMessage.For(validationResult);
        if (message.Severity > ValidationSeverity.None)
        {
            collection.Add(Update(message, violationOnType));
            return true;
        }
        else return false;

        ValidationMessage Update(ValidationMessage message, bool violationOnType)
        {
            if (string.IsNullOrEmpty(Root)) return message;
            else
            {
                var members = violationOnType && string.IsNullOrEmpty(message.PropertyName)
                    ? [Root]
                    : message.MemberNames.Select(name => $"{Root}.{name}").ToArray();

                return new ValidationMessage(
                    message.Severity,
                    message.Message,
                    members);
            }
        }
    }

    /// <summary>Creates context for the property.</summary>
    [Pure]
    public NestedValidationContext ForProperty(AnnotatedProperty property)
        => new(Root, Instance, ServiceProvider, Items, Done, collection)
        {
            MemberName = property.Name,
        };

    /// <summary>Creates a nested context for the property context.</summary>
    /// <param name="value">
    /// The value of the property.
    /// </param>
    /// <param name="index">
    /// The optional index in case of an enumeration.
    /// </param>
    [Pure]
    public NestedValidationContext Nested(object value, int? index = null)
        => new(
            root: Combine(Root, MemberName, index),
            instance: value,
            ServiceProvider,
            Items,
            Done,
            collection);

    [Pure]
    private static string Combine(string root, string? path, int? index)
    {
        var combine = string.IsNullOrEmpty(root) ? string.Empty : ".";
        return index.HasValue
            ? $"{root}{combine}{path}[{index}]"
            : $"{root}{combine}{path}";
    }

    /// <summary>Implicitly casts to the (sealed base) <see cref="ValidationContext"/>.</summary>
    [Pure]
    public static implicit operator ValidationContext(NestedValidationContext context)
        => new(context.Instance, context.ServiceProvider, context.Items)
        {
            MemberName = context.MemberName,
        };

    /// <summary>Creates a root context.</summary>
    [Pure]
    public static NestedValidationContext CreateRoot(object instance, IServiceProvider serviceProvider, IDictionary<object, object?> items) => new(
        root: string.Empty,
        Guard.NotNull(instance),
        serviceProvider,
        items,
        new HashSet<object>(ReferenceComparer.Instance),
        []);
}
