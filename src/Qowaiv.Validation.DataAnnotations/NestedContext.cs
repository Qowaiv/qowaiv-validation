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
        TypeAnnotations annotations,
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
    public readonly TypeAnnotations Annotations;

    /// <summary>The collected messages.</summary>
    public readonly List<IValidationMessage> Messages;

    /// <summary>Keeps track of objects that already have been validated.</summary>
    private readonly HashSet<object> Done;

    /// <summary>The underlying base.</summary>
    private readonly ValidationContext Base;

    /// <inheritdoc cref="ValidationContext.ObjectInstance" />
    public object Instance => Base.ObjectInstance;

    /// <inheritdoc cref="ValidationContext.MemberName" />
    public string? MemberName => Base.MemberName;

    [Impure]
    public bool Visited(object value) => Annotations is { } && !Done.Add(value);

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
    [Impure]
    public bool AddMessage(ValidationResult? validationResult)
    {
        if (validationResult is { } && validationResult != ValidationResult.Success)
        {
            var added = false;

            // We want to have a message per member name
            foreach (var member in validationResult.MemberNames.Where(m => m is { Length: > 0 }))
            {
                added = true;

                Messages.Add(validationResult is ValidationMessage m
                   ? ValidationMessage.For(m.Severity, m.Message!, Path.Property(member))!
                   : ValidationMessage.Error(validationResult.ErrorMessage, Path.Property(member)));
            }

            // If no member name has been specified, we add one message with an updated property.
            if (!added)
            {
                Messages.Add(validationResult is ValidationMessage m
                   ? ValidationMessage.For(m.Severity, m.Message!, Path.Property())!
                   : ValidationMessage.Error(validationResult.ErrorMessage, Path.Property()));
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>Creates context for the property.</summary>
    public bool TryMember(MemberAnnotations annotations, out object? value)
    {
        Base.MemberName = annotations.Name;
        Base.DisplayName = annotations.Display?.GetName() ?? annotations.Name;

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
            TypeAnnotations.Get(typeof(TModel))!,
            [],
            new(ReferenceComparer.Instance),
            new ValidationContext(instance!, serviceProvider, items));
}
