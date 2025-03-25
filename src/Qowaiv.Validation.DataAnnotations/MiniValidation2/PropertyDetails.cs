namespace MiniValidation2;

internal record PropertyDetails(string Name, DisplayAttribute? DisplayAttribute, Type Type, Func<object, object?> GetValue, ValidationAttribute[] ValidationAttributes, bool Recurse, Type? EnumerableType)
{
    public bool IsEnumerable => EnumerableType != null;

    public bool HasValidationAttributes => ValidationAttributes.Length > 0;
}
