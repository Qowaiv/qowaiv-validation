namespace Qowaiv.Validation.DataAnnotations.Reflection;

using System.Linq.Expressions;
using System.Reflection;
using Contract = System.Func<
    System.ComponentModel.DataAnnotations.ValidationAttribute,
    object?,
    System.ComponentModel.DataAnnotations.ValidationContext,
    System.ComponentModel.DataAnnotations.ValidationResult?>;

internal static class ValidationAttributes
{
    /// <summary>Checks if the value is valid according to he specific attribute.</summary>
    [Pure]
    public static ValidationResult? IsValid(
        ValidationAttribute attr,
        object? value,
        ValidationContext context)
    {
        var type = attr.GetType();
        if (!Cache.TryGetValue(type, out var expression))
        {
            expression = Compile(type);
            Cache[type] = expression;
        }
        return expression(attr, value, context);
    }

    [Pure]
    private static Contract Compile(Type type)
    {
        var att = Expression.Parameter(typeof(ValidationAttribute), "attr");
        var mth = type.GetMethod(nameof(IsValid), Instance, null, [typeof(object), typeof(ValidationContext)], null)!;
        var val = Expression.Parameter(typeof(object), "value");
        var ctx = Expression.Parameter(typeof(ValidationContext), "context");
        var cast = Expression.Convert(att, type);
        var body = Expression.Call(cast, mth, val, ctx);
        var expr = Expression.Lambda<Contract>(body, att, val, ctx);
        return expr.Compile();
    }

    private static readonly ConcurrentDictionary<Type, Contract> Cache = [];

#pragma warning disable S3011 // Reflection should not be used to increase accessibility.
    public const BindingFlags Instance = BindingFlags.Instance | BindingFlags.NonPublic;
}
