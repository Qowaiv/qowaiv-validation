using Qowaiv.Validation.DataAnnotations.Reflection;
using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Represents an annotated model.</summary>
/// <remarks>
/// An annotated model should contains at least one <see cref="ValidationAttribute"/>
/// or implement <see cref="IValidatableObject"/>.
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
[Obsolete("Will be dropped in v4.0.0. Use TypeAnnotations instead.")]
public sealed class AnnotatedModel
{
    internal static readonly AnnotatedModel None = new(typeof(object), false, [], []);

    /// <summary>Gets the singleton instance of the <see cref="AnnotatedModelStore"/>.</summary>
    internal static readonly AnnotatedModelStore Store = new();

    /// <summary>Initializes a new instance of the <see cref="AnnotatedModel"/> class.</summary>
    private AnnotatedModel(
        Type type,
        bool isIValidatableObject,
        ValidationAttribute[] typeAttributes,
        AnnotatedProperty[] annotatedProperties)
    {
        ModelType = type;
        IsIValidatableObject = isIValidatableObject;
        TypeAttributes = typeAttributes;
        Properties = annotatedProperties;
    }

    /// <summary>Gets the <see cref="Type"/> of the model.</summary>
    public Type ModelType { get; }

    /// <summary>Gets the <see cref="ValidationAttribute"/>s of the model.</summary>
    public IReadOnlyCollection<ValidationAttribute> TypeAttributes { get; }

    /// <summary>Gets the annotated properties of the model.</summary>
    public IReadOnlyCollection<AnnotatedProperty> Properties { get; }

    /// <summary>Returns true if the model implements <see cref="IValidatableObject"/>,
    /// otherwise false.
    /// </summary>
    public bool IsIValidatableObject { get; }

    /// <summary>Gets an <see cref="AnnotatedModel"/>.</summary>
    /// <param name="type">
    /// Type to create the annotated model for.
    /// </param>
    /// <remarks>
    /// This one uses caching.
    /// </remarks>
    [Pure]
    public static AnnotatedModel Get(Type type) => Store.GetAnnotatedModel(type);

    /// <summary>Creates an <see cref="AnnotatedModel"/>.</summary>
    [Pure]
    internal static AnnotatedModel Create(Type type)
    {
        Guard.NotNull(type);

        if (type.GetCustomAttribute<SkipValidationAttribute>() is { })
        {
            return None;
        }

        var isIValidatable = typeof(IValidatableObject).IsAssignableFrom(type);
        var validations = type.ValidationAttributes().ToArray();
        var properties = AnnotatedProperty.CreateAll(type).ToArray();

        return isIValidatable || validations.Length > 0 || properties.Length > 0
            ? new AnnotatedModel(type, isIValidatable, validations, properties)
            : None;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay
        => $"{ModelType}, Attributes: {TypeAttributes.Count}, Properties: {Properties.Count}";
}
