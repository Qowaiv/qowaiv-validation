namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a type.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public sealed class TypeAnnotations
{
    /// <summary>Initializes a new instance of the <see cref="TypeAnnotations"/> class.</summary>
    internal TypeAnnotations(
        IReadOnlyCollection<ValidationAttribute> attributes,
        IReadOnlyCollection<MemberAnnotations> members)
    {
        Attributes = attributes;
        Members = members;
    }

    /// <summary>Gets the <see cref="ValidationAttribute"/>s defined on the type.</summary>
    public IReadOnlyCollection<ValidationAttribute> Attributes { get; }

    /// <summary>Gets the annotated members.</summary>
    public IReadOnlyCollection<MemberAnnotations> Members { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Attributes: {Attributes.Count}, Members: {Members.Count}";
}
