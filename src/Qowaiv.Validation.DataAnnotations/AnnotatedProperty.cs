using Qowaiv.Validation.DataAnnotations.Refelection;
using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents a property that contains at least one <see cref="ValidationAttribute"/>.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public class AnnotatedProperty
{
    /// <summary>Initializes a new instance of the <see cref="AnnotatedProperty"/> class.</summary>
    private AnnotatedProperty(PropertyInfo property)
    {
        PropertyType = property.PropertyType;
        Name = property.Name;
        DisplayAttribute = property.DisplayAttribute();
        RequiredAttribute = property.RequiredAttribute() ?? OptionalAttribute.Optional;
        ValidationAttributes = property.ValidationAttributes().Where(attr => attr is not System.ComponentModel.DataAnnotations.RequiredAttribute).ToArray();
        IsNestedModel = property.PropertyType.IsValidatableObject();
        IsEnumerable = PropertyType != typeof(string)
            && PropertyType != typeof(byte[])
            && GetEnumerableType(PropertyType) is not null;

        getValue = (model) => property.GetValue(model);
    }

    /// <summary>Gets the type of the property.</summary>
    public Type PropertyType { get; }

    /// <summary>Gets the name of the property.</summary>
    public string Name { get; }

    /// <summary>True if the property is an <see cref="IEnumerable{T}"/> type, otherwise false.</summary>
    public bool IsEnumerable { get; }

    /// <summary>True if the model is decorated with the <see cref="NestedModelAttribute"/>, otherwise false.</summary>
    public bool IsNestedModel { get; }

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
    public object GetValue(object model) => getValue(model);
    private readonly Func<object, object> getValue;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay
        => $"{PropertyType} {Name}, Attributes: {string.Join(", ", GetAll().Select(a => Shorten(a)))}";

    [Pure]
    private IEnumerable<ValidationAttribute> GetAll() => Enumerable.Repeat(RequiredAttribute, 1).Concat(ValidationAttributes);

    [Pure]
    private static string Shorten(Attribute attr) => attr.GetType().Name.Replace("Attribute", string.Empty);

    /// <summary>Creates a <see cref="AnnotatedProperty"/> for all annotated properties.</summary>
    [Pure]
    internal static IEnumerable<AnnotatedProperty> CreateAll(Type type)
        => type.GetProperties()
        .Select(property => new AnnotatedProperty(property));

    [Pure]
    private static Type GetEnumerableType(Type type)
        => type
        .GetInterfaces()
        .FirstOrDefault(iface =>
            iface.IsGenericType &&
            iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))?
        .GetGenericArguments()[0];
}
