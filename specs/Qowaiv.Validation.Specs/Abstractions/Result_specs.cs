using Qowaiv.Validation.Abstractions;
using static Specs.Abstractions.Arrange;

namespace Abstractions.Result_specs;

public class Valid_result
{
    [Test]
    public void Contains_no_ErrorMessages()
    {
        var result = Result.WithMessages<int>(Warning1, Info1, Info2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Has_access_to_Value()
    {
        var result = Result.For(2);
        Assert.That(result.Value, Is.EqualTo(2));
    }
}

public class Invalid_result
{
    [Test]
    public void Contains_at_least_one_ErrorMessage()
    {
        var result = Result.WithMessages(TestMessages.AsEnumerable());
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Has_no_access_to_Value()
    {
        var result = Result.For(new object(), ValidationMessage.Error("Not OK"));
        Assert.That(() => result.Value, Throws.TypeOf<InvalidModelException>());
    }
}

public class Null_result
{
    [Test]
    public void valid_if_explicit() => Assert.That(Result.Null<object>().IsValid, Is.True);

    [Test]
    public void valid_for_nullable_struct() => Assert.That(Result.Null<int?>().IsValid, Is.True);

    [Test]
    public void valid_if_explicit_with_messages()
    {
        var messages = new List<IValidationMessage> { ValidationMessage.Warn("Some warning") };
        Assert.That(Result.Null<object>(messages).IsValid, Is.True);
    }

    [Test]
    public void invalid_if_implicit() 
        => Assert.That(() => Result.For<object>(null), Throws.TypeOf<NoValue>().With.Message.EqualTo("The value of the Result<Object> can not be null. (Parameter 'Value')"));

    [Test]
    public void invalid_if_implicit_with_messages()
        => Assert.That(() => Result.WithMessages<object>(), Throws.TypeOf<NoValue>());

    [Test]
    public void invalid_if_implicit_with_empty_messages()
        => Assert.That(() => Result.WithMessages<object>(Array.Empty<IValidationMessage>()), Throws.TypeOf<NoValue>());
}

public class Filtering
{
    [Test]
    public void Error_messages_is_done_via_the_Errors_property()
    {
        var result = Result.WithMessages(TestMessages);
        Assert.That(result.Errors, Is.EqualTo(new[] { Error1, Error2 }));
    }

    [Test]
    public void Warning_messages_is_done_via_the_Warnings_property()
    {
        var result = Result.WithMessages(TestMessages);
        var act = result.Warnings;
        var exp = new[] { Warning1, Warning2 };
        Assert.AreEqual(exp, act);
    }

    [Test]
    public void Info_messages_is_done_via_the_Infos_property()
    {
        var result = Result.WithMessages(TestMessages);
        var act = result.Infos;
        var exp = new[] { Info1, Info2 };
        Assert.AreEqual(exp, act);
    }
}

public class Casts
{
    [Test]
    public void Implicit_from_T_to_Result_of_T_is_supported()
    {
        Result<int> result = 17;
        result.Value.Should().Be(17);;
    }

    [Test]
    public void Explicit_from_Result_of_T_to_T_is_supported()
    {
        var result = Result.For(666);
        ((int)result).Should().Be(666);
    }
}

public class Result_of_TModel
{
    [Test]
    public void As_Task_with_AsTask()
        => Result.For(17).AsTask().Should().BeOfType<Task<Result<int>>>();
}

public class Throw_if_invalid
{
    [Test]
    public void Nothing_when_valid()
    {
        Action action = () => Result.For(17).ThrowIfInvalid();
        action.Should().NotThrow();
    }

    [Test]
    public void InvalidModelException_when_invalid()
    {
        Action action = () => Result.For(17, ValidationMessage.Error("Oops")).ThrowIfInvalid();
        action.Should().Throw<InvalidModelException>();
    }
}

public class Warnings_as_errors
{
    [Test]
    public void has_no_effect_on_result_without_warnings()
    {
        var original = Result.WithMessages(Error1, Error2, Info1);
        var transformed = original.WarningsAsErrors();
        transformed.Messages.Should().BeEquivalentTo(original.Messages);
    }

    [Test]
    public void has_no_effect_on_result_of_T_without_warnings()
    {
        var original = Result.For(69, Error1, Error2, Info1);
        var transformed = original.WarningsAsErrors();
        transformed.Messages.Should().BeEquivalentTo(original.Messages);
    }

    [Test]
    public void transforms_warings_for_result()
    {
        var original = Result.WithMessages(Error1, Error2, Warning1, Warning2, Info1);
        var transformed = original.WarningsAsErrors();
        transformed.Should().BeInvalid().WithMessages(Error1, Error2, Info1,
            ValidationMessage.Error(Warning1.Message, Warning1.PropertyName),
            ValidationMessage.Error(Warning2.Message, Warning2.PropertyName));
    }

    [Test]
    public void transforms_warings_for_result_of_T()
    {
        var original = Result.For(69, Error1, Error2, Warning1, Warning2, Info1);
        var transformed = original.WarningsAsErrors();
        transformed.Should().BeInvalid().WithMessages(Error1, Error2, Info1,
            ValidationMessage.Error(Warning1.Message, Warning1.PropertyName),
            ValidationMessage.Error(Warning2.Message, Warning2.PropertyName));
    }
}
