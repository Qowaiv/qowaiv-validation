namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a member.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public sealed class MemberAnnotations
{
    /// <summary>Initializes a new instance of the <see cref="MemberAnnotations"/> class.</summary>
    internal MemberAnnotations(
        string name,
        bool isSealed,
        TypeAnnotations? typeAnnotations,
        DisplayAttribute? display,
        IReadOnlyCollection<ValidationAttribute> attributes,
        Func<object, object?> getValue)
    {
        Name = name;
        IsSealed = isSealed;
        TypeAnnotations = typeAnnotations;
        Display = display;
        Attributes = attributes;
        Accessor = getValue;
    }

    /// <summary>Gets the name of the member.</summary>
    public string Name { get; }

    /// <summary>Indicates that the type of the member is sealed.</summary>
    /// <remarks>
    /// When sealed, the status of the <see cref="TypeAnnotations"/> is final.
    /// </remarks>
    public bool IsSealed { get; }

    /// <summary>Gets <see cref="TypeAnnotations"/> of the member type.</summary>
    public TypeAnnotations? TypeAnnotations { get; }

    /// <summary>Gets the (optional) display attribute.</summary>
    public DisplayAttribute? Display { get; }

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
