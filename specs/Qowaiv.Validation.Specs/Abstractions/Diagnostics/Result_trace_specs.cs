using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.Abstractions.Diagnostics;
using Result = Qowaiv.Validation.Abstractions.Result;

namespace Abstractions.Diagnostics.Result_trace_specs;

public class Empty
{
    [Test]
    public void for_contant()
        => ResultTrace.Empty.Should().BeEmpty();

    [Test]
    public void for_succesfull_trace()
    {
        var result = Result.For(42).Act(MyClass.WithResult);
        result.StackTrace.Should().BeEmpty();
        result.StackTrace.ToString().Should().BeEmpty();
    }
}

public class Not_Empty
{
    [Test]
    public void for_error_tace()
    {
        var result = Result.For(42);

        var outcome = result
            .Act(MyClass.NowInvalid)
            .Act(MyClass.StillInvalid);

        outcome.StackTrace.Should().NotBeEmpty();

        var str = outcome.StackTrace.ToString();

        
    }
}

file static class MyClass
{
    public static Result<int> WithResult(int r) => 42;
    public static Result<int> NowInvalid(int r) => Result.WithMessages<int>(ValidationMessage.Error("Now it is broken."));
    public static Result<int> StillInvalid(int r) => Result.WithMessages<int>(ValidationMessage.Error("Still broken."));
}
