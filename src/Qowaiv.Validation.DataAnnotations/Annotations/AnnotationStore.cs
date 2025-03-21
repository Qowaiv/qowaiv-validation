using Qowaiv.Validation.DataAnnotations.Reflection;
using System.Linq.Expressions;
using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Store of resolved <see cref="TypeAnnotations"/>.</summary>
internal sealed class AnnotationStore
{
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
            .GetProperties()
            .Where(Include)
            .Select(p => Annotate(p, visited))
            .OfType<PropertyAnnotations>()
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
    private PropertyAnnotations? Annotate(PropertyInfo prop, HashSet<Type> visited)
    {
        var attributes = prop.ValidationAttributes()
            .OrderByDescending(attr => attr is RequiredAttribute)
            .ToList();

        // Add required member attribute if no Required (including Optional] attribute has been defined.
        if (attributes.FirstOrDefault() is not RequiredAttribute &&
            prop.RequiredMemberAttribute() is { } required)
        {
            attributes.Insert(0, required);
        }

        // We do not want optional to occur in this list.
        if (attributes.FirstOrDefault() is OptionalAttribute)
        {
            attributes.RemoveAt(0);
        }

        var typeAnnotations = Get(prop.PropertyType, visited);

        return typeAnnotations is { } || attributes is { Count: > 0 }
            ? new(prop.Name, typeAnnotations, attributes.ToArray(), PropertyHelper.MakeNullSafeFastPropertyGetter(prop))
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

    [Pure]
    private static Func<object, object?> Compile(PropertyInfo prop)
    {
        var model = Expression.Parameter(typeof(object), "model");
        var typed = Expression.Convert(model, prop.DeclaringType!);
        var accss = Expression.MakeMemberAccess(typed, prop);
        var body = Expression.Convert(accss, typeof(object));
        return Expression.Lambda<Func<object, object?>>(body, model).Compile();
    }
}
