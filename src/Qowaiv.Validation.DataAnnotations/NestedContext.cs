using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents a wrapper for a (sealed) <see cref="ValidationContext"/>.</summary>
/// <remarks>
/// This allows to pass through extra context needed for nested scenarios. The
/// use of a struct, and the reuse of the <see cref="ValidationContext"/> is to
/// reduce object creation.
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
internal readonly struct NestedContext
{
    private NestedContext(
        MemberPath path,
        TypeAnnotations? annotations,
        List<IValidationMessage> messages,
        HashSet<object> done,
        ValidationContext @base)
    {
        Path = path;
        Annotations = annotations;
        Messages = messages;
        Done = done;
        Base = @base;
    }

    /// <summary>The Path of the nested context.</summary>
    public readonly MemberPath Path;

    /// <summary>The annotations for nested context.</summary>
    public readonly TypeAnnotations? Annotations;

    /// <summary>The collected messages.</summary>
    public readonly List<IValidationMessage> Messages;

    /// <summary>Keeps track of objects that already have been validated.</summary>
    public readonly HashSet<object> Done;

    /// <summary>The underlying base.</summary>
    private readonly ValidationContext Base;

    /// <inheritdoc cref="ValidationContext.ObjectInstance" />
    public object Instance => Base.ObjectInstance;

    /// <inheritdoc cref="ValidationContext.MemberName" />
    public string? MemberName => Base.MemberName;

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
            Messages.Add(Update(message, violationOnType));
            return true;
        }
        else
        {
            return false;
        }
    }

    [Pure]
    private ValidationMessage Update(ValidationMessage message, bool violationOnType)
    {
        if (Path is { Length: > 0 })
        {
            string[] members = violationOnType && message.PropertyName is not { Length: > 0 }
                ? [Path.Property()]
                : [.. message.MemberNames.Select(Path.Property)];

            return new(message.Severity, message.Message, members);
        }
        else
        {
            return message;
        }
    }

    /// <summary>Creates context for the property.</summary>
    public bool TryProperty(PropertyAnnotations annotations, out object? value)
    {
        Base.MemberName = annotations.Name;
        Base.DisplayName = Base.GetDisplayName();

        try
        {
            value = annotations.GetValue(Instance);
            return true;
        }
        catch
        {
            value = null;
            AddMessage(ValidationMessage.Error($"The value is inaccessible.", annotations.Name));
            return false;
        }
    }

    [Pure]
    public NestedContext Nested(object instance, TypeAnnotations annotations, int index = -1) => new(
        Path.Nested(Base.MemberName!, index),
        annotations,
        Messages,
        Done,
        new(instance, Base, Base.Items));

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Path = '{Path}', MemberName = '{MemberName}'";

    /// <summary>Implicitly casts to the (sealed base) <see cref="ValidationContext"/>.</summary>
    [Pure]
    public static implicit operator ValidationContext(NestedContext context) => context.Base;

    [Pure]
    public static NestedContext Root<TModel>(
        TModel instance,
        IServiceProvider serviceProvider,
        IDictionary<object, object?> items) => new(
            MemberPath.Root,
            Annotator.Annotate(typeof(TModel)),
            [],
            new(ReferenceComparer.Instance),
            new ValidationContext(instance!, serviceProvider, items));
}
