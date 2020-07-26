using NUnit.Framework;
using Qowaiv.TestTools;
using Qowaiv.Validation.Abstractions;

namespace Validation_message_specs
{
    public class None
    {
        [Test]
        public void Has_an_empty_string_representation()
        {
            var message = ValidationMessage.None;
            Assert.AreEqual("", message.ToString());
        }
    }

    public class Serialization
    {
        [Test]
        public void On_an_info_message_keeps_all_data()
        {
            var message = ValidationMessage.Info("Can be serialized", "Prop");

            var actual = SerializationTest.SerializeDeserialize(message);

            Assert.AreEqual(message.Severity, actual.Severity);
            Assert.AreEqual(message.Message, actual.Message);
            Assert.AreEqual(message.PropertyName, actual.PropertyName);
            Assert.AreEqual("INF: Property: Prop, Can be serialized", message.ToString());
        }

        [Test]
        public void On_a_warning_message_keeps_all_data()
        {
            var message = ValidationMessage.Warn("Can be serialized", "Prop");

            var actual = SerializationTest.SerializeDeserialize(message);

            Assert.AreEqual(message.Severity, actual.Severity);
            Assert.AreEqual(message.Message, actual.Message);
            Assert.AreEqual(message.PropertyName, actual.PropertyName);
            Assert.AreEqual("WRN: Property: Prop, Can be serialized", message.ToString());
        }

        [Test]
        public void On_an_error_message_keeps_all_data()
        {
            var message = ValidationMessage.Error("Can be serialized", "Prop");

            var actual = SerializationTest.SerializeDeserialize(message);

            Assert.AreEqual(message.Severity, actual.Severity);
            Assert.AreEqual(message.Message, actual.Message);
            Assert.AreEqual(message.PropertyName, actual.PropertyName);
            Assert.AreEqual("ERR: Property: Prop, Can be serialized", message.ToString());
        }
    }
}
