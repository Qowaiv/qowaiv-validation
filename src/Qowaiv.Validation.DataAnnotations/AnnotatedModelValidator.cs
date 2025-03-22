using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>A static validator to validate models based on their data annotations.</summary>

public static class AnnotatedModelValidator
{
    /// <summary>Validates the model.</summary>
    /// <typeparam name="TModel">
    /// The type of the annotated model to validate.
    /// </typeparam>
    /// <param name="model">
    /// The model to validate.
    /// </param>
    /// <returns>
    /// A result including the model and the <see cref="ValidationResult"/>s.
    /// </returns>
    [Pure]
    public static Result<TModel> Validate<TModel>(TModel model) => Validate(model, null, null);

    /// <summary>Validates the model.</summary>
    /// <typeparam name="TModel">
    /// The type of the annotated model to validate.
    /// </typeparam>
    /// <param name="model">
    /// The model to validate.
    /// </param>
    /// <param name="serviceProvider">
    /// The object that implements the System.IServiceProvider interface. This parameter is optional.
    /// </param>
    /// <param name="items">
    /// A dictionary of key/value pairs to make available to the service consumers. This parameter is optional.
    /// </param>
    /// <returns>
    /// A result including the model and the <see cref="ValidationResult"/>s.
    /// </returns>
    [Pure]
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
