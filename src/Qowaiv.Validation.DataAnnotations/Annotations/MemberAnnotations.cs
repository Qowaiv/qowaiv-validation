namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a member.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct MemberAnnotations
{
    /// <summary>Initializes a new instance of the <see cref="MemberAnnotations"/> struct.</summary>
    internal MemberAnnotations(
        string name,
        DisplayAttribute? display,
        IReadOnlyCollection<ValidationAttribute> attributes,
        Func<object, object?> getValue)
    {
        Name = name;
        Display = display;
        Attributes = attributes;
        Accessor = getValue;
    }

    /// <summary>Gets the name of the member.</summary>
    public readonly string Name;

    /// <summary>Gets the (optional) display attribute.</summary>
    public readonly DisplayAttribute? Display;

    /// <summary>Gets the <see cref="ValidationAttribute"/>s defined on the member.</summary>
    public IReadOnlyCollection<ValidationAttribute> Attributes { get; }

    /// <summary>Gets the value of the member.</summary>
    [Pure]
    public object? GetValue(object model) => Accessor(model);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Func<object, object?> Accessor;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"{Name}, Attributes: {string.Join(", ", Attributes.Select(Shorten))}";

    [Pure]
    private static string Shorten(Attribute attr) => attr.GetType().Name.Replace("Attribute", string.Empty);
}
