namespace MiniValidation2;

internal record PropertyDetails(string Name, DisplayAttribute? DisplayAttribute, Type Type, Func<object, object?> PropertyGetter, ValidationAttribute[] ValidationAttributes, bool Recurse, Type? EnumerableType)
{
    public object? GetValue(object target) => PropertyGetter(target);

    public bool IsEnumerable => EnumerableType != null;

    public bool HasValidationAttributes => ValidationAttributes.Length > 0;
}
