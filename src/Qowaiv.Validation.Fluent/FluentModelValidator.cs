#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods
// Has no purpose without defining rules for TModel.
#pragma warning disable S4039 // Interface methods should be callable by derived types
// Validate should only be visible when used via this interface.
using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.Fluent
{
    /// <summary>Base class for an <see cref="IValidator{TModel}"/> using FluentValidation.NET.</summary>
    public abstract class FluentModelValidator<TModel> : FluentValidation.AbstractValidator<TModel>, IValidator<TModel>
    {
        /// <inheritdoc />
        Result<TModel> IValidator<TModel>.Validate(TModel model)
        {
            var context = new FluentValidation.ValidationContext<TModel>(model);
            var result = Validate(context);
            return Result.For(model, ValidationMessage.For(result.Errors));
        }
    }
}
