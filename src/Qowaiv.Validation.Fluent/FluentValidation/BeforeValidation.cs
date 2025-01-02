using FluentValidation.Internal;
using System.Linq.Expressions;

namespace FluentValidation;

/// <summary>Validates if a <see cref="IComparable"/> is before a specific value.</summary>
/// <remarks>
/// This syntactic sugar to:
/// <code>
/// ruleBuilder.GreaterThanOrEqualTo(valueToCompare).WithMessage().
/// </code>
/// </remarks>
public static class BeforeValidation
{
    /// <summary>
    /// Defines a 'before' validator on the current rule builder.
    /// The validation will succeed if the property value is less than the specified value.
    /// The validation will fail if the property value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of the model being validated.</typeparam>
    /// <typeparam name="TProperty">Type of the property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="valueToCompare">The value being compared.</param>
    [FluentSyntax]
    public static IRuleBuilderOptions<T, TProperty> Before<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable

        => ruleBuilder.LessThan(valueToCompare).WithMessage(QowaivValidationFluentMessages.Before);

    /// <summary>
    /// Defines a 'before' validator on the current rule builder.
    /// The validation will succeed if the property value is less than the specified value.
    /// The validation will fail if the property value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of the model being validated.</typeparam>
    /// <typeparam name="TProperty">Type of the property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="valueToCompare">The value being compared.</param>
    [FluentSyntax]
    public static IRuleBuilderOptions<T, TProperty?> Before<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable

        => ruleBuilder.LessThan(valueToCompare).WithMessage(QowaivValidationFluentMessages.Before);

    /// <summary>
    /// Defines a 'before' validator on the current rule builder using a lambda expression.
    /// The validation will succeed if the property value is less than the specified value.
    /// The validation will fail if the property value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of the model being validated.</typeparam>
    /// <typeparam name="TProperty">Type of the property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="expression">A lambda that should return the value being compared.</param>
    [FluentSyntax]
    public static IRuleBuilderOptions<T, TProperty> Before<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, TProperty>> expression)
        where TProperty : IComparable<TProperty>, IComparable

        => ruleBuilder.LessThan(expression).WithMessage(QowaivValidationFluentMessages.Before);

    /// <summary>
    /// Defines a 'before' validator on the current rule builder using a lambda expression.
    /// The validation will succeed if the property value is less than the specified value.
    /// The validation will fail if the property value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of the model being validated.</typeparam>
    /// <typeparam name="TProperty">Type of the property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="expression">A lambda that should return the value being compared.</param>
    [FluentSyntax]
    public static IRuleBuilderOptions<T, TProperty> Before<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, TProperty?>> expression)
        where TProperty : struct, IComparable<TProperty>, IComparable

        => ruleBuilder.LessThan(expression).WithMessage(QowaivValidationFluentMessages.Before);

    /// <summary>
    /// Defines a 'before' validator on the current rule builder using a lambda expression.
    /// The validation will succeed if the property value is less than the specified value.
    /// The validation will fail if the property value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of the model being validated.</typeparam>
    /// <typeparam name="TProperty">Type of the property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="expression">A lambda that should return the value being compared.</param>
    [FluentSyntax]
    public static IRuleBuilderOptions<T, TProperty?> Before<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, Expression<Func<T, TProperty>> expression)
        where TProperty : struct, IComparable<TProperty>, IComparable

        => ruleBuilder.LessThan(expression).WithMessage(QowaivValidationFluentMessages.Before);

    /// <summary>
    /// Defines a 'before' validator on the current rule builder using a lambda expression.
    /// The validation will succeed if the property value is less than the specified value.
    /// The validation will fail if the property value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of the model being validated.</typeparam>
    /// <typeparam name="TProperty">Type of the property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="expression">A lambda that should return the value being compared.</param>
    [FluentSyntax]
    public static IRuleBuilderOptions<T, TProperty?> Before<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, Expression<Func<T, TProperty?>> expression)
        where TProperty : struct, IComparable<TProperty>, IComparable

        => ruleBuilder.LessThan(expression).WithMessage(QowaivValidationFluentMessages.Before);
}
