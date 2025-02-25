# Qowaiv.Validation Test Tools

These test tools help to write meaningful tests and assert the state of a
`Result` or `Result<T>`. The syntax is inspired by [FluentAssertions](https://fluentassertions.com).

These assertions could look like this:

``` C#
Result.For(17).Should().BeValid()
    .WithoutMessages()
    .Value.Should().Be(17); 
```
Where the chain after `.Value` exposes the value.

Other options for `.IsValid()` are:

``` C#
Result.For(17).Shoud().BeValid()
    .WithMessage(ValidationMessage.Warn("Are you sure?"))
    .Value.Should().Be(17);
````
and

``` C#
Result.For(17).Should().BeValid()
    .WithMessages(/* ... */)
    .Value.Should().Be(17);
````

Obviously, you can also check for invalidness:

``` C#
Result.For(17).Should().BeInvalid(); 
```

In that case you do not have the option of `.WithoutMessages()`, obviously,
as `Result` is defined to be invalid bases on containing at least one error
message. Furthermore, it does not exposes the `.Value` as that is not
accessible for an invalid result.

All (except for the `.Value`) is also for the non-generic `Result`.

## Validate with
There is also some syntatic sugar for testing validators on a model of choise:

``` C#
var model = new MyModel();

Result<MyModel> result = model.ValidateWith(new ValidatorForMyModel);
```
And this result can be changed with the assertions described on top.
