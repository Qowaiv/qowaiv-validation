namespace FluentAssertions.Qowaiv.Validation;

/// <summary>Contains a number of methods to assert the value of a valid <see cref="Result{TModel}"/>.</summary>
public class ResultValidnessAssertions<TModel> : ResultValidnessAssertionsBase<Result<TModel>>
{
    internal ResultValidnessAssertions(Result<TModel> subject) : base(subject) { }

    /// <summary>Expose the value, so that the chain of assertions can be continued.</summary>
    public TModel Value => Subject.Value;
}
