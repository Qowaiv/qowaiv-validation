namespace MiniValidation2;

internal sealed record PropertyDetails(string Name, DisplayAttribute? DisplayAttribute, Type Type, Func<object, object?> GetValue, ValidationAttribute[] ValidationAttributes, bool Recurse, Type? EnumerableType)
{
    public bool HasValidationAttributes => ValidationAttributes.Length > 0;
}
