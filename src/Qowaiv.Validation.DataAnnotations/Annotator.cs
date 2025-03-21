namespace Qowaiv.Validation.DataAnnotations;

/// <summary>The factory to resolve <see cref="TypeAnnotations"/>.</summary>
public static class Annotator
{
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
    public static TypeAnnotations? Annotate(Type type)
        => Store.Get(Guard.NotNull(type), []);

    private static readonly AnnotationStore Store = new();
}
