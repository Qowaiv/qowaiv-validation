namespace Qowaiv.Validation.TestTools;

/// <summary>Contains extension methods for custom assertions in unit tests.</summary>
[DebuggerNonUserCode]
public static class QowaivFluentAssertions
{
    /// <summary>
    /// Returns an <see cref="ResultAssertions"/> object that can be used to assert the
    /// current <see cref="Result"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions Should(this Result? result, [CallerArgumentExpression(nameof(result))] string? expression = null) => new(result, expression);

    /// <summary>
    /// Returns an <see cref="ResultAssertions{TModel}"/> object that can be used to assert the
    /// current <see cref="Result{TModel}"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions<TModel> Should<TModel>(this Result<TModel> result, [CallerArgumentExpression(nameof(result))] string? expression = null) => new(result, expression);

    /// <summary>Asserts thats object is valid for the specified validator.</summary>
    [Pure]
    public static Result<TModel> ValidateWith<TModel>(this TModel model, IValidator<TModel> validator) where TModel : class
    {
        Guard.NotNull(model, nameof(model));
        Guard.NotNull(validator, nameof(validator));
        return validator.Validate(model);
    }
}
