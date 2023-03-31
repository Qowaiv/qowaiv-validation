namespace Qowaiv.Validation.Abstractions.Diagnostics;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
internal sealed class ActFailed : IReadOnlyCollection<StackFrame>, IValidationMessage
{
    /// <summary>Initiates a new instance of the <see cref="ActFailed"/> class.</summary>
    private ActFailed() => Trace = new(2);

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
        => (Trace?.GetFrames() ?? Enumerable.Empty<StackFrame>()).GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [Pure]
    public static IValidationMessage New(bool isValid)
        => isValid || !Debugger.IsAttached
        ? ValidationMessage.None
        : new ActFailed();

}
