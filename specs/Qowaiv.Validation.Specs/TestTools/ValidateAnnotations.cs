using Qowaiv.Validation.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Specs.TestTools;

public static class AnnotationsValidations
{
    [Pure]
    public static Qowaiv.Validation.Abstractions.Result<TModel> ValidateAnnotations<TModel>(this TModel model)
        where TModel : class
        => AnnotatedModelValidator.Validate(model);
}
