using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using System;
using System.Linq;
using static Qowaiv.Validation.Abstractions.UnitTests.Arrange;

namespace Result_specs
{
    public class ValidResult
    {
        [Test]
        public void Contains_no_ErrorMessages()
        {
            var result = Result.WithMessages<int>(Warning1, Info1, Info2);
            Assert.True(result.IsValid);
        }

        [Test]
        public void Has_access_to_Value()
        {
            var result = Result.For(2);
            Assert.AreEqual(2, result.Value);
        }
    }

    public class InvalidResult
    {
        [Test]
        public void Contains_at_least_one_ErrorMessage()
        {
            var result = Result.WithMessages(TestMessages.AsEnumerable());
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void Has_no_access_to_Value()
        {
            var result = Result.For(new object(), ValidationMessage.Error("Not OK"));
            Assert.Catch<InvalidModelException>(() => Console.WriteLine(result.Value));
        }
    }

    public class Filtering
    {
        [Test]
        public void Error_messages_is_done_via_the_Errors_property()
        {
            var result = Result.WithMessages(TestMessages);
            var act = result.Errors;
            var exp = new[] { Error1, Error2 };
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Warning_messages_is_done_via_the_Warnings_property()
        {
            var result = Result.WithMessages(TestMessages);
            var act = result.Warnings;
            var exp = new[] { Warning1, Warning2 };
            Assert.AreEqual(exp, act);
        }

        [Test]
        public void Info_messages_are_is_done_via_the_Infos_property()
        {
            var result = Result.WithMessages(TestMessages);
            var act = result.Infos;
            var exp = new[] { Info1, Info2 };
            Assert.AreEqual(exp, act);
        }
    }

    public class Casting
    {
        [Test]
        public void Implicit_from_T_to_Result_of_T_is_supported()
        {
            Result<int> result = 17;
            Assert.AreEqual(17, result.Value);
        }

        [Test]
        public void Expicit_from_Result_of_T_to_T_is_supported()
        {
            var result = Result.For(666);
            var actual = (int)result;
            Assert.AreEqual(666, actual);
        }
    }
}
