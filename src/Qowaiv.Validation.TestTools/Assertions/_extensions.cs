using System.Runtime.CompilerServices;

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
    public static ResultAssertions<TModel> Should<TModel>(this Result<TModel> result, [CallerArgumentExpression(nameof(result))] string? expression = null) => new (result, expression);

    /// <summary>Asserts thats object is valid for the specified validator.</summary>
    [Assertion]
    public static ResultValidnessAssertions ShouldBeValidFor<TModel>(this TModel model, IValidator<TModel> validator) where TModel : class
        => Validate(model, validator).Should().BeValid();

    /// <summary>Asserts thats object is invalid for the specified validator.</summary>
    [Assertion]
    public static ResultInvalidnessAssertions ShouldBeInvalidFor<TModel>(this TModel model, IValidator<TModel> validator) where TModel : class
        => Validate(model, validator).Should().BeInvalid();

    [Assertion]
    private static Result Validate<TModel>(TModel model, IValidator<TModel> validator) where TModel : class
    {
        Guard.NotNull(model, nameof(model));
        Guard.NotNull(validator, nameof(validator));
        return validator.Validate(model);
    }
}
