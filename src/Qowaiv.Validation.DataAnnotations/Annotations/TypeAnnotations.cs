namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents annotations of a type.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
internal sealed class TypeAnnotations
{
    /// <summary>Initializes a new instance of the <see cref="TypeAnnotations"/> class.</summary>
    internal TypeAnnotations(
        ValidationAttribute[] attributes,
        MemberAnnotations[] members)
    {
        Attributes = attributes;
        Members = members;
    }

    /// <summary>Gets the <see cref="ValidationAttribute"/>s defined on the type.</summary>
    public ValidationAttribute[] Attributes { get; }

    /// <summary>Gets the annotated members.</summary>
    public MemberAnnotations[] Members { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Attributes: {Attributes.Length}, Members: {Members.Length}";

    /// <summary>Gets the <see cref="TypeAnnotations"/> of the <see cref="Type"/>.</summary>
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
    internal static TypeAnnotations? Get(Type type)
        => Store.Get(Guard.NotNull(type), []);

    private static readonly AnnotationStore Store = new();
}
