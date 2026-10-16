namespace Qowaiv.Validation.Abstractions;

public static class Results
{
    extension(IEnumerable<Result> results)
    {
        /// <summary>Combines the results.</summary>
        [Pure]
        public Result Combine()
            => new([.. results.SelectMany(r => r.Messages)]);
    }

    extension(IEnumerable<AsyncResult> results)
    {

        /// <summary>Combines the results.</summary>
        [Pure]
        public AsyncResult Combine()
        {
            var completedTasks = await Task.WhenAll(results);
        }
    }
}
