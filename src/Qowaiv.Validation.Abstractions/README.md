# Qowaiv Validation Abstractions

Core abstractions for model validation. Provides a vendor-lock-in-free
`IValidator<TModel>` / `Result<TModel>` abstraction that DataAnnotations,
FluentValidation, and XML validators all implement.

## IValidator&lt;TModel&gt;

The main interface. Validates a model and returns the validation result.

``` C#
public interface IValidator<TModel>
{
    Result<TModel> Validate(TModel model);
}
```

### Validator.Empty

To create a validator that always returns a valid result:

``` C#
IValidator<MyModel> validator = Validator.Empty<MyModel>();
Result<MyModel> result = validator.Validate(model); // always valid
```

## Result and Result&lt;TModel&gt;

A validation result containing messages. Created only via factory methods.

``` C#
Result<DataType> ok = Result.OK;
Result<DataType> withValue = Result.For(value);
Result<DataType> withErrors = Result.For(value, ValidationMessage.Error("Bad"));
Result<DataType> nullResult = Result.Null<DataType>();
Result<DataType> withMessages = Result.WithMessages<DataType>(ValidationMessage.Warn("Check"));
```

### Properties

- `Messages` — all validation messages
- `IsValid` — `true` when there are no error-severity messages
- `Errors` / `Warnings` / `Infos` — filtered views by severity

### Value access

The generic `Result<TModel>` wraps a `Value` that is only accessible when
valid. Accessing `Value` on an invalid result throws `InvalidModelException`.

``` C#
Result<MyModel> result = Validate(model);
if (result.IsValid)
{
    MyModel value = result.Value; // safe
}
```

### Throw if invalid

When you want to fail fast:

``` C#
result.ThrowIfInvalid(); // throws InvalidModelException if invalid
```

### Implicit and explicit casts

``` C#
// Implicit: valid values auto-wrap into Result<T>
Result<MyModel> implicit = validModel;

// Explicit: extracts the value, throws if invalid
MyModel explicit = (MyModel)result;

// Cast: changes the wrapped type (must be a subclass)
Result<DerivedModel> casted = result.Cast<DerivedModel>();
```

## Result composition

### Act

Chain multiple validation/transformation steps. Subsequent actions execute
only while the result is valid, and messages accumulate:

``` C#
Result<DataType> result = GetModel()
    .Act(m => ValidateName(m))
    .Act(m => ValidateAge(m));
```

### ActAsync

Async versions for all `Act` overloads:

``` C#
Result<DataType> result = await GetModelAsync()
    .ActAsync(m => FetchDetailsAsync(m))
    .ActAsync(m => SaveAsync(m));
```

### Pipe operator

The `|` operator provides an alternative syntax for `Act`:

``` C#
Result<DataType> result = GetModel()
    | ValidateName
    | ValidateAge;
```

### Act with context update

For mutable or immutable shared context patterns:

``` C#
// Mutable update
Result<Context> ctx = NewContext()
    .Act(c => Service.GetValue(), (c, value) => c.Value = value);

// Immutable update
Result<Context> ctx = NewContext()
    .Act(c => Service.GetValue(), (c, value) => c.Update(value));
```

## IValidationMessage

The common interface for all validation messages.

``` C#
public interface IValidationMessage
{
    ValidationSeverity Severity { get; }
    string? PropertyName { get; }
    string? Message { get; }
}
```

## ValidationMessage

A concrete, sealed implementation with factory methods:

``` C#
IValidationMessage error = ValidationMessage.Error("Name is required", "Name");
IValidationMessage warn = ValidationMessage.Warn("Deprecated field");
IValidationMessage info = ValidationMessage.Info("Auto-corrected");
IValidationMessage none = ValidationMessage.None; // null
```

## ValidationSeverity

``` C#
public enum ValidationSeverity
{
    Info = 0,
    Warning = 1,
    Error = 2,
}
```

## InvalidModelException

Thrown when accessing `Value` on an invalid result. Contains the `Errors`
collection:

``` C#
try
{
    var value = invalidResult.Value;
}
catch (InvalidModelException ex)
{
    IEnumerable<IValidationMessage> errors = ex.Errors;
}
```
