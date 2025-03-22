using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.DataAnnotations;

public static class AnnotatedModelValidator
{
    public static Result<TModel> Validate<TModel>(
        TModel model,
        IServiceProvider? serviceProvider,
        IDictionary<object, object?>? items)
    {
        var context = NestedContext.Root(model!, serviceProvider ?? EmptyProvider.Instance, items ?? new Dictionary<object, object?>(0));
        Validates.Model(context);

        return Result.For(model, context.Messages);
    }
}
