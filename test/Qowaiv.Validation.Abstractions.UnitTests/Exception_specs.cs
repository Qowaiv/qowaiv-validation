using NUnit.Framework;
using Qowaiv.TestTools;
using Qowaiv.Validation.Abstractions;
using static Qowaiv.Validation.Abstractions.UnitTests.Arrange;

namespace Exception_specs
{
    public class InvalidModel
    {
        [Test]
        public void Contains_the_error_messages_in_the_ToString()
        {
            var str = InvalidModelException.For<int>(new IValidationMessage[]
            {
                ValidationMessage.None,
                ValidationMessage.Error("Not a prime", "_value"),
                ValidationMessage.Warn("Small", "_value"),
                ValidationMessage.Info("Not my favorite", "_value"),
                ValidationMessage.Error("Not a multiple of 17", "_value"),
            }).ToString();

            Assert.AreEqual(@"Qowaiv.Validation.Abstractions.InvalidModelException: The System.Int32 model can not be operated on as it is invalid.
Not a prime (_value)
Not a multiple of 17 (_value)
", str);
        }

        [Test]
        public void Serializes_the_error_messages()
        {
            var exception = InvalidModelException.For<int>(TestMessages);
            var actual = SerializationTest.SerializeDeserialize(exception);

            CollectionAssert.AreEqual(new []{ Error1, Error2 }, actual.Errors);
        }
    }
}
