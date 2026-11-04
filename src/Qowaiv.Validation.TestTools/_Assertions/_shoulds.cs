using System.Threading.Tasks;

namespace Qowaiv.Validation.TestTools;

/// <summary>Contains extension methods for custom assertions in unit tests.</summary>
[DebuggerNonUserCode]
public static class Shoulds
{
    /// <summary>
    /// Returns an <see cref="ResultAssertions"/> object that can be used to assert the
    /// current <see cref="Result"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions Should(this Result? result, [CallerArgumentExpression(nameof(result))] string? expression = null)
        => new(result, expression);

    /// <summary>
    /// Returns an <see cref="ResultAssertions{TModel}"/> object that can be used to assert the
    /// current <see cref="Result{TModel}"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions<TModel> Should<TModel>(this Result<TModel> result, [CallerArgumentExpression(nameof(result))] string? expression = null)
        => new(result, expression);

    /// <summary>
    /// Returns an <see cref="ResultAssertions"/> object that can be used to assert the
    /// current task of a <see cref="Result"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions Should(this Task<Result> promise, [CallerArgumentExpression(nameof(promise))] string? expression = null)
        => new(Run.Sync(() => promise), expression);

    /// <summary>
    /// Returns an <see cref="ResultAssertions{TModel}"/> object that can be used to assert the
    /// current task of a <see cref="Result{TModel}"/>.
    /// </summary>
    [Pure]
    public static ResultAssertions<TModel> Should<TModel>(this Task<Result<TModel>> promise, [CallerArgumentExpression(nameof(promise))] string? expression = null)
        => new(Run.Sync(() => promise), expression);
}
