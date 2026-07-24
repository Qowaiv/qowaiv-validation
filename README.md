![Qowaiv](https://github.com/Qowaiv/Qowaiv/blob/master/design/qowaiv-logo_linkedin_100x060.jpg)

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Code of Conduct](https://img.shields.io/badge/%E2%9D%A4-code%20of%20conduct-blue.svg?style=flat)](https://github.com/Qowaiv/qowaiv-validation/blob/master/CODE_OF_CONDUCT.md)

| version                                                                        | downloads                                                             | package                                                                                              |
|--------------------------------------------------------------------------------|-----------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.Abstractions?color=18C)   |![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.Abstractions)   |[Qowaiv.Validation.Abstractions](https://www.nuget.org/packages/Qowaiv.Validation.Abstractions/)      |
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.DataAnnotations?color=18C)|![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.DataAnnotations)|[Qowaiv.Validation.DataAnnotations](https://www.nuget.org/packages/Qowaiv.Validation.DataAnnotations/)|
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.Fluent?color=18C)         |![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.Fluent)         |[Qowaiv.Validation.Fluent](https://www.nuget.org/packages/Qowaiv.Validation.Fluent/)                  |
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.Guarding?color=18C)       |![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.Guarding)       |[Qowaiv.Validation.Guarding](https://www.nuget.org/packages/Qowaiv.Validation.Guarding/)              |
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.Messages?color=18C)       |![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.Messages)       |[Qowaiv.Validation.Messages](https://www.nuget.org/packages/Qowaiv.Validation.Messages/)              |
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.Xml?color=18C)            |![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.Xml)            |[Qowaiv.Validation.Xml](https://www.nuget.org/packages/Qowaiv.Validation.Xml/)                        |
|![v](https://img.shields.io/nuget/v/Qowaiv.Validation.TestTools?color=118)      |![v](https://img.shields.io/nuget/dt/Qowaiv.Validation.TestTools)      |[Qowaiv.Validation.TestTools](https://www.nuget.org/packages/Qowaiv.Validation.TestTools/)            |

# Qowaiv Validation
There are multiple ways to support validation within .NET. Most notably are
* [System.ComponentModel](https://www.nuget.org/packages/System.ComponentModel)
* [FluentValidation.NET](https://fluentvalidation.net)

Qowaiv.Validation aims to provide extensions on top of those that work well when
using [Qowaiv SVOs](https://github.com/Qowaiv/Qowaiv), and prevent vendor lock-in.


## Qowaiv Validation Abstractions
To prevent a vendor lock-in, `Qowaiv.Validation.Abstractions` has been introduced.
To achieve that the following is added:

### IValidator&lt;TModel&gt;
The main interface. Validates a model and returns the validation result.
``` C#
public interface IValidator<TModel>
{
    Result<TModel> Validate(TModel model);
}
```

### Result and Result&lt;TModel&gt;
A (validation) result, containing validation messages. Creation is only via
factory methods. The generic result wraps a `Value` that is only accessible
when valid — accessing it on an invalid result throws `InvalidModelException`.

``` C#
Result<DataType> result = Result.For(value);
Result<DataType> resultWithMessages = Result.For(value, messages);
```

Actions can be composed via method chaining or the `|` pipe operator. Subsequent
actions execute only while the result is valid:

``` C#
Result<DataType> result = GetModel()
    .Act(m => m.Action1())
    .Act(m => m.Action2());

// or with the pipe operator
Result<DataType> result = GetModel()
    | (m => m.Action1())
    | (m => m.Action2());
```

Async versions (`ActAsync`) are available for all overloads. Full documentation
on `ThrowIfInvalid()`, `Validator.Empty<T>()`, casting, and context-update
patterns is available in the [Abstractions README](src/Qowaiv.Validation.Abstractions/README.md).

### IValidationMessage
The common interface for validation messages, with `Severity`, `PropertyName`,
and `Message` properties. Implementations are available in each package.

## Messages
`Qowaiv.Validation.Messages` provides `IValidationMessage` implementations that
are also exceptions: `AccessDenied` (403), `ConcurrencyIssue` (409),
`EntityNotFound` (404), and `ServiceUnavailable` (503). See the
[Messages README](src/Qowaiv.Validation.Messages/README.md) for details.

## Qowaiv extensions on [*Fluent Validation](https://fluentvalidation.net/)
Provides a Fluent Validation based implementation of the `Qowaiv.Validation.Abstractions.IValidator`
and custom validation extensions [(..)](src/Qowaiv.Validation.Fluent/README.md).

## Qowaiv DataAnnotations based validation
Provides a data annotations based implementation of the `Qowaiv.Validation.Abstractions.IValidator`
and data annotation attributes [(..)](src/Qowaiv.Validation.DataAnnotations/README.md).

## XML validation
Validates XML documents and models against XSD schemas, integrated with
`Result<T>`. Supports `XDocument.Validate()` and round-trip validation via
`SchemaValidator<TModel>`. See the [XML README](src/Qowaiv.Validation.Xml/README.md)
for details.

## Guarding
Fluent pre-condition guards using `.Must()` that return `Result<T>`:

``` C#
game.Must().Be(game.Phase == GamePhase.Started, "Game has started");
```

Provides `Be`, `NotBe`, and `Exist` out of the box. See the
[Guarding README](src/Qowaiv.Validation.Guarding/README.md) for details.

## Test Tools
Qowaiv.Validation comes with a separate [Test Tools package](https://www.nuget.org/packages/Qowaiv.TestTools).
Details about that package can be found [here](src/Qowaiv.Validation.TestTools/README.md).
