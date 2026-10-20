using Qowaiv.Validation.Abstractions;

namespace Abstractions.Result_Act_specs;

public class ValidModel
{
    private static Result<TestModel> Model() => Result.For(new TestModel());

    [Test]
    public void Act_on_transforming_function_is_executed_and_result_stays_valid()
        => Model().Act(m => m.TransformingFunction())
        .Should().BeValid()
        .Value.Actions.Should().Be(1);

    [Test]
    public void Act_on_value_transforming_function_is_executed_and_result_stays_valid()
        => Model().Act(m => m.ToModel())
        .Should().BeValid()
        .Value.Actions.Should().Be(1);

    [Test]
    public void Act_on_value_transforming_function_returning_null_throws()
        => Model().Invoking(m => m.Act(_ => (TestModel?)null))
        .Should().Throw<NoValue>();

    [Test]
    public void Act_on_value_transforming_function_of_valid_null_model_returning_null_throws()
        => Result.Null<TestModel>().Invoking(m => m.Act(model => model))
        .Should().Throw<NoValue>();

    [Test]
    public void Act_on_result_returning_function_with_Result_Null_is_valid_with_null_value()
        => Model().Act(_ => Result.Null<TestModel>())
        .Should().BeValid()
        .Value.Should().BeNull();

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
    public async Task ActAsync_on_async_result_returning_function_with_Result_Null_is_valid_with_null_value()
        => (await Model().ActAsync(_ => Result.Null<TestModel>().Async()))
        .Should().BeValid()
        .Value.Should().BeNull();

    [Test]
    public void Act_on_result_returning_function_of_valid_null_model_stays_valid()
        => Result.Null<TestModel>().Act(_ => Result.Null<TestModel>())
        .Should().BeValid()
        .Value.Should().BeNull();

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
    private static AsyncResult<TestModel> ModelAsync() => Result.Async(new TestModel());

    [Test]
    public async Task ActAsync_on_sync_transforming_function_is_executed_and_result_stays_valid()
        => (await ModelAsync().ActAsync(m => m.TransformingFunction()))
        .Should().BeValid()
        .Value.Actions.Should().Be(1);

    [Test]
    public async Task ActAsync_on_sync_value_transforming_function_is_executed_and_result_stays_valid()
        => (await ModelAsync().ActAsync(m => m.ToModel()))
        .Should().BeValid()
        .Value.Actions.Should().Be(1);

    [Test]
    public async Task ActAsync_on_sync_value_transforming_function_returning_null_throws()
    {
        var act = async () => await ModelAsync().ActAsync(_ => (TestModel?)null);
        await act.Should().ThrowAsync<NoValue>();
    }

    [Test]
    public async Task ActAsync_on_async_result_returning_function_with_Result_Null_is_valid_with_null_value()
        => (await ModelAsync().ActAsync(_ => Result.Null<TestModel>().Async()))
        .Should().BeValid()
        .Value.Should().BeNull();

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
    private static Result<TestModel> Model() => Result.WithMessages<TestModel>(ValidationMessage.Error("FailingModel"));

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
    private static AsyncResult<TestModel> ModelAsync() => Result.WithMessages<TestModel>(ValidationMessage.Error("FailingModelAsync")).Async();

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
    public void Is_equal_to_Act_on_function_with_result()
    {
        Result<TestModel> result = new TestModel();
        result |= (m => m.NonfailingFunction());

        result.Should().BeValid().Value.Actions.Should().Be(1);
    }

    [Test]
    public void Is_equal_to_Act_on_transforming_function_with_result()
    {
        Result<TestModel> result = new TestModel();
        result |= (m => m.TransformingFunction());

        result.Should().BeValid().Value.Actions.Should().Be(1);
    }

    [Test]
    public void Is_equal_to_Act_on_value_transforming_function()
    {
        Result<TestModel> result = new TestModel();
        result |= (m => m.ToModel());

        result.Should().BeValid().Value.Actions.Should().Be(1);
    }

    [Test]
    public async Task Is_equal_to_ActAsync_on_function()
    {
        Result<TestModel> result = new TestModel();
        var promise = await (result | (m => m.NonfailingFunctionAsync()));
        promise.Should().BeValid().Value.Actions.Should().Be(1);
    }

    [Test]
    public async Task Is_equal_to_ActAsync_on_value_transforming_function()
    {
        AsyncResult<TestModel> result = Result.Async(new TestModel());
        var promise = await (result | (m => m.ToModel()));
        promise.Should().BeValid().Value.Actions.Should().Be(1);
    }

    [Test]
    public void Is_equal_to_Act_on_action()
    {
        Result<TestModel> result = new TestModel();
        result |= (m => m.NonfailingAction());

        result.Should().BeValid().Value.Actions.Should().Be(1);
    }
}

internal class TestModel(int actions = 0)
{
    public int Actions { get; private set; } = actions;

    public Result<TestModel> TransformingFunction()
        => new TestModel(Actions + 1);

    public TestModel ToModel()
        => new(Actions + 1);

    public Result<TestModel> NonfailingFunction()
        => TransformingFunction();

    public Result<TestModel> FailingFunction()
    {
        Actions = -Actions;
        return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(FailingFunction)));
    }

    public AsyncResult<TestModel> NonfailingFunctionAsync()
        => Result.Async(new TestModel(Actions + 1));

    public AsyncResult<TestModel> FailingFunctionAsync()
    {
        Actions = -Actions;
        return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(FailingFunctionAsync))).Async();
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

    public AsyncResult NonfailingActionAsync()
    {
        Actions++;
        return Result.OK.Async();
    }
    public AsyncResult FailingActionAsync()
    {
        Actions = -Actions;
        return Task.FromResult(Result.WithMessages(ValidationMessage.Error(nameof(FailingActionAsync))));
    }

    public override string ToString() => Actions.ToString(CultureInfo.InvariantCulture);
}
