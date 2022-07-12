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

public class Casting
{
    [Test]
    public void Implicit_from_T_to_Result_of_T_is_supported()
    {
        Result<int> result = 17;
        Assert.That(result.Value, Is.EqualTo(17));
    }

    [Test]
    public void Explicit_from_Result_of_T_to_T_is_supported()
    {
        var result = Result.For(666);
        Assert.That((int)result, Is.EqualTo(666));
    }
}

public class Result_Of_TModel
{
    [Test]
    public void As_Task_with_AsTask()
        => Assert.That(Result.For(17).AsTask(), Is.InstanceOf<Task<Result<int>>>());
}

public class ThrowIfInvalid
{
    [Test]
    public void Nothing_when_valid()
    {
        Assert.That(() => Result.For(17).ThrowIfInvalid(), Throws.Nothing);
    }

    [Test]
    public void InvalidModelException_when_invalid()
    {
        Assert.That(
            () => Result.WithMessages<int>(ValidationMessage.Error("Oops")).ThrowIfInvalid()
            , Throws.InstanceOf<InvalidModelException>());
    }
}
