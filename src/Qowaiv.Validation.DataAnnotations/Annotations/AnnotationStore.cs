using Qowaiv.Validation.DataAnnotations.Reflection;
using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Store of resolved <see cref="MemberAnnotations"/>.</summary>
internal sealed class AnnotationStore
{
    private static readonly AttributeSorter Sorter = new();
    private readonly ConcurrentDictionary<Type, MemberAnnotations[]?> Annotations;

    /// <summary>Initializes a new instance of the <see cref="AnnotationStore"/> class.</summary>
    public AnnotationStore()
    {
        Annotations = new ConcurrentDictionary<Type, MemberAnnotations[]?>(
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
    public MemberAnnotations[]? Get(Type type, HashSet<Type> visited) => Trim(type) switch
    {
        var t when LackAnnotations(t) => null,
        var t when Annotations.TryGetValue(t, out var annotations) => annotations,
        var t when visited.Add(t) => Annotate(t, visited),
        _ => null,
    };

    [Pure]
    private MemberAnnotations[]? Annotate(Type type, HashSet<Type> visited)
    {
        MemberAnnotations[] members =
        [
            ..type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(Include)
                .Select(i => Annotate(Member.New(i), visited))
                .OfType<MemberAnnotations>(),

            ..type
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Select(i => Annotate(Member.New(i), visited))
                 .OfType<MemberAnnotations>(),
        ];

        if (members.Length > 0 || type.ImplementsIValidatableObject())
        {
            Annotations[type] = members;
            return members;
        }
        else
        {
            return null;
        }
    }

    [Pure]
    private MemberAnnotations? Annotate(Member info, HashSet<Type> visited)
    {
        List<ValidationAttribute> attributes = [];
        DisplayAttribute? display = null;
        var required = false;
        var skipRequired = info.MemberType.IsNonNullableValueType();

        foreach (var attr in info.GetCustomAttributes())
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
        if (!required && info.GetRequiredMemberAttribute() is { } member)
        {
            attributes.Insert(0, member);
        }
        attributes.Sort(Sorter);

        var typeAnnotations = Get(info.MemberType, visited);
        var isSealed = Trim(info.MemberType).IsSealed;

        return !isSealed || typeAnnotations is { } || attributes is { Count: > 0 }
            ? new(info.Name, display, attributes.ToArray(), info.GetValue)
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
    private static KeyValuePair<Type, MemberAnnotations[]?> None<T>() => new(typeof(T), null);

    [Pure]
    private static KeyValuePair<Type, MemberAnnotations[]?> None(Type tp) => new(tp, null);

    private sealed class AttributeSorter : IComparer<ValidationAttribute>
    {
        [Pure]
        public int Compare(ValidationAttribute? x, ValidationAttribute? y)
            => (y is RequiredAttribute).CompareTo(x is RequiredAttribute);
    }
}
