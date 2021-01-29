#pragma warning disable S4039 // Interface methods should be callable by derived types
// The whole purpose of this class it so satisfy both interfaces.

using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.Fluent
{
    /// <summary>Base class for an <see cref="IValidator{TModel}"/> using FluentValidation.NET.</summary>
    /// <typeparam name="TModel">
    /// The model to validate for.
    /// </typeparam>
    public class ModelValidator<TModel> : FluentValidation.AbstractValidator<TModel>, IValidator<TModel>
    {
        /// <summary>Initializes a new instance of the <see cref="ModelValidator{TModel}"/> class.</summary>
        protected ModelValidator() => Do.Nothing();

        /// <inheritdoc />
        Result<TModel> IValidator<TModel>.Validate(TModel model)
        {
            var context = new FluentValidation.ValidationContext<TModel>(model);
            var result = Validate(context);
            return Result.For(model, ValidationMessage.For(result.Errors));
        }
    }
}
