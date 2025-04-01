using Qowaiv.Validation.DataAnnotations.Reflection;
using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

/// <summary>Store of resolved <see cref="MemberAnnotations"/>.</summary>
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
        MemberAnnotations[] members =
        [
            .. type
                .GetPublicInstanceMembers()
                .Where(Include)
                .Select(i => Annotate(i, visited))
                .OfType<MemberAnnotations>()
        ];

        var checks = AnnotationChecks.None;
        checks |= members.Length > 0 ? AnnotationChecks.Attributes : default;
        checks |= type.ImplementsIValidatableObject() ? AnnotationChecks.Validatable : default;
        
        // checks |= AnnotationChecks.Recursive;

        if (checks != AnnotationChecks.None)
        {
            var annotations = new TypeAnnotations(checks, members);
            Annotations[type] = annotations;
            return annotations;
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
                case SkipValidationAttribute: return null;
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
            ? new(default, info.Name, display, attributes.ToArray(), info.GetValue)
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
    private static bool Include(Member member)
        => member.CanRead
        && member.IsNotIndexed;

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
