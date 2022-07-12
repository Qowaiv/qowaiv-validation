namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Decorates a class so that the <see cref="AnnotatedModelValidator{Tmodel}"/>
/// will also validate its children.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class NestedModelAttribute : ValidationAttribute 
{
    /// <summary>Returns true, as this is a decoration attribute only.</summary>
    [Pure]
    public override bool IsValid(object? value) => true;
}
