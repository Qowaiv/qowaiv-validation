namespace Qowaiv.Validation.Abstractions;

/// <summary>Static validator helper class.</summary>
public static class Validator
{
    /// <summary>Gets an empty <see cref="IValidator{TModel}"/>.</summary>
    [Pure]
    public static IValidator<TModel> Empty<TModel>()
        where TModel : notnull
        => new EmptyValidator<TModel>();

    /// <summary>Implementation of an empty validator.</summary>
    private sealed class EmptyValidator<TModel> : IValidator<TModel>
        where TModel : notnull
    {
        /// <inheritdoc />
        [Pure]
        public Result<TModel> Validate(TModel model) => Result.For(model);
    }
}
