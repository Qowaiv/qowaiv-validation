#pragma warning disable CS3001 // Argument type is not CLS-compliant

namespace FluentAssertions;

/// <summary>Contains extension methods for custom assertions in unit tests.</summary>
[DebuggerNonUserCode]
public static class QowaivFluentAssertions
{
    /// <summary>
    /// Returns an <see cref="ResultAssertions"/> object that can be used to assert the
    /// current <see cref="Result"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions Should(this Result? result) => new(result);

    /// <summary>
    /// Returns an <see cref="ResultAssertions{TModel}"/> object that can be used to assert the
    /// current <see cref="Result{TModel}"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions<TModel> Should<TModel>(this Result<TModel> result) => new(result);

    /// <summary>Asserts thats object is valid for the specified validator.</summary>
    [CustomAssertion]
    public static ResultValidnessAssertions BeValidFor<TModel>(this ObjectAssertions assertions, IValidator<TModel> validator)
        => assertions.Validate(validator).Should().BeValid();

    /// <summary>Asserts thats object is invalid for the specified validator.</summary>
    [CustomAssertion]
    public static ResultInvalidnessAssertions BeInvalidFor<TModel>(this ObjectAssertions assertions, IValidator<TModel> validator)
        => assertions.Validate(validator).Should().BeInvalid();

    [CustomAssertion]
    private static Result Validate<TModel>(this ObjectAssertions assertions, IValidator<TModel> validator)
    {
        Guard.NotNull(assertions, nameof(assertions));
        Guard.NotNull(validator, nameof(validator));
        return validator.Validate((TModel)assertions.Subject);
    }

    [Impure]
    internal static IAssertionScope WithDefaultIdentifier(this IAssertionScope scope)
        => scope.WithDefaultIdentifier("Result");
}
