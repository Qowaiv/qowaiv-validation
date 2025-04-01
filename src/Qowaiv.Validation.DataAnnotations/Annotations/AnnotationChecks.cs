namespace Qowaiv.Validation.DataAnnotations;

[Flags]
internal enum AnnotationChecks
{
    None = 0,
    Attributes /*..*/ = 0b001,
    Enumerable /*..*/ = 0b010,
    Validatable /*.*/ = 0b100,
    Recursive /*...*/ = Attributes | Enumerable | Validatable,
}
