namespace Qowaiv.Validation.Abstractions.Diagnostics;

/// <summary>Represents the <see cref="Result"/> stack trace.</summary>
public sealed class ResultTrace
{
    /// <summary>An empty result trace.</summary>
    public static readonly ResultTrace Empty = new(null);

    private ResultTrace(StackTrace? trace) => Trace = trace;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly StackTrace? Trace;

    /// <summary>Gets the stack frames.</summary>
    public IReadOnlyList<StackFrame> Frames => Trace?.GetFrames() ?? [];

    /// <summary>Gets a specific stack frame.</summary>
    public StackFrame this[int index] => Frames[index];

    /// <inheritdoc />
    [Pure]
    public override string ToString() => Trace?.ToString() ?? string.Empty;

    /// <summary>Creates a validation message.</summary>
    /// <remarks>
    /// Skips the <see cref="StackFrame"/>s that are called by this assembly.
    /// </remarks>
    /// <returns>
    /// <see cref="ValidationMessage.None"/> when valid or when the debugger is not attached
    /// else a <see cref="ActFailed"/>.
    /// </returns>
    [Pure]
    internal static ResultTrace New(IEnumerable<IValidationMessage> messages)
    {
        if (!messages.GetErrors().Any())
        {
            return Empty;
        }
        else
        {
            var trace = new StackTrace(2);
            var skip = trace.GetFrames().TakeWhile(IsInternal).Count();
            return new(skip == 0 || skip + 2 == trace.FrameCount ? trace : new(2 + skip));
        }

        static bool IsInternal(StackFrame frame)
            => typeof(ResultTrace).Assembly == frame.GetMethod()?.DeclaringType?.Assembly;
    }
}
