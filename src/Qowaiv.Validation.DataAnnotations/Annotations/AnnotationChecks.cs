namespace Qowaiv.Validation.DataAnnotations;

[Flags]
internal enum AnnotationChecks
{
    None = 0,

    /// <summary>Type contains members.</summary>
    Members /*.....*/ = 0b0001,

    /// <summary>Type is enumerable (and its items have annotations).</summary>
    Enumerable /*..*/ = 0b0010,

    /// <summary>Type implements <see cref="IValidatableObject"/>.</summary>
    Validatable /*.*/ = 0b0100,

    /// <summary>Type is not sealed.</summary>
    Inheritance /*.*/ = 0b1000,

    /// <summary>When a type is not sealed, it can result in anything.</summary>
    NotSealed = Inheritance | Members | Enumerable | Validatable,

    /// <summary>Recursion should be applied if any of the flags is checked.</summary>
    Recursive = Members | Enumerable | Validatable,

    /// <summary>
    /// The creation of <see cref="ValidateContext"/> is only needed for types
    /// with members or when implementing <see cref="IValidatableObject"/>.
    /// </summary>
    WithContext = Members | Validatable,
}

internal static class AnnotationCheck
{
    [Pure]
    public static AnnotationChecks New(Type type) => type switch
    {
        _ when !type.IsSealed => AnnotationChecks.NotSealed,
        _ when type.IsArray => New(type.GetElementType()!).And(AnnotationChecks.Enumerable),
        _ when Nullable.GetUnderlyingType(type) is { } underlying => New(underlying),
        _ when type.GetEnumerableType() is { } enumerable => New(enumerable).And(AnnotationChecks.Enumerable),
        _ when type.ImplementsIValidatableObject() => AnnotationChecks.Validatable,
        _ => AnnotationChecks.None,
    };

    [Pure]
    private static AnnotationChecks And(this AnnotationChecks checks, AnnotationChecks and)
        => checks is AnnotationChecks.None
        ? checks
        : checks | and;
}
