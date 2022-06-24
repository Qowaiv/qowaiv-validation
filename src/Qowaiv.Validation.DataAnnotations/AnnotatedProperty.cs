namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents a property that contains at least one <see cref="ValidationAttribute"/>.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public class AnnotatedProperty
{
    /// <summary>The underlying descriptor.</summary>
    private readonly PropertyDescriptor descriptor;

    /// <summary>Initializes a new instance of the <see cref="AnnotatedProperty"/> class.</summary>
    private AnnotatedProperty(PropertyDescriptor desc)
    {
        descriptor = desc;
        DisplayAttribute = desc.GetDisplayAttribute() ?? new DisplayAttribute { Name = desc.Name };
        RequiredAttribute = desc.GetRequiredAttribute() ?? OptionalAttribute.Optional;
        ValidationAttributes = desc.GetValidationAttributes().Except(new[] { RequiredAttribute }).ToArray();
        IsEnumerable = PropertyType != typeof(string)
            && PropertyType != typeof(byte[])
            && GetEnumerableType(PropertyType) is not null;
    }

    /// <summary>Gets the type of the property.</summary>
    public Type PropertyType => descriptor.PropertyType;

    /// <summary>Gets the name of the property.</summary>
    public string Name => descriptor.Name;

    /// <summary>True if the property is read-only, otherwise false.</summary>
    public bool IsReadOnly => descriptor.IsReadOnly;

    /// <summary>True if the property is an <see cref="IEnumerable{T}"/> type, otherwise false.</summary>
    public bool IsEnumerable { get; }

    /// <summary>Always true.</summary>
    [Obsolete("Not longer used. Will be dropped.")]
    public bool IsNestedModel => true;

        /// <summary>Gets the type converter associated with the property.</summary>
        /// <remarks>
        /// If not decorated, get the default type converter of the property type.
        /// </remarks>
        [Obsolete("Not longer used, will be dropped.")]
        public TypeConverter TypeConverter => descriptor.GetTypeConverter();

    /// <summary>Gets the display attribute.</summary>
    /// <remarks>
    /// Returns a display attribute with the name equal to the property name if not decorated.
    /// </remarks>
    public DisplayAttribute DisplayAttribute { get; }

    /// <summary>Gets the required attribute.</summary>
    /// <remarks>
    /// <see cref="OptionalAttribute"/> if the property is not decorated.
    /// </remarks>
    public RequiredAttribute RequiredAttribute { get; }

    /// <summary>Gets the <see cref="ValidationAttribute"/>s the property
    /// is decorated with.
    /// </summary>
    public IReadOnlyCollection<ValidationAttribute> ValidationAttributes { get; }

    /// <summary>Gets the value of the property for the specified model.</summary>
    [Pure]
    public object GetValue(object model) => descriptor.GetValue(model);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay
        => $"{descriptor.PropertyType} {descriptor.Name}, Attributes: {string.Join(", ", GetAll().Select(a => Shorten(a)))}";

    [Pure]
    private IEnumerable<ValidationAttribute> GetAll() => Enumerable.Repeat(RequiredAttribute, 1).Concat(ValidationAttributes);

    [Pure]
    private static string Shorten(Attribute attr) => attr.GetType().Name.Replace("Attribute", string.Empty);

    /// <summary>Creates a <see cref="AnnotatedProperty"/> for all annotated properties.</summary>
    [Pure]
    internal static IEnumerable<AnnotatedProperty> CreateAll(Type type)
        => TypeDescriptor
        .GetProperties(type)
        .Cast<PropertyDescriptor>()
        .Select(desc => new AnnotatedProperty(desc));

    [Pure]
    private static Type GetEnumerableType(Type type)
        => type
        .GetInterfaces()
        .FirstOrDefault(iface =>
            iface.IsGenericType &&
            iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))?
        .GetGenericArguments()[0];
}
