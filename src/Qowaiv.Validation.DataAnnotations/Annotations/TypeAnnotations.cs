namespace Qowaiv.Validation.DataAnnotations;

internal sealed class TypeAnnotations(AnnotationChecks checks, MemberAnnotations[] members) : Annotations(checks)
{
    public readonly MemberAnnotations[] Members = members;

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
