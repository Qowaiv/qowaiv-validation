namespace FluentAssertions.Qowaiv.Validation;

/// <summary>Contains a number of methods to assert the (in)validness of a <see cref="Result"/>.</summary>
public sealed class ResultAssertions : ResultAssertionsBase<Result>
{
    /// <summary>Initializes a new instance of the <see cref="ResultAssertions"/> class.</summary>
    internal ResultAssertions(Result? subject) : base(subject) { }

    /// <summary>Asserts thats <see cref="Result"/> is invalid.</summary>
    [CustomAssertion]
    public ResultValidnessAssertions BeValid(string because = "", params object[] becauseArgs)
    {
        ExecuteBeValid(because, becauseArgs);
        return new ResultValidnessAssertions(Subject);
    }
}
