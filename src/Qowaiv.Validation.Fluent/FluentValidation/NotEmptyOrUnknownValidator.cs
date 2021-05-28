using FluentValidation.Validators;
using Qowaiv;

namespace FluentValidation
{
    /// <remarks>
    /// To ensure that NotEmpty is validated equally for
    /// <see cref="UnknownValidation.NotEmptyOrUnknown{TModel, TProperty}(IRuleBuilder{TModel, TProperty})"/>
    /// and
    /// <see cref="UnknownValidation.NotUnknown{TModel, TProperty}(IRuleBuilder{TModel, TProperty})"/>
    /// the <see cref="NotEmptyValidator{TModel, TProperty}"/> is overridden.
    /// </remarks>
    internal sealed class NotEmptyOrUnknownValidator<TModel, TProperty> : NotEmptyValidator<TModel, TProperty>
    {
        /// <inheritdoc />
        public override bool IsValid(ValidationContext<TModel> context, TProperty value)
            => base.IsValid(context, value)
                && !Equals(Unknown.Value(typeof(TProperty)), value);
    }
}
