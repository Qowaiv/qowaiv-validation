using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

[Flags]
internal enum AnnotationChecks
{
    None = 0,
    Members /*.....*/ = 0b00001,
    Attributes /*..*/ = 0b00010,
    Enumerable /*..*/ = 0b00100,
    Validatable /*.*/ = 0b01000,
    Inheritance /*.*/ = 0b10000,

    All /*.........*/ = 0b11111,

    Recursive = Members | Validatable | Inheritance,
}

internal static class AnnotationCheck
{
    [Pure]
    public static AnnotationChecks New(Type type) => type switch
    {
        _ when !type.IsSealed => AnnotationChecks.All ^ AnnotationChecks.Attributes,
        _ when type.IsArray => AnnotationChecks.Enumerable | New(type.GetElementType()!),
        _ when Nullable.GetUnderlyingType(type) is { } underlying => New(underlying),
        _ when type.GetEnumerableType() is { } enumerable => AnnotationChecks.Enumerable | New(enumerable),
        _ when type.ImplementsIValidatableObject() => AnnotationChecks.Validatable,
        _ => AnnotationChecks.None,
    };
}
