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
        /// <summary>Represents the async results as tasks.</summary>
        [Pure]
        public IEnumerable<Task<Result>> AsTasks()
            => results.Select(r => r.AsTask());

        /// <summary>Combines the results.</summary>
        [Pure]
        public AsyncResult Combine()
        {
            async Task<Result> Excecute()
            {
                var promises = await Task.WhenAll(results.AsTasks());
                return new([.. promises.SelectMany(r => r.Messages)]);
            }

            return new(Excecute());
        }
    }
}
