# Qowaiv DataAnnotations based validation
Provides an data annotations based implementation of the `Qowaiv.Validation.Abstractions.IValidator`
and data annotation attributes.

## Annotated model validator
The `AnnotatedModelValidator` validates a model using its data annotations.
It returns a `Result<TModel>` of the validated model:

``` C#
var validator = new AnnotatedModelValidator<SomeModel>();
Result<SomeModel> result = validator.Validate(model);
```

## Validation messages
`Qowaiv.Validation.DataAnnotations.ValidationMessage` inherits Microsoft's
`ValidationResult`, and implements `Qowaiv.Validation.Abstractions.IValidationMessage`.
This allows the creation of messages with different severities:

``` C#
var none = ValidationMessage.None;
var info = ValidationMessage.Info(message, args);
var warn = ValidationMessage.Warning(message, args);
var error = ValidationMessage.Error(message, args);
```

## Required modifier
The [`required`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required) modifier is available since C# 11.
When a model has a property with `required` modifier, it is considered beining
decorated with a `[Required]` attribute when the property:
1. is a reference type
2. lacks a nullable (`?`) annotation
3. is not decorated with a `[Required]` attribute.

So in this example:

``` C#
public class Model
{
    public required string RequiredByModifier { get; init; }

    public required string? Optional { get; init; }

    [Required(AllowEmptyStrings = true)]
    public required string AllowStringEmpty {get; init; }
}
```

the first property is considered required by the `AnnotatedModelValidator`. The
second one is considered optional. The third one is also considered reqruied,
but via the attribute it was decorated with, not because of the `required` modifier.

## Validation attributes
Multiple `[System.ComponentModel.DataAnnotations.Validation]` attributes can be
used to decorate models.

### Mandatory
The `[Required]` attribute does not work for value types. The `[Mandatory]`
attribute does. The default value of the `struct` is not valid. It also is not
valid for the Unknown value, unless that is explicitly allowed.

``` C#
public class Model
{
    [Mandatory(AllowUnknownValue = true)]
    public EmailAddress Email { get; set; }

    [Mandatory()]
    public string SomeString { get; set; }
}
```

### Any
The `[Required]` attribute does not work (well) for collections. The `[Any]`
attribute does. It is only valid if the collection contains at least one item.

``` C#
public class Model
{
    [Any()]
    public List<int> Numbers { get; set; }
}
```

### Allowed values
The `[AllowedValues]` attribute allows to define a subset of allowed values. It
supports type converters to get the allowed values based on a string value.

``` C#
public class Model
{
    [Allowed<Country>("DE", "FR", "GB")]
    public Country CountryOfBirth { get; set; }
}
```

### Forbidden values
The `[ForbiddenValues]` attribute allows to define a subset of forbidden values. It
supports type converters to get the forbidden values based on a string value.

``` C#
public class Model
{
    [Forbidden<Country>("US", "IR")]
    public Country CountryOfBirth { get; set; }
}
```

### Defined enum values only
The `[DefinedOnly<TEnum>]` attribute limits the allowed values to defined
enums only. By default it supports all possible combinations of defined enums 
when dealing with flags, but that can be restricted by setting 
`OnlyAllowDefinedFlagsCombinations` to true.

``` C#
public class Model
{
    [DefinedOnly<SomeEnum>(OnlyAllowDefinedFlagsCombinations = false)]
    public SomeEnum CountryOfBirth { get; set; }
}
```

### Is finite
The `[IsFinite]` attribute validates that the floating point value of the field
represents a finite (e.a. not NaN, or infinity).

``` C#
public class Model
{
    [IsFinite]
    public double Number { get; set; }
}
```

### Multiple of
The `[MultipleOf]` attribute validates that the value of a field is a multiple
of the specified factor.

``` C#
public class Model
{
    [MultipleOf(0.001)]
    public Amount Total { get; set; }
}
```
### Optional 
The `[Optional]` attribute indicates explicitly that a field is optional.

``` C#
public class Model
{
    [Optional]
    public string? Message { get; set; }
}
```

### Unique values
The `[Unique&lt;TValue&gt;]` attribute validates that all items of the collection are
distinct. If needed, a custom `IEqualityComparer&lt;TValue&gt` comparer can be defined.

``` C#
public class Model
{
    [Unique<int>(typeof(CustomEqualityComparer))]
    public IEnumerable<int> Numbers { get; set; }
}
```
