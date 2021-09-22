using FluentAssertions;
using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.Guarding;
using System;

namespace Must_specs
{
    public class Requires
    {
        [Test]
        public void Subject_not_to_be_null()
        {
            Func<object> create = () => ((TestModel)null).Must();
            create.Should().Throw<ArgumentNullException>();
        }
    }

    public class Guards_when
    {
        [Test]
        public void Must_Be_condition_is_met()
        {
            var model = new TestModel();
            model.Must().Be(condition: true, ValidationMessage.Error("This is wrong"))
                .Should().BeValid().WithoutMessages()
                .Value.Should().BeSameAs(model);
        }

        [Test]
        public void Must_NotBe_condition_is_not_met()
        {
            var model = new TestModel();
            model.Must().NotBe(condition: false, "This is wrong")
                .Should().BeValid().WithoutMessages()
                .Value.Should().BeSameAs(model);
        }

        [Test]
        public void Must_Exist_resolves_entity()
        {
            var model = new TestModel();
            model.Must().Exist(8, (m, id) => new object())
                .Should().BeValid().WithoutMessages()
                .Value.Should().BeSameAs(model);
        }
    }

    public class Yields_when
    {
        [Test]
        public void Must_Be_condition_is_not_met()
            => new TestModel().Must().Be(condition: false, ValidationMessage.Error("This is wrong"))
            .Should().BeInvalid().WithMessage(ValidationMessage.Error("This is wrong"));

        [Test]
        public void Must_NotBe_condition_is_met()
            => new TestModel().Must().NotBe(condition: true, "This is wrong")
            .Should().BeInvalid().WithMessage(ValidationMessage.Error("This is wrong"));

        [Test]
        public void Must_Exist_does_not_resolve_entity()
          => new TestModel().Must().Exist(666, (m, id) => (object)null)
            .Should().BeInvalid().WithMessage(ValidationMessage.Error("Entity with ID 666 could not be found."));
    }

    public class ToString_as_type
    {
        [Test]
        public void Displays_type_based_on_TSubject()
            => new TestModel().Must().ToString().Should().Be("Must<System.Object>");
    }

    internal class TestModel { }
}
