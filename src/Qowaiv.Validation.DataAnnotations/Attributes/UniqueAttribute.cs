namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that all values are distinct.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Validates(GenericArgument = true)]
public sealed class UniqueAttribute<TValue> : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="UniqueAttribute{TValue}"/> class.</summary>
    public UniqueAttribute() : this(null) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="UniqueAttribute{TValue}"/> class.</summary>
    /// <remarks>
    /// The type of the custom <see cref="IEqualityComparer"/> or <see cref="IEqualityComparer{T}"/> (for <see cref="object"/>).
    /// </remarks>
    public UniqueAttribute(Type? comparer)
        : base(() => QowaivValidationMessages.UniqueValuesAttribute_ValidationError)
    {
        EqualityComparer = CreateComparer(comparer);
    }

    /// <summary>Gets and set a custom <see cref="IEqualityComparer"/>.</summary>
    public IEqualityComparer<TValue> EqualityComparer { get; }

    /// <summary>True if null or all items in the collection are distinct, otherwise false.</summary>
    [Pure]
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }
        else
        {
            var collection = Guard.IsInstanceOf<IEnumerable<TValue>>(value);
            var checker = new HashSet<TValue>(EqualityComparer);
            return collection.All(checker.Add);
        }
    }

    /// <summary>Creates the Comparer to do the distinct with.</summary>
    [Pure]
    private static IEqualityComparer<TValue> CreateComparer(Type? comparer) => comparer switch
    {
        null => EqualityComparer<TValue>.Default,
        _ when typeof(IEqualityComparer<TValue>).IsAssignableFrom(comparer) => (IEqualityComparer<TValue>)Activator.CreateInstance(comparer)!,
        _ => throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, QowaivValidationMessages.ArgumentException_TypeIsNotEqualityComparer, comparer), nameof(comparer)),
    };
}
