using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Store of resolved <see cref="TypeAnnotations"/>.</summary>
internal sealed class AnnotationStore
{
    private static readonly AttributeSorter Sorter = new();
    private readonly ConcurrentDictionary<Type, TypeAnnotations?> Annotations;

    /// <summary>Initializes a new instance of the <see cref="AnnotationStore"/> class.</summary>
    public AnnotationStore()
    {
        Annotations = new ConcurrentDictionary<Type, TypeAnnotations?>(
        [
            None<object>(),
            None<string>(),
            None<decimal>(),
            None<Guid>(),
#if NET6_0_OR_GREATER
            None<DateOnly>(),
            None<TimeOnly>(),
#endif
            None<DateTime>(),
            None<DateTimeOffset>(),
            None<Version>(),

            .. typeof(Date).Assembly.GetExportedTypes().Where(tp => tp.IsValueType).Select(None)
        ]);
    }

    [Pure]
    public TypeAnnotations? Get(Type type, HashSet<Type> visited) => Trim(type) switch
    {
        var t when LackAnnotations(t) => null,
        var t when Annotations.TryGetValue(t, out var annotations) => annotations,
        var t when visited.Add(t) => Annotate(t, visited),
        _ => null,
    };

    [Pure]
    private TypeAnnotations? Annotate(Type type, HashSet<Type> visited)
    {
        var attributes = type.ValidationAttributes().ToArray();
        var properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(Include)
            .Select(p => Annotate(p, visited))
            .OfType<MemberAnnotations>()
            .ToArray();

        if (attributes.Length > 0 || properties.Length > 0 || type.ImplementsIValidatableObject())
        {
            var annotations = new TypeAnnotations(attributes, properties);
            Annotations[type] = annotations;
            return annotations;
        }
        else
        {
            return null;
        }
    }

    [Pure]
    private MemberAnnotations? Annotate(PropertyInfo prop, HashSet<Type> visited)
    {
        List<ValidationAttribute> attributes = [];
        DisplayAttribute? display = null;
        var required = false;
        var skipRequired = prop.PropertyType.IsNonNullableValueType();

        foreach (var attr in prop.GetCustomAttributes(inherit: true))
        {
            switch (attr)
            {
                case OptionalAttribute: required = true; break;
                case RequiredAttribute req:
                    // The default [Required] will always return true for value types so can be skipped.
                    if (!skipRequired || req.GetType() != typeof(RequiredAttribute))
                    {
                        required = true;
                        attributes.Add(req);
                    }
                    break;
                case ValidationAttribute val: attributes.Add(val); break;
                case DisplayAttribute d: display = d; break;
            }
        }
        if (!required && prop.RequiredMemberAttribute() is { } member)
        {
            attributes.Insert(0, member);
        }
        attributes.Sort(Sorter);

        var typeAnnotations = Get(prop.PropertyType, visited);
        var isSealed = Trim(prop.PropertyType).IsSealed;

        return !isSealed || typeAnnotations is { } || attributes is { Count: > 0 }
            ? new(prop.Name, display, attributes.ToArray(), prop.GetValue)
            : null;
    }

    [Pure]
    private static Type Trim(Type type) => type switch
    {
        _ when Nullable.GetUnderlyingType(type) is { } underlying => Trim(underlying),
        _ when type.GetEnumerableType() is { } enumerable => Trim(enumerable),
        _ => type,
    };

    [Pure]
    private static bool LackAnnotations(Type type)
        => type.IsPrimitive
        || type.IsEnum
        || type.IsPointer
        || type.GetCustomAttribute<SkipValidationAttribute>() is { };

    [Pure]
    private static bool Include(PropertyInfo prop)
        => prop.CanRead
        && prop.GetIndexParameters() is { Length: 0 }
        && prop.GetCustomAttribute<SkipValidationAttribute>() is null;

    [Pure]
    private static KeyValuePair<Type, TypeAnnotations?> None<T>() => new(typeof(T), null);

    [Pure]
    private static KeyValuePair<Type, TypeAnnotations?> None(Type tp) => new(tp, null);

    private sealed class AttributeSorter : IComparer<ValidationAttribute>
    {
        [Pure]
        public int Compare(ValidationAttribute? x, ValidationAttribute? y)
            => (y is RequiredAttribute).CompareTo(x is RequiredAttribute);
    }
}
