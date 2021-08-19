using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Qowaiv.Validation.Abstractions
{
    internal static class TaskExtensions
    {
        /// <summary>Continue the awaitable <see cref="Task"/> on any context, not necessarily the captured one.</summary>
        public static ConfiguredTaskAwaitable<T> ContinueOnAnyContext<T>(this Task<T> task)
            => task.ConfigureAwait(continueOnCapturedContext: false);
    }
}
