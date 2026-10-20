using Qowaiv.Validation.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Specs.TestTools;

internal static class AnnotationsValidations
{
    /// <summary>Helps to test types that have custom should you want to skip.</summary>
    [Pure]
    public static object AsObject<T>(this T obj) where T:  notnull => obj;

    [Pure]
    public static Qowaiv.Validation.Abstractions.Result<TModel> ValidateAnnotations<TModel>(this TModel model)
        where TModel : class
        => AnnotatedModelValidator.Validate(model);
}
