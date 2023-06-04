namespace Qowaiv.Validation.Abstractions.Diagnostics;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
internal sealed class ActFailed : IReadOnlyCollection<StackFrame>, IValidationMessage
{
    /// <summary>Initiates a new instance of the <see cref="ActFailed"/> class.</summary>
    private ActFailed(StackTrace trace) => Trace = trace;

    /// <inheritdoc />
    public ValidationSeverity Severity
        => Trace is { }
        ? ValidationSeverity.Error
        : ValidationSeverity.None;

    /// <inheritdoc />
    public string? PropertyName => null;

    /// <inheritdoc />
    public string Message => Trace.ToString();

    public StackTrace Trace { get; }

    /// <inheritdoc />
    public int Count => Trace.GetFrames().Length;

    /// <inheritdoc />
    [Pure]
    public IEnumerator<StackFrame> GetEnumerator()
        => Trace.GetFrames().AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Creates a validation message.</summary>
    /// <remarks>
    /// Skips the <see cref="StackFrame"/>s that are called by this assembly.
    /// </remarks>
    /// <returns>
    /// <see cref="ValidationMessage.None"/> when valid or when the debugger is not attached
    /// else a <see cref="ActFailed"/>.
    /// </returns>
    [Pure]
    public static IValidationMessage New(bool isValid)
    {
        if (isValid || !Debugger.IsAttached)
        {
            return ValidationMessage.None;
        }
        else
        {
            var trace = new StackTrace(2);
            var skip = trace.GetFrames().TakeWhile(IsInternal).Count();
            return new ActFailed(skip == 0 ? trace : new(2 + skip));
        }

        static bool IsInternal(StackFrame frame)
            => typeof(ActFailed).Assembly == frame.GetMethod()?.DeclaringType?.Assembly;
    }
}
