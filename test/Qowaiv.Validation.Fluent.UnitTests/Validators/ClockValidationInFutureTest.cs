using FluentValidation;
using NUnit.Framework;
using Qowaiv.Globalization;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using Qowaiv.Validation.TestTools;
using System.Globalization;

namespace Qowaiv.Validation.Fluent.UnitTests.Validators
{
    public class ClockValidationInFutureTest
    {
        [Test]
        public void InFuture_DateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<DateInFutureValidator, DateModel>(model);
            }
        }

        [Test]
        public void InFuture_DateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new DateTimeModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<DateTimeInFutureValidator, DateTimeModel>(model);
            }
        }

        [Test]
        public void InFuture_NullableDateSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<NullableDateInFutureValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void InFuture_NullableDateTimeSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 12) };
                FluentValidatorAssert.IsValid<NullableDateTimeInFutureValidator, NullableDateTimeModel>(model);
            }
        }

        [Test]
        public void InFuture_NullableDateNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateInFutureValidator, NullableDateModel>(model);
            }
        }

        [Test]
        public void InFuture_NullableDateTimeNotSet_IsValid()
        {
            using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
            {
                var model = new NullableDateTimeModel { Prop = null };
                FluentValidatorAssert.IsValid<NullableDateTimeInFutureValidator, NullableDateTimeModel>(model);
            }
        }

        [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the future.", "en-GB")]
        public void InFuture_Date(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateModel { Prop = new Date(2017, 06, 11) };

                    FluentValidatorAssert.WithErrors<DateInFutureValidator, DateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the future.", "en-GB")]
        public void InFuture_DateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new DateTimeModel { Prop = new Date(2017, 06, 11) };

                    FluentValidatorAssert.WithErrors<DateTimeInFutureValidator, DateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the future.", "en-GB")]
        public void InFuture_NullableDate(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateModel { Prop = new Date(2017, 06, 11) };

                    FluentValidatorAssert.WithErrors<NullableDateInFutureValidator, NullableDateModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }

        [TestCase("'Prop' moet in de toekomst liggen.", "nl-NL")]
        [TestCase("'Prop' should be in the future.", "en-GB")]
        public void InFuture_NullableDateTime(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
                using (Clock.SetTimeForCurrentThread(() => new Date(2017, 06, 11)))
                {
                    var model = new NullableDateTimeModel { Prop = new Date(2017, 06, 11) };

                    FluentValidatorAssert.WithErrors<NullableDateTimeInFutureValidator, NullableDateTimeModel>(model,
                        ValidationMessage.Error(message, "Prop"));
                }
            }
        }


        public class DateInFutureValidator : FluentModelValidator<DateModel>
        {
            public DateInFutureValidator() => RuleFor(m => m.Prop).InFuture();
        }

        public class DateTimeInFutureValidator : FluentModelValidator<DateTimeModel>
        {
            public DateTimeInFutureValidator() => RuleFor(m => m.Prop).InFuture();
        }

        public class NullableDateInFutureValidator : FluentModelValidator<NullableDateModel>
        {
            public NullableDateInFutureValidator() => RuleFor(m => m.Prop).InFuture();
        }

        public class NullableDateTimeInFutureValidator : FluentModelValidator<NullableDateTimeModel>
        {
            public NullableDateTimeInFutureValidator() => RuleFor(m => m.Prop).InFuture();
        }
    }
}
