namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Decorates a class so that the <see cref="AnnotatedModelValidator{Tmodel}"/>
/// will also validate its children.
/// </summary>
[Obsolete("Not longer used. All children will be validated.")]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class NestedModelAttribute : Attribute { }
