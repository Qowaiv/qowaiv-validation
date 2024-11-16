namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Specifies that all values are distinct.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
[Obsolete("Use UniqueAttribute<T> isntead.")]
public sealed class DistinctValuesAttribute : ValidationAttribute
{
    /// <summary>Initializes a new instance of the <see cref="DistinctValuesAttribute"/> class.</summary>
    public DistinctValuesAttribute() : this(null) => Do.Nothing();

    /// <summary>Initializes a new instance of the <see cref="DistinctValuesAttribute"/> class.</summary>
    /// <remarks>
    /// The type of the custom <see cref="IEqualityComparer"/> or <see cref="IEqualityComparer{T}"/> (for <see cref="object"/>).
    /// </remarks>
    public DistinctValuesAttribute(Type? comparer)
        : base(() => QowaivValidationMessages.DistinctValuesAttribute_ValidationError)
    {
        EqualityComparer = CreateComparer(comparer);
    }

    /// <summary>Gets and set a custom <see cref="IEqualityComparer"/>.</summary>
    public IEqualityComparer<object> EqualityComparer { get; }

    /// <summary>True if all items in the collection are distinct, otherwise false.</summary>
    [Pure]
    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        else
        {
            var collection = Guard.IsInstanceOf<IEnumerable>(value).Cast<object>();
            var checker = new HashSet<object>(EqualityComparer);
            return collection.All(checker.Add);
        }
    }

    /// <summary>Creates the Comparer to do the distinct with.</summary>
    [Pure]
    private static IEqualityComparer<object> CreateComparer(Type? comparer)
    {
        if (comparer is null)
        {
            return EqualityComparer<object>.Default;
        }
        else if (typeof(IEqualityComparer<object>).IsAssignableFrom(comparer))
        {
            return (IEqualityComparer<object>)Activator.CreateInstance(comparer)!;
        }
        else if (typeof(IEqualityComparer).IsAssignableFrom(comparer))
        {
            return new WrappedComparer((IEqualityComparer)Activator.CreateInstance(comparer)!);
        }
        else throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, QowaivValidationMessages.ArgumentException_TypeIsNotEqualityComparer, comparer), nameof(comparer));
    }

    /// <summary>As there is no none generic hash set.</summary>
    private sealed class WrappedComparer : IEqualityComparer<object>
    {
        private readonly IEqualityComparer _comparer;

        public WrappedComparer(IEqualityComparer comparer) => _comparer = comparer;

        [Pure]
        public new bool Equals(object? x, object? y) => _comparer.Equals(x, y);

        [Pure]
        public int GetHashCode(object obj) => _comparer.GetHashCode(obj);
    }
}
