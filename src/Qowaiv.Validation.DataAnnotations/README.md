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
var none = ValidationMessage.None; // returns null, but for telling the story ValidationMessage.None is preferred.
var info = ValidationMessage.Info(message, args);
var warn = ValidationMessage.Warning(message, args);
var error = ValidationMessage.Error(message, args);
```

## Required modifier
The [`required`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required) modifier is available since C# 11.
When a model has a property with a `required` modifier, it is considered being
decorated with a `[Required]` attribute via the `required` modifier when the
property:
1. is a reference type
2. lacks a nullable (`?`) annotation

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

The first property is considered required by the `AnnotatedModelValidator`, and
the second one is considered optional. The third one is also considered required,
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
    public EmailAddress Email { get; init; }

    [Mandatory()]
    public string SomeString { get; init; }
}
```

### Any
The `[Required]` attribute does not work (well) for collections. The `[Any]`
attribute does. It is only valid if the collection contains at least one item.

``` C#
public class Model
{
    [Any()]
    public List<int> Numbers { get; init; }
}
```

### Allowed values
The `[Allowed<TValue>]` attribute allows to define a subset of allowed values. It
supports type converters to get the allowed values based on a primitive value.

``` C#
public class Model
{
    [Allowed<Country>("DE", "FR", "GB")]
    public Country CountryOfBirth { get; init; }
}
```

### Forbidden values
The `[Forbidden<TValue>]` attribute allows to define a subset of forbidden values. It
supports type converters to get the forbidden values based on a primitive value.

``` C#
public class Model
{
    [Forbidden<Country>("US", "IR")]
    public Country CountryOfBirth { get; init; }
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
    public SomeEnum CountryOfBirth { get; init; }
}
```

### In future
The `[InFuture]` attributes requires the `DateTime`, `DateTimeOffset`, `Date`,
`DateOnly`, or `Year` value to be in the future. The current time is resolved
using [`Qowaiv.Clock.UtcNow()`](https://github.com/Qowaiv/Qowaiv/blob/master/README.md#qowaiv-clock).

``` C#
public class Model
{
    [InFuture]
    public DateOnly? ExpiryDate { get; init; }
}
```

### In past
The `[InPast]` attributes requires the `DateTime`, `DateTimeOffset`, `Date`,
`DateOnly`, or `Year` value to be in the past. The current time is resolved
using [`Qowaiv.Clock.UtcNow()`](https://github.com/Qowaiv/Qowaiv/blob/master/README.md#qowaiv-clock).

``` C#
public class Model
{
    [InPast]
    public DateOnly DateOfBirth { get; init; }
}
```

### In range
The `[InRange<TValue>]` attribute the allowed values to the specified range.
This attribute is simliar to Microsoft's `[Range]`(https://learn.microsoft.com/dotnet/api/system.componentmodel.dataannotations.rangeattribute).


### Items validation
The `[Items<TValidator>]` attribute to define a validation attribute to apply
on all items of a collection. This is useful in multiple cases:

``` C#
public class Model
{
    [Items<Mandatory>] // ensures none of the items is null or empty
    public string[] Names { get; init; } = [];

    [Items<Allowed<int>(42, 2017)] // ensures that all items have either the value 42 or 2017.
    public int[] Numbers { get; init; } = [];
}
```

By defining either `ErrorMessage` or both `ErrorMessageResourceName` and `ErrorMessageResourceType`,
those values are set to the `TValidator` allowing full control of the error message generation.

### Is finite
The `[IsFinite]` attribute validates that the floating point value of the field
represents a finite (e.a. not NaN, or infinity).

``` C#
public class Model
{
    [IsFinite]
    public double Number { get; init; }
}
```

### Skip validation
The `[SkipValidation]` attribute allows to skip the validation of property or
type.

``` C#
[SkipValidation]
public class Model
{
    [SkipValidation]
    public double Number { get; init; }
}
```

### Multiple of
The `[MultipleOf]` attribute validates that the value of a field is a multiple
of the specified factor.

``` C#
public class Model
{
    [MultipleOf(0.001)]
    public Amount Total { get; init; }
}
```

# Not in future
The `[NotInFuture]` attributes requires the `DateTime`, `DateTimeOffset`, `Date`,
`DateOnly`, or `Year` value not to be in the future. The current time is resolved
using [`Qowaiv.Clock.UtcNow()`](https://github.com/Qowaiv/Qowaiv/blob/master/README.md#qowaiv-clock).

``` C#
public class Model
{
    [InFuture]
    public DateTime CreationTime { get; init; }
}
```

# Not in past
The `[NotInPast]` attributes requires the `DateTime`, `DateTimeOffset`, `Date`,
`DateOnly`, or `Year` value not to be in the past. The current time is resolved
using [`Qowaiv.Clock.UtcNow()`](https://github.com/Qowaiv/Qowaiv/blob/master/README.md#qowaiv-clock).

``` C#
public class Model
{
    [InPast]
    public DateOnly Start { get; init; }
}
```

### Optional 
The `[Optional]` attribute indicates explicitly that a field is optional.

``` C#
public class Model
{
    [Optional]
    public string? Message { get; init; }
}
```

### Unique values
The `[Unique<Value>]` attribute validates that all items of the collection are
distinct. If needed, a custom `IEqualityComparer<Value>` comparer can be defined.

``` C#
public class Model
{
    [Unique<int>(typeof(CustomEqualityComparer))]
    public IEnumerable<int> Numbers { get; init; }
}
```

### Validates attribute
The `[Validates]` attribute is designed to help the [QW0102](https://github.com/Qowaiv/qowaiv-analyzers/blob/main/rules/QW0102.md)
to report on misusage of the attribute. By decorating an `[Validation]` attribute
the analyzer knows on which member types the attribute can be set.
