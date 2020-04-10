using NUnit.Framework;
using Qowaiv.Validation.TestTools;
using System.Globalization;
using System.Threading.Tasks;

namespace Qowaiv.Validation.Abstractions.UnitTests
{
    public class ResultActTest
    {
        internal static Result<TestModel> ValidModel() => Result.For(new TestModel());
        internal static Result<TestModel> InvalidModel() => Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(InvalidModel)));
        internal static Result<TestModel> NullModel() => Result.For<TestModel>(null);

        internal static Task<Result<TestModel>> ValidModelAsync() => Task.FromResult(Result.For(new TestModel()));
        internal static Task<Result<TestModel>> InvalidModelAsync() => Task.FromResult(Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(InvalidModelAsync))));
        internal static Task<Result<TestModel>> NullModelAsync() => Task.FromResult(Result.For<TestModel>(null));
        internal static Task<Result<TestModel>> NullTaskAsync() => Task.FromResult<Result<TestModel>>(null);

        [Test] 
        public void Act_NullModelInvalidAction_Valid()
        {
            var result = NullModel().Act(m => m.InvalidAction());
            ValidationMessageAssert.IsValid(result);
        }
        [Test]
        public async Task ActAsync_NullModelInvalidActionAsync_Valid()
        {
            var result = await NullModel().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.IsValid(result);
        }

        [Test]
        public async Task ActAsync_NullModelAsyncInvalidAction_Valid()
        {
            var result = await NullModelAsync().ActAsync(m => m.InvalidAction());
            ValidationMessageAssert.IsValid(result);
        }
        [Test]
        public async Task ActAsync_NullModelAsyncInvalidActionAsync_Valid()
        {
            var result = await NullModelAsync().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.IsValid(result);
        }

        [Test]
        public async Task ActAsync_NullTaskAsyncInvalidAction_Valid()
        {
            var result = await NullTaskAsync().ActAsync(m => m.InvalidAction());
            ValidationMessageAssert.IsValid(result);
        }
        [Test]
        public async Task ActAsync_NullTaskAsyncInvalidActionAsync_Valid()
        {
            var result = await NullTaskAsync().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.IsValid(result);
        }

        [Test]
        public void Act_InvalidModel_WithErrors()
        {
            var result = InvalidModel().Act(m => m.InvalidAction());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModel"));
        }
        [Test]
        public void Act_ValidModelValidAction_Act()
        {
            var result = ValidModel().Act(m => m.ValidAction());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public void Act_ValidModelInvalidAction_WithErrors()
        {
            var result = ValidModel().Act(m => m.InvalidAction());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidAction"));
        }

        [Test]
        public async Task ActAsync_InvalidModel_WithErrors()
        {
            var result = await InvalidModel().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModel"));
        }
        [Test]
        public async Task ActAsync_ValidModelValidActionAsync_ActAsync()
        {
            var result = await ValidModel().ActAsync(m => m.ValidActionAsync());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public async Task ActAsync_ValidModelInvalidActionAsync_WithErrors()
        {
            var result = await ValidModel().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidActionAsync"));
        }

        [Test]
        public async Task Act_InvalidModelAsync_WithErrors()
        {
            var result = await InvalidModelAsync().ActAsync(m => m.InvalidAction());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModelAsync"));
        }
        [Test]
        public async Task Act_ValidModelAsyncValidAction_Act()
        {
            var result = await ValidModelAsync().ActAsync(m => m.ValidAction());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public async Task Act_ValidModelAsyncInvalidAction_WithErrors()
        {
            var result = await ValidModelAsync().ActAsync(m => m.InvalidAction());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidAction"));
        }

        [Test]
        public async Task Act_InvalidModelAsyncInvalidActionAsync_WithErrors()
        {
            var result = await InvalidModelAsync().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModelAsync"));
        }
        [Test]
        public async Task Act_ValidModelAsyncValidActionAsync_Act()
        {
            var result = await ValidModelAsync().ActAsync(m => m.ValidActionAsync());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public async Task Act_ValidModelAsyncInvalidActionAsync_WithErrors()
        {
            var result = await ValidModelAsync().ActAsync(m => m.InvalidActionAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidActionAsync"));
        }

        [Test]
        public void Act_InvalidModelInvalidResult_WithErrors()
        {
            var result = InvalidModel().Act(m => m.InvalidResult());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModel"));
        }
        [Test]
        public void Act_ValidModelValidResult_Act()
        {
            var result = ValidModel().Act(m => m.ValidResult());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public void Act_ValidModelInvalidResult_WithErrors()
        {
            var result = ValidModel().Act(m => m.InvalidResult());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidResult"));
        }

        [Test]
        public async Task ActAsync_InvalidModelInvalidResultAsync_WithErrors()
        {
            var result = await InvalidModel().ActAsync(m => m.InvalidResultAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModel"));
        }
        [Test]
        public async Task ActAsync_ValidModelValidResultAsync_ActAsync()
        {
            var result = await ValidModel().ActAsync(m => m.ValidResultAsync());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public async Task ActAsync_ValidModelInvalidResultAsync_WithErrors()
        {
            var result = await ValidModel().ActAsync(m => m.InvalidResultAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidResultAsync"));
        }

        [Test]
        public async Task Act_InvalidModelAsyncInvalidResult_WithErrors()
        {
            var result = await InvalidModelAsync().ActAsync(m => m.InvalidResult());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModelAsync"));
        }
        [Test]
        public async Task Act_ValidModelAsyncValidResult_Act()
        {
            var result = await ValidModelAsync().ActAsync(m => m.ValidResult());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public async Task Act_ValidModelAsyncInvalidResult_WithErrors()
        {
            var result = await ValidModelAsync().ActAsync(m => m.InvalidResult());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidResult"));
        }

        [Test]
        public async Task Act_InvalidModelAsyncInvalidResultAsync_WithErrors()
        {
            var result = await InvalidModelAsync().ActAsync(m => m.InvalidResultAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidModelAsync"));
        }
        [Test]
        public async Task Act_ValidModelAsyncValidResultAsync_Act()
        {
            var result = await ValidModelAsync().ActAsync(m => m.ValidResultAsync());
            Assert.AreEqual(1, result.Value.Actions);
        }
        [Test]
        public async Task Act_ValidModelAsyncInvalidResultAsync_WithErrors()
        {
            var result = await ValidModelAsync().ActAsync(m => m.InvalidResultAsync());
            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidResultAsync"));
        }
    }

    internal class TestModel
    {
        public int Actions { get; private set; }

        public Result<TestModel> ValidAction()
        {
            Actions++;
            return this;
        }
        public Result<TestModel> InvalidAction()
        {
            Actions = -Actions;
            return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(InvalidAction)));
        }

        public Task<Result<TestModel>> ValidActionAsync()
        {
            Actions++;
            return Task.FromResult(Result.For(this));
        }
        public Task<Result<TestModel>> InvalidActionAsync()
        {
            Actions = -Actions;
            return Task.FromResult(Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(InvalidActionAsync))));
        }

        public Result ValidResult()
        {
            Actions++;
            return Result.OK;
        }
        public Result InvalidResult()
        {
            Actions = -Actions;
            return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(InvalidResult)));
        }

        public Task<Result> ValidResultAsync()
        {
            Actions++;
            return Task.FromResult(Result.OK);
        }
        public Task<Result> InvalidResultAsync()
        {
            Actions = -Actions;
            return Task.FromResult(Result.WithMessages(ValidationMessage.Error(nameof(InvalidResultAsync))));
        }

        public override string ToString() => Actions.ToString(CultureInfo.InvariantCulture);
    }
}
