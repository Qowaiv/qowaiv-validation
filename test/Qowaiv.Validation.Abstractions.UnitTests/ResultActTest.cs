using NUnit.Framework;
using Qowaiv.TestTools.Validiation;
using System.Threading.Tasks;

namespace Qowaiv.Validation.Abstractions.UnitTests
{
    public class ResultActTest
    {
        [Test]
        public void Act_MultipleTimes_WithErrors()
        {
            var model = new TestModel();
            var result = model.UpdateValue()
                .Act(m => m.UpdateValue())
                .Act(m => m.UpdateValue())
                .Act(m => m.UpdateValue())
                .Act(m => m.OkResult())
                .Act(m => m.InvalidUpdate())
                .Act(m => m.InvalidResult())
                .Act(m => m.OkResult());

            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidUpdate"));
        }

        [Test]
        public async Task ActAsync_MultipleTimes_WithErrors()
        {
            var model = new TestModel();
            var result = await model.UpdateValueAsync()
                .ActAsync(m => m.UpdateValueAsync())
                .ActAsync(m => m.InvalidUpdate());

            ValidationMessageAssert.WithErrors(result, ValidationMessage.Error("InvalidUpdate"));
        }
    }

    internal class TestModel
    {
        public int Value { get; private set; }

        public Result InvalidResult()
        {
            Value++;
            return Result.WithMessages(ValidationMessage.Error(nameof(InvalidResult)));
        }
        public Result OkResult()
        {
            Value += 2;
            return Result.OK;
        }
        public Result<TestModel> UpdateValue()
        {
            Value++;
            return this;
        }
        public Result<TestModel> InvalidUpdate()
        {
            Value++;
            return Result.WithMessages<TestModel>(ValidationMessage.Error(nameof(InvalidUpdate)));
        }

        public Task<Result<TestModel>> UpdateValueAsync()
        {
            Value++;
            return Task.FromResult(Result.For(this));
        }

        public override string ToString() => Value.ToString();
    }

}
