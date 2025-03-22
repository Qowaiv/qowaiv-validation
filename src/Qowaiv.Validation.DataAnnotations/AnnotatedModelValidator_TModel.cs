using Qowaiv.Validation.Abstractions;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>A validator to validate models based on their data annotations.</summary>
/// <typeparam name="TModel">
/// The type of the annotated model to validate.
/// </typeparam>
[Mutable]
public class AnnotatedModelValidator<TModel> : IValidator<TModel>
{
    /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
    public AnnotatedModelValidator() : this(null, null) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
    /// <param name="serviceProvider">
    /// The object that implements the System.IServiceProvider interface. This parameter is optional.
    /// </param>
    public AnnotatedModelValidator(IServiceProvider? serviceProvider)
        : this(serviceProvider, null) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
    /// <param name="items">
    /// A dictionary of key/value pairs to make available to the service consumers. This parameter is optional.
    /// </param>
    public AnnotatedModelValidator(IDictionary<object, object?>? items)
        : this(null, items) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="AnnotatedModelValidator{TModel}"/> class.</summary>
    /// <param name="serviceProvider">
    /// The object that implements the System.IServiceProvider interface. This parameter is optional.
    /// </param>
    /// <param name="items">
    /// A dictionary of key/value pairs to make available to the service consumers. This parameter is optional.
    /// </param>
    public AnnotatedModelValidator(IServiceProvider? serviceProvider, IDictionary<object, object?>? items)
    {
        ServiceProvider = serviceProvider ?? EmptyProvider.Instance;
        Items = items ?? new Dictionary<object, object?>(0);
    }

    /// <summary>Gets the <see cref="IServiceProvider"/>.</summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>Gets the <see cref="IServiceProvider"/>.</summary>
    protected IDictionary<object, object?> Items { get; }

    /// <summary>Validates the model.</summary>
    /// <returns>
    /// A result including the model and the <see cref="ValidationResult"/>s.
    /// </returns>
    [Pure]
    public Result<TModel> Validate(TModel model)
        => AnnotatedModelValidator.Validate(model, ServiceProvider, Items);
}
