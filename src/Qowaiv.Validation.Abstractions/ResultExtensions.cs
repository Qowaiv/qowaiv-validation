using System;
using System.Linq;
using System.Threading.Tasks;

namespace Qowaiv.Validation.Abstractions
{
    public static class ResultExtensions
    {
        public static async Task<Result<TModel>> ActAsync<TModel>(this Task<Result<TModel>> promise, Func<TModel, Result> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);
            if (!result.IsValid)
            {
                return result;
            }
            var messages = result.Messages.ToList();

            var act = action(result.Value);

            messages.AddRange(act.Messages);

            return Result.For(result.Value, messages);
        }
    
        public static async Task<Result<TOther>> ActAsync<TModel, TOther>(this Task<Result<TModel>> promise, Func<TModel, Result<TOther>> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);

            if (result is null)
            {
                return Result.WithMessages<TOther>();
            }
            if (!result.IsValid)
            {
                return Result.WithMessages<TOther>(result.Messages);
            }
            var messages = result.Messages.ToList();

            var outcome = action(result.Value);
            messages.AddRange(outcome.Messages);

            return outcome.IsValid
                ? Result.For(outcome.Value, messages)
                : Result.WithMessages<TOther>(messages);
        }
        
        public static async Task<Result<TModel>> ActAsync<TModel>(this Task<Result<TModel>> promise, Func<TModel, Task<Result>> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);
            if(!result.IsValid)
            {
                return result;
            }
            var messages = result.Messages.ToList();

            var act = await action(result.Value).ConfigureAwait(false);

            messages.AddRange(act.Messages);

            return Result.For(result.Value, messages);
        }
        
        public static async Task<Result<TOther>> ActAsync<TModel, TOther>(this Task<Result<TModel>> promise, Func<TModel, Task<Result<TOther>>> action)
        {
            _ = Guard.NotNull(promise, nameof(promise));
            Guard.NotNull(action, nameof(action));

            var result = await promise.ConfigureAwait(false);
            
            if(result is null)
            {
                return Result.WithMessages<TOther>();
            }
            if (!result.IsValid)
            {
                return Result.WithMessages<TOther>(result.Messages);
            }
            var messages = result.Messages.ToList();

            var outcome = await action(result.Value).ConfigureAwait(false);
            messages.AddRange(outcome.Messages);

            return outcome.IsValid
                ? Result.For(outcome.Value, messages)
                : Result.WithMessages<TOther>(messages);
        }
    }
}
