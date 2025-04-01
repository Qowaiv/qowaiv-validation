namespace Qowaiv.Validation.DataAnnotations;

internal abstract class Annotations(AnnotationChecks checks)
{
	public readonly AnnotationChecks Checks = checks;

	public bool CheckEnumerable => Checks.HasFlag(AnnotationChecks.Enumerable);

	public bool CheckRecursive => Checks.HasFlag(AnnotationChecks.Recursive);

	public bool CheckValidatable => Checks.HasFlag(AnnotationChecks.Validatable);
}
