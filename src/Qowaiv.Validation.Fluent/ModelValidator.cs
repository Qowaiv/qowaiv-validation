using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.Fluent
{
    /// <summary>Base class for an <see cref="IValidator{TModel}"/> using FluentValidation.NET.</summary>
    public class ModelValidator<TModel> : FluentValidation.AbstractValidator<TModel>, IValidator<TModel>
    {
        /// <summary>Creates a new instance of a <see cref="ModelValidator{TModel}"/>.</summary>
        protected ModelValidator() { }

        /// <inheritdoc />
        Result<TModel> IValidator<TModel>.Validate(TModel model)
        {
            var context = new FluentValidation.ValidationContext<TModel>(model);
            var result = Validate(context);
            return Result.For(model, ValidationMessage.For(result.Errors));
        }
    }
}
