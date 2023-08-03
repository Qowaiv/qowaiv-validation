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
This allows the creates messages with different severities:

``` C#
var none = ValidationMessage.None;
var info = ValidationMessage.Info(message, args);
var warn = ValidationMessage.Warning(message, args);
var error = ValidationMessage.Error(message, args);
```

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
attribute does. It is only valid as he collection contains at least one item.

``` C#
public class Model
{
    [Any()]
    public List<int> Numbers { get; set; }
}
```

### Allowed values
The `]AllowedValues]` attribute allows to define a subset of allowed values. It
supports type converters to get the allowed values based on a string value.

``` C#
public class Model
{
    [AllowedValues("DE", "FR", "GB")]
    public Country CountryOfBirth { get; set; }
}
```

### Forbidden values
The `[ForbiddenValues]` attribute allows to define a subset of forbidden values. It
supports type converters to get the allowed values based on a string value.

``` C#
public class Model
{
    [ForbiddenValues("US", "IR")]
    public Country CountryOfBirth { get; set; }
}
```

### Defined enum values only
The `[DefinedEnumValuesOnly]` attribute limits the allowed values to defined
enums only. By default it supports all possible combinations of defined enums 
when dealing with flags, but that can be restricted by setting 
`OnlyAllowDefinedFlagsCombinations` to true.

``` C#
public class Model
{
    [DefinedEnumValuesOnly(OnlyAllowDefinedFlagsCombinations = false)]
    public SomeEnum CountryOfBirth { get; set; }
}
```

### Distinct values
The `[DistinctValues]` attribute validates that all items of the collection are
distinct. If needed, a custom `IEqualityComparer` comparer can be defined.

``` C#
public class Model
{
    [DistinctValues(typeof(CustomEqualityComparer))]
    public IEnumerable<int> Numbers { get; set; }
}
```

### Multiple of
The `[MultipleOf]` attribute validates that the value of a field is a multiple
of the specified factor..

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
