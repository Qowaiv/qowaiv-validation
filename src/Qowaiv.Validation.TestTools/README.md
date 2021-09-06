# Qowaiv.Validation Test Tools

These test tools help writing meaningful tests an asserting the state of a
`Result` or `Result<T>`. This is done by extending on
[FluentAssertions](https://fluentassertions.com).

These assertions could look like this:

``` C#
Result.For(17).IsValid().WithoutMessages().Value.Should().Be(17); 
```
Where the chain after `.Value` is regular FluentAssertions.

Other options for `.IsValid()` are:

``` C#
Result.For(17).IsValid().WithMessage(ValidationMessage.Warn("Are you sure?")).Value.Should().Be(17);
````
and

``` C#
Result.For(17).IsValid().WithMessages(/* ... */).Value.Should().Be(17);
````

Obviously, you can also check for invalidness:

``` C#
Result.For(17).IsInValid(); 
```
In that case you do not have the option of `.WithoutMessages()`, obviously,
as `Result` is defined to be invalid bases on containing at least one error
message. Furthermore, it does not exposes the `.Value` as that is not
accessible for an invalid result.

All (except for the `.Value`) is also for the non-generic `Result`.
