namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a type.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public sealed class TypeAnnotations
{
    /// <summary>Initializes a new instance of the <see cref="TypeAnnotations"/> class.</summary>
    internal TypeAnnotations(
        IReadOnlyCollection<ValidationAttribute> attributes,
        IReadOnlyCollection<PropertyAnnotations> properties)
    {
        Attributes = attributes;
        Properties = properties;
    }

    /// <summary>Gets the <see cref="ValidationAttribute"/>s defined on the type.</summary>
    public IReadOnlyCollection<ValidationAttribute> Attributes { get; }

    /// <summary>Gets the annotated properties.</summary>
    public IReadOnlyCollection<PropertyAnnotations> Properties { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Attributes: {Attributes.Count}, Properties: {Properties.Count}";
}
