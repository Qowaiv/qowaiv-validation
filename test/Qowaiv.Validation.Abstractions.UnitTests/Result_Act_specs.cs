using FluentAssertions;
using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.TestTools;
using System.Globalization;
using System.Threading.Tasks;

namespace Result_Act_specs
{
    public class ValidModel
    {
        internal static Result<TestModel> Model() => Result.For(new TestModel());

        [Test]
        public void Act_on_nonfailing_function_is_executed_and_result_stays_valid()
            => Model().Act(m => m.NonfailingFunction())
            .Should().BeValid()
            .Value.Actions.Should().Be(1);
    
        [Test]
        public void Act_on_failing_function_makes_result_invalid()
            => Model().Act(m => m.FailingFunction())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingFunction"));

        [Test]
        public async Task ActAsync_on_nonfailing_function_is_executed_and_result_stays_valid()
            => (await Model().ActAsync(m => m.NonfailingFunctionAsync()))
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public async Task ActAsync_on_failing_function_makes_result_invalid()
            => (await Model().ActAsync(m => m.FailingFunctionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingFunctionAsync"));

        [Test]
        public void Act_on_nonfailing_action_is_executed_and_result_stays_valid()
            => Model().Act(m => m.NonfailingAction())
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public void Act_on_failing_action_makes_result_invalid()
            => Model().Act(m => m.FailingAction())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingAction"));

        [Test]
        public async Task ActAsync_on_nonfailing_action_is_executed_and_result_stays_valid()
            => (await Model().ActAsync(m => m.NonfailingActionAsync()))
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public async Task ActAsync_on_failing_action_makes_result_invalid()
            => (await Model().ActAsync(m => m.FailingActionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingActionAsync"));
    }

    public class AsyncValidModel
    {
        internal static Task<Result<TestModel>> ModelAsync() => Result.For(new TestModel()).AsTask();

        [Test]
        public async Task ActAsync_on_sync_nonfailing_function_is_executed_and_result_stays_valid()
            => (await ModelAsync().ActAsync(m => m.NonfailingFunction()))
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public async Task ActAsync_on_async_nonfailing_function_is_executed_and_result_stays_valid()
            => (await ModelAsync().ActAsync(m => m.NonfailingFunctionAsync()))
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public async Task ActAsync_on_sync_failing_function_makes_result_invalid()
            => (await ModelAsync().ActAsync(m => m.FailingFunction()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingFunction"));

        [Test]
        public async Task ActAsync_on_async_failing_function_makes_result_invalid()
            => (await ModelAsync().ActAsync(m => m.FailingFunctionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingFunctionAsync"));

        [Test]
        public async Task ActAsync_on_sync_nonfailing_action_is_executed_and_result_stays_valid()
            => (await ModelAsync().ActAsync(m => m.NonfailingAction()))
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public async Task ActAsync_on_async_nonfailing_action_is_executed_and_result_stays_valid()
            => (await ModelAsync().ActAsync(m => m.NonfailingActionAsync()))
            .Should().BeValid()
            .Value.Actions.Should().Be(1);

        [Test]
        public async Task ActAsync_on_sync_failing_action_makes_result_invalid()
            => (await ModelAsync().ActAsync(m => m.FailingAction()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingAction"));

        [Test]
        public async Task ActAsync_on_async_failing_action_makes_result_invalid()
            => (await ModelAsync().ActAsync(m => m.FailingActionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingActionAsync"));
    }

    public class InvalidModel
    {
        internal static Result<TestModel> Model() => Result.WithMessages<TestModel>(ValidationMessage.Error("FailingModel"));

        [Test]
        public void Act_is_never_triggered()
            => Model().Act(m => m.FailingFunction())
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingModel"));

        [Test]
        public async Task ActAsync_is_never_triggered()
            => (await Model().ActAsync(m => m.FailingFunctionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingModel"));
    }

    public class AsyncInvalidModel
    {
        internal static Task<Result<TestModel>> ModelAsync() => Result.WithMessages<TestModel>(ValidationMessage.Error("FailingModelAsync")).AsTask();

        [Test]
        public async Task ActAsync_with_sync_function_is_never_triggered()
            => (await ModelAsync().ActAsync(m => m.FailingFunction()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingModelAsync"));

        [Test]
        public async Task ActAsync_with_async_function_is_never_triggered()
            => (await ModelAsync().ActAsync(m => m.FailingFunctionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingModelAsync"));

        [Test]
        public async Task ActAsync_with_sync_action_is_never_triggered()
            => (await ModelAsync().ActAsync(m => m.FailingAction()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingModelAsync"));

        [Test]
        public async Task ActAsync_with_async_action_is_never_triggered()
            => (await ModelAsync().ActAsync(m => m.FailingActionAsync()))
            .Should().BeInvalid()
            .WithMessage(ValidationMessage.Error("FailingModelAsync"));
    }

    public class Pipe_Symbol
    {
        [Test]
        public void Is_equal_to_Act_on_function()
        {
            Result<TestModel> result = new TestModel();
            result |= (m => m.NonfailingFunction());

            result.Should().BeValid().Value.Actions.Should().Be(1);
        }

        [Test]
        public void Is_equal_to_Act_on_action()
        {
            Result<TestModel> result = new TestModel();
            result |= (m => m.NonfailingAction());

            result.Should().BeValid().Value.Actions.Should().Be(1);
        }
    }

    internal class TestModel
    {
        public TestModel(int actions = 0) => Actions = actions;

        public int Actions { get; private set; }

        public Result<TestModel> NonfailingFunction()
        {
            return new TestModel(Actions + 1);
        }
        public Result<TestModel> FailingFunction()
        {
            Actions = -Actions;
            return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(FailingFunction)));
        }

        public Task<Result<TestModel>> NonfailingFunctionAsync()
            => Result.For(new TestModel(Actions + 1)).AsTask();

        public Task<Result<TestModel>> FailingFunctionAsync()
        {
            Actions = -Actions;
            return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(FailingFunctionAsync))).AsTask();
        }

        public Result NonfailingAction()
        {
            Actions++;
            return Result.OK;
        }
        public Result FailingAction()
        {
            Actions = -Actions;
            return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(FailingAction)));
        }

        public Task<Result> NonfailingActionAsync()
        {
            Actions++;
            return Task.FromResult(Result.OK);
        }
        public Task<Result> FailingActionAsync()
        {
            Actions = -Actions;
            return Task.FromResult(Result.WithMessages(ValidationMessage.Error(nameof(FailingActionAsync))));
        }

        public override string ToString() => Actions.ToString(CultureInfo.InvariantCulture);
    }
}
