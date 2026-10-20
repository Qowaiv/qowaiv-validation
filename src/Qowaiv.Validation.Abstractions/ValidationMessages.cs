namespace Qowaiv.Validation.Abstractions;

/// <summary>Represents a read-only collection of valiation messages.</summary>
[DebuggerDisplay("{DebuggerDisplay),nq}")]
[CollectionBuilder(typeof(ValidationMessages), nameof(New))]
public readonly struct ValidationMessages : IReadOnlyCollection<IValidationMessage>, ICollection
{
    /// <summary>An empty validation message collection.</summary>
    public static readonly ValidationMessages None = new(0, 0, 0, []);

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    private readonly IValidationMessage[] Messages;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Err;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Wrn;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Inf;

    private ValidationMessages(int err, int wrn, int inf, IValidationMessage[] messages)
    {
        Err = err;
        Wrn = wrn;
        Inf = inf;
        Messages = messages;
    }

    /// <summary>True if any of the messages is an error.</summary>
    public bool HasErrors => Err is not 0;

    /// <summary>Indicates that the collection is not empty.</summary>
    public bool HasAny => Count is not 0;

    /// <inheritdoc />
    public int Count => messages().Length;

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Error"/>.</summary>
    public ValidationMessages Errors => new(Err, 0, 0, messages(..Err));

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Warning"/>.</summary>
    public ValidationMessages Warnings => new(0, Wrn, 0, messages(Err..^Inf));

    /// <summary>Gets all messages with <see cref="ValidationSeverity.Info"/>.</summary>
    public ValidationMessages Infos => new(0, 0, Inf, messages(^Inf..));

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get
        {
            var sb = new StringBuilder().Append($"Count = {Count}");
            if (Err is not 0) sb.Append($", Errors = {Err}");
            if (Wrn is not 0) sb.Append($", Warings = {Wrn}");
            if (Inf is not 0) sb.Append($", Warings = {Inf}");
            return sb.ToString();
        }
    }

    /// <inheritdoc />
    bool ICollection.IsSynchronized => true;

    /// <inheritdoc />
    object ICollection.SyncRoot => messages().SyncRoot;

    [Pure]
    public ValidationMessages Append(IEnumerable<IValidationMessage> messages)
    {
        var additions = New(messages);

        if (Count is 0) return additions;
        else if (additions.Count is 0) return this;
        else
        {
            IValidationMessage[] combined = [.. Messages, .. additions.Messages];
            Array.Sort(combined, Compare);

            return new(
                Err + additions.Err,
                Wrn + additions.Wrn,
                Inf + additions.Inf,
                combined);
        }
    }

    /// <inheritdoc />
    public void CopyTo(Array array, int index) => messages().CopyTo(array, index);

    /// <inheritdoc />
    [Pure]
    public IEnumerator<IValidationMessage> GetEnumerator() => ((IEnumerable<IValidationMessage>)messages()).GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => messages().GetEnumerator();

    /// <summary>Helper to get a subset of all messages.</summary>
    [Pure]
    private IValidationMessage[] messages(Range range)
#if NETSTANDARD2_0
    {
        // SubRange is not available for .NET standard 2.0 unfortunatly.
        var count = Count;
        var start = range.Start is { IsFromEnd: true } ? count - range.Start.Value : range.Start.Value;
        var end = range.End is { IsFromEnd: true } ? count - range.End.Value : range.End.Value;
        var length = end - start;

        if (length == count) return messages();

        var copy = new IValidationMessage[length];
        Array.Copy(Messages, start, copy, 0, length);
        return copy;
    }
    #else
        => messages()[range];
    #endif

    [Pure]
    private IValidationMessage[] messages() => Messages ?? [];

    /// <summary>Creates a new collection of <see cref="ValidationMessages" /></summary>
    /// <param name="messages">
    /// The messages.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// When the message Severity is other then Error, Warning, Info, or None.
    /// </exception>
    [Pure]
    [OverloadResolutionPriority(1)]
    public static ValidationMessages New(IEnumerable<IValidationMessage?> messages)
    {
        if (messages is null) return None;

        var index = -1;
        var list = new List<IValidationMessage>();
        var err = 0;
        var wrn = 0;
        var inf = 0;
        foreach (var message in messages)
        {
            index++;
            if (message is null || message.Severity is ValidationSeverity.None) continue;

            if (message.Severity is < ValidationSeverity.None or > ValidationSeverity.Error)
                throw new ArgumentOutOfRangeException($"messages[{index}]", $"Severity {message.Severity} is not Error, Warning, Info, or None.");

            list.Add(message);

            switch (message.Severity)
            {
                case ValidationSeverity.Error: err++; break;
                case ValidationSeverity.Warning: wrn++; break;
                case ValidationSeverity.Info: inf++; break;
            }
        }

        list.Sort(Compare);

        return new(err, wrn, inf, [.. list]);
    }

    /// <inheritdoc  cref="New(IEnumerable{IValidationMessage?})"/>
    [Pure]
    public static ValidationMessages New(ReadOnlySpan<IValidationMessage> messages) => New(messages.ToArray());

    /// <summary>Sorts based on Severity descending.</summary>
    [Pure]
    private static int Compare(IValidationMessage l, IValidationMessage r) => r.Severity.CompareTo(l.Severity);
}
