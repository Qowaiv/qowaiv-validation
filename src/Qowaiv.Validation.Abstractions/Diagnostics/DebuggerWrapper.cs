namespace Qowaiv.Validation.Abstractions.Diagnostics;

/// <summary>
/// The <see cref="Debugger" /> class is a part of the System.Diagnostics package
/// and is used for communicating with a debugger.
/// </summary>
/// <remarks>
/// For testability reasons, this internal wrapper is added, so that under test
/// the behavior can be adjusted.
/// </remarks>
internal static class DebuggerWrapper
{
    /// <summary>Returns whether or not a debugger is attached to the process.</summary>
    public static Func<bool> IsAttached { get; set; } = () => Debugger.IsAttached;

    /// <summary>
    /// Break causes a breakpoint to be signalled to an attached debugger.If no debugger
    /// is attached, the user is asked if they want to attach a debugger. If yes, then the
    /// debugger is launched.
    /// </summary>
    public static Action Break { get; set; } = Debugger.Break;
}
