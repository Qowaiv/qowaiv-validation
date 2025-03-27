namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a member.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
internal readonly struct MemberAnnotations
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

    /// <summary>Gets the <see cref="MemberAnnotations"/> of the <see cref="Type"/>.</summary>
    /// <param name="type">
    /// The type to get the annotations from.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// When the type is null.
    /// </exception>
    /// <returns>
    /// The annotations of the type or null if the type lacks annotations.
    /// </returns>
    [Pure]
    internal static MemberAnnotations[]? Get(Type type)
        => Store.Get(Guard.NotNull(type), []);

    private static readonly AnnotationStore Store = new();

}
