using Qowaiv.Validation.Abstractions;
using System.Diagnostics.Contracts;

namespace Qowaiv.Validation.TestTools
{
    /// <summary>Implements <see cref="IValidator{TModel}"/> using <see cref="FluentValidation.IValidator{T}"/>.</summary>
    internal class WrapperValidator<TModel> : IValidator<TModel>
    {
        private readonly FluentValidation.IValidator<TModel> _validator;

        /// <summary>Initializes a new instance of the <see cref="WrapperValidator{TModel}"/> class.</summary>
        public WrapperValidator(FluentValidation.IValidator<TModel> validator) => _validator = validator;

        /// <inheritdoc />
        [Pure]
        public Result<TModel> Validate(TModel model)
        {
            var context = new FluentValidation.ValidationContext<TModel>(model);
            var result = _validator.Validate(context);
            return Result.For(model, Fluent.ValidationMessage.For(result.Errors));
        }
    }
}
