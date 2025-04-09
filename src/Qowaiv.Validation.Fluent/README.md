# Qowaiv exensions on [*Fluent Validation](https://fluentvalidation.net/)
Qowaiv provides a set of extensions on top of the FluentValidation library. It
allows to use FluentValidation in combination with Qowaiv's `Result<T>`.

## Validators
There is a set of (generic purpose) validators to validate properties of a model.

### Required
The `RequiredValidation` validates that a required property has a set value. If
specified, an unknown value can be seen as a set value, by default it is not.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.Email).Required();
        RuleFor(m => m.Iban).Required(allowUnknown: true);
    }
}
```

### Not unknown
The `UnknownValidation` validates that a value does not equal the Unknown
value (if existing of course). Accessible via the fluent syntax.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.Email).NotEmptyOrUnknown();
        RuleFor(m => m.Iban).NotUnknown();
    }
}
```

### (Not) before and (not) after
To have messages that use the phrasing `'{PropertyName}' should be after {Value}`
instead of `'{PropertyName}' should be greater than {Value}` makes sense for a
big range of property types, including date (time) related values.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.EndDate).After(m => Clock.Today().AddYears(20));
        RuleFor(m => m.EndDate).NotAfter(m => Clock.Today().AddYears(20));
        RuleFor(m => m.EndDate).Before(m => Clock.Today().AddYears(20));
        RuleFor(m => m.EndDate).NotBefore(m => Clock.Today().AddYears(20));
    }
}
```

### Relative to the clock

The `ClockValidation` validates if a date (time) is in the past, or future.
It supports `Date`, `DateTime`, `Date?`, and `DateTime?`, and the provision
of custom date (time) provider. By Default, `Clock.Now()` and `Clock.Today()`
are used.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.Date1).InFuture();
        RuleFor(m => m.Date2).InPast();
        RuleFor(m => m.Date3).NotInFuture();
        RuleFor(m => m.Date4).NotInPast(() => CustomeDateProvider());
    }
}
```

### Postal code valid for specific country
The `PostalCodeValidation` validates that a `PostalCode` value is valid for
a specific `Country`, both static and via another property.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.PostalCode).ValidFor(m => m.Country);
    }
}
```

### Finite floating points
The `FloatingPointValidation` validates that `double` and `float` values
are finite numbers.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.Number).IsFinite();
    }
}
```
### Email address should not be IP-based
The `EmailAddressValidation` validates that an `EmailAddress`
does not have an IP-based domain.

``` C#
public class CustomValidator : AbstractValidator<Model>
{
    public CustomValidator()
    {
        RuleFor(m => m.Email).NotIPBased();
    }
}
```
