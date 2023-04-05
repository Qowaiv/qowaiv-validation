using Qowaiv.Validation.Abstractions;
using System.Diagnostics;

namespace Abstractions.Result_Act_Debugger_specs;

[NonParallelizable]
public class When_debugger_is_attached
{
    [SetUp]
    public void SetUp()
    {
        SetDebugger.IsAttached(true);
        SetDebugger.Break(true);
    }

    [TearDown]
    public void TearDown()
    {
        SetDebugger.IsAttached(null);
        SetDebugger.Break(false);
    }

    [Test]
    public void Break_on_Act_when_invalid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.WithMessages(ValidationMessage.Error("Break"))))
            .Should().Throw<DebuggerBreaks>();
    }
    [Test]
    public void Break_on_Act_T_when_invalid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.WithMessages<int>(ValidationMessage.Error("Break"))))
            .Should().Throw<DebuggerBreaks>();
    }

    [Test]
    public void Continue_on_Act_when_valid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.OK))
            .Should().NotThrow();
    }

    [Test]
    public void Continue_on_Act_of_T_when_valid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.For(17)))
            .Should().NotThrow();
    }
}

[NonParallelizable]
public class When_debugger_is_not_attached
{
    [SetUp]
    public void SetUp() => SetDebugger.IsAttached(false);

    [TearDown]
    public void TearDown() => SetDebugger.IsAttached(null);


    [Test]
    public void Continue_on_Act_when_invalid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.WithMessages(ValidationMessage.Error("Break"))))
            .Should().NotThrow();
    }
    [Test]
    public void Continue_on_Act_T_when_invalid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.WithMessages<int>(ValidationMessage.Error("Break"))))
            .Should().NotThrow();
    }

    [Test]
    public void Continue_on_Act_when_valid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.OK))
            .Should().NotThrow();
    }

    [Test]
    public void Continue_on_Act_of_T_when_valid()
    {
        var result = Result.For(42);
        result.Invoking(res => res.Act(r => Result.For(17)))
            .Should().NotThrow();
    }
}

static class SetDebugger
{
    public static void IsAttached(bool? isAttached)
    {
        Func<bool> action = isAttached.HasValue
            ? () => isAttached.Value
            : () => Debugger.IsAttached;

        DebuggerWrapper.GetProperty(nameof(IsAttached)).SetValue(null, action);
    }

    public static void Break(bool @throw)
    {
        Action action = @throw
            ? () => throw new DebuggerBreaks()
            : Debugger.Break;

        DebuggerWrapper.GetProperty(nameof(Break)).SetValue(null, action);
    }

    private static Type DebuggerWrapper = typeof(Result).Assembly.DefinedTypes.Single(t => t.Name == nameof(DebuggerWrapper));
}

public sealed class DebuggerBreaks : Exception { }
