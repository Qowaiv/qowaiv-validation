namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a property.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public sealed class PropertyAnnotations
{
    /// <summary>Initializes a new instance of the <see cref="PropertyAnnotations"/> class.</summary>
    internal PropertyAnnotations(
        string name,
        TypeAnnotations? typeAnnotations,
        RequiredAttribute required,
        IReadOnlyCollection<ValidationAttribute> attributes,
        Func<object, object?> getValue)
    {
        Name = name;
        TypeAnnotations = typeAnnotations;
        Required = required;
        Attributes = attributes;
        Accessor = getValue;
    }

    /// <summary>Gets the name of the property.</summary>
    public string Name { get; }

    /// <summary>Gets <see cref="TypeAnnotations"/> of the property type.</summary>
    public TypeAnnotations? TypeAnnotations { get; }

    /// <summary>Gets the required attribute.</summary>
    /// <remarks>
    /// <see cref="OptionalAttribute"/> if the property is not decorated.
    /// </remarks>
    public RequiredAttribute Required { get; }

    /// <summary>Gets the <see cref="ValidationAttribute"/>s defined on the property.</summary>
    public IReadOnlyCollection<ValidationAttribute> Attributes { get; }

    /// <summary>Gets the value of the property.</summary>
    [Pure]
    public object? GetValue(object model) => Accessor(model);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Func<object, object?> Accessor;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"{Name}, Attributes: {string.Join(", ", GetAll().Select(Shorten))}";

    [Pure]
    private IEnumerable<ValidationAttribute> GetAll() => [Required, .. Attributes];

    [Pure]
    private static string Shorten(Attribute attr) => attr.GetType().Name.Replace("Attribute", string.Empty);
}
