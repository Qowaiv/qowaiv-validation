using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.TestTools;
using System.Threading.Tasks;

namespace Result_Act_Mutable_Context_specs
{
    public class Task_Null
    {
        [Test]
        public async Task ActAsync_with_sync_is_never_triggered()
        {
            var result = await Context.NullTask.ActAsync(Actions.Failure, Context.Update);
            ValidationMessageAssert.IsValid(result);
        }

        [Test]
        public async Task ActAsync_with_async_is_never_triggered()
        {
            var result = await Context.NullTask.ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.IsValid(result);
        }
    }

    public class Task_Null_Context
    {
        [Test]
        public async Task ActAsync_with_sync_is_never_triggered()
        {
            var result = await Context.Null.AsTask().ActAsync(Actions.Failure, Context.Update);
            ValidationMessageAssert.IsValid(result);
        }

        [Test]
        public async Task ActAsync_with_async_is_never_triggered()
        {
            var result = await Context.Null.AsTask().ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.IsValid(result);
        }
    }

    public class Null_Model
    {
        [Test]
        public void Act_is_never_triggered()
        {
            var result = Context.Null.Act(Actions.Failure, Context.Update);
            ValidationMessageAssert.IsValid(result);
        }

        [Test]
        public async Task ActAsync_is_never_triggered()
        {
            var result = await Context.Null.ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.IsValid(result);
        }
    }

    public class Valid_Model
    {
        [Test]
        public void Act_on_success_is_executed_and_result_stays_valid()
        {
            var result = Context.Valid.Act(Actions.Success, Context.Update);
            ValidationMessageAssert.IsValid(result);
            Assert.That(result.Value.Updated, Is.True);
        }

        [Test]
        public void Act_on_failing_makes_result_invalid()
        {
            var result = Context.Valid.Act(Actions.Failure, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("Failure"));
        }

        [Test]
        public async Task ActAsync_on_success_is_executed_and_result_stays_valid()
        {
            var result = await Context.Valid.ActAsync(Actions.SuccessAsync, Context.Update);
            ValidationMessageAssert.IsValid(result);
            Assert.That(result.Value.Updated, Is.True);
        }

        [Test]
        public async Task ActAsync_on_failing_makes_result_invalid()
        {
            var result = await Context.Valid.ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("FailureAsync"));
        }
    }

    public class Task_Valid_Model
    {
        [Test]
        public async Task ActAsync_on_sync_success_is_executed_and_result_stays_valid()
        {
            var result = await Context.Valid.AsTask().ActAsync(Actions.Success, Context.Update);
            ValidationMessageAssert.IsValid(result);
            Assert.That(result.Value.Updated, Is.True);
        }

        [Test]
        public async Task ActAsync_on_async_success_is_executed_and_result_stays_valid()
        {
            var result = await Context.Valid.AsTask().ActAsync(Actions.SuccessAsync, Context.Update);
            ValidationMessageAssert.IsValid(result);
            Assert.That(result.Value.Updated, Is.True);
        }

        [Test]
        public async Task ActAsync_on_sync_failing_makes_result_invalid()
        {
            var result = await Context.Valid.AsTask().ActAsync(Actions.Failure, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("Failure"));
        }

        [Test]
        public async Task ActAsync_on_async_failing_makes_result_invalid()
        {
            var result = await Context.Valid.AsTask().ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("FailureAsync"));
        }
    }

    public class Invalid_Model
    {
        [Test]
        public void Act_is_never_triggered()
        {
            var result = Context.Invalid.Act(Actions.Failure, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidContext"));
        }

        [Test]
        public async Task ActAsync_is_never_triggered()
        {
            var result = await Context.Invalid.ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidContext"));
        }
    }

    public class Task_Invalid_Model
    {
        [Test]
        public async Task ActAsync_with_sync_is_never_triggered()
        {
            var result = await Context.Invalid.AsTask().ActAsync(Actions.Failure, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidContext"));
        }

        [Test]
        public async Task ActAsync_with_async_is_never_triggered()
        {
            var result = await Context.Invalid.AsTask().ActAsync(Actions.FailureAsync, Context.Update);
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidContext"));
        }
    }

    internal class Context
    {
        public static Task<Result<Context>> NullTask => Task.FromResult<Result<Context>>(null);
        public static Result<Context> Null => Result.For<Context>(null);
        public static Result<Context> Valid => Result.For(new Context());
        public static Result<Context> Invalid => Result.WithMessages<Context>(ValidationMessage.Error("InvalidContext"));

        public string Value { get; set; }
        public bool Updated => Value is { };

        public static void Update(Context context, string value) => context.Value = value;
    }

    internal static class Actions
    {
        public static Result<string> Success(Context context)
            => Result.For(nameof(Success));

        public static Task<Result<string>> SuccessAsync(Context context)
            => Result.For(nameof(SuccessAsync)).AsTask();
        
        public static Result<string> Failure(Context context) 
            => Result.WithMessages<string>(ValidationMessage.Error(nameof(Failure)));

        public static Task<Result<string>> FailureAsync(Context context)
            => Result.WithMessages<string>(ValidationMessage.Error(nameof(FailureAsync))).AsTask();
    }
}
