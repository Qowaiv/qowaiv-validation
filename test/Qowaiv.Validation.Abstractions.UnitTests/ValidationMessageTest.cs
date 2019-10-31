﻿using NUnit.Framework;
using Qowaiv.TestTools;

namespace Qowaiv.Validation.Abstractions.UnitTests
{
    public class ValidationMessageTest
    {
        [Test]
        public void Serializable_SomeInfoMessage_Successful()
        {
            var message = ValidationMessage.Info("Can be serialized", "Prop");

            var actual = SerializationTest.SerializeDeserialize(message);

            Assert.AreEqual(message.Severity, actual.Severity);
            Assert.AreEqual(message.Message, actual.Message);
            Assert.AreEqual(message.PropertyName, actual.PropertyName);
        }

        [Test]
        public void Serializable_SomeWarningMessage_Successful()
        {
            var message = ValidationMessage.Warn("Can be serialized", "Prop");

            var actual = SerializationTest.SerializeDeserialize(message);

            Assert.AreEqual(message.Severity, actual.Severity);
            Assert.AreEqual(message.Message, actual.Message);
            Assert.AreEqual(message.PropertyName, actual.PropertyName);
        }

        [Test]
        public void Serializable_SomeErrorMessage_Successful()
        {
            var message = ValidationMessage.Error("Can be serialized", "Prop");

            var actual = SerializationTest.SerializeDeserialize(message);

            Assert.AreEqual(message.Severity, actual.Severity);
            Assert.AreEqual(message.Message, actual.Message);
            Assert.AreEqual(message.PropertyName, actual.PropertyName);
        }
    }
}
