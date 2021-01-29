using FluentValidation.Validators;
using Qowaiv;

namespace FluentValidation
{
    /// <remarks>
    /// To ensure that NotEmpty is validated equally for
    /// <see cref="UnknownValidation.NotEmptyOrUnknown{T, TProperty}(IRuleBuilder{T, TProperty})"/>
    /// and
    /// <see cref="UnknownValidation.NotUnknown{TModel, TProperty}(IRuleBuilder{TModel, TProperty})"/>
    /// the <see cref="NotEmptyValidator"/> is overridden.
    /// </remarks>
    internal sealed class NotEmptyOrUnknownValidator : NotEmptyValidator
    {
        public NotEmptyOrUnknownValidator(object defaultValueForType)
            : base(defaultValueForType) => Do.Nothing();

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return base.IsValid(context)
                && !Equals(Unknown.Value(context.PropertyValue.GetType()), context.PropertyValue);
        }
    }
}
