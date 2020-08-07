using FluentValidation;
using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class ClockValidationNotInFutureTest
    {
        [Test]
        public void NotInFuture_DateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateModel { Prop = new Date(2017, 06, 11) };
                FluentValidatorAssert.IsValid<DateNotInFutureValidator, DateModel>(model);
            }
        }

        [Test]
        public void NotInFuture_DateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateTimeModel { Prop = new Date(2017, 06, 11) };
                FluentValidatorAssert.IsValid<DateTimeNotInFutureValidator, DateTimeModel>(model);
            }
        }

        [Test]
        public void NotInFuture_NullableDateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = new Date(2017, 06, 11) };
                FluentValidatorAssert.IsValid<NullableDateNotInFutureValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void NotInFuture_NullableDateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 11) };
                FluentValidatorAssert.IsValid<NullableDateTimeNotInFutureValidator, NullableDateTimeModel>(model);
            }
        }

        [Test]
        public void NotInFuture_NullableDateNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateNotInFutureValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void NotInFuture_NullableDateTimeNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateTimeNotInFutureValidator, NullableDateTimeModel>(model);
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void NotInFuture_Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<DateNotInFutureValidator, DateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void NotInFuture_DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateTimeModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<DateTimeNotInFutureValidator, DateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void NotInFuture_NullableDate(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<NullableDateNotInFutureValidator, NullableDateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' mag niet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should not be in the future.", "en-GB")]
        public void NotInFuture_NullableDateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 12) };

                    FluentValidatorAssert.WithErrors<NullableDateTimeNotInFutureValidator, NullableDateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }


        public class DateNotInFutureValidator : ModelValidator<DateModel>
        {
            public DateNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
        }

        public class DateTimeNotInFutureValidator : ModelValidator<DateTimeModel>
        {
            public DateTimeNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
        }

        public class NullableDateNotInFutureValidator : ModelValidator<NullableDateModel>
        {
            public NullableDateNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
        }

        public class NullableDateTimeNotInFutureValidator : ModelValidator<NullableDateTimeModel>
        {
            public NullableDateTimeNotInFutureValidator() => RuleFor(m => m.Prop).NotInFuture();
        }
    }
}
