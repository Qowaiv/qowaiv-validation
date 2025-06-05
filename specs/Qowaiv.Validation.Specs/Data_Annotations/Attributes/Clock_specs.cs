using AwesomeAssertions.Extensions;
using Qowaiv.Validation.DataAnnotations;

namespace Data_annotations.Attributes.Clock_specs;

public class InFuture
{
    public class Guards
    {
        [TestCase("2017-06-11T06:16")]
        [TestCase("2048-03-14T15:00")]
        public void DateTime(DateTime d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-11T06:16+00:00")]
        [TestCase("2048-03-14T15:00")]
        public void DateTimeOffset(DateTimeOffset d)
        {
            using var clock = Clock.SetTimeAndTimeZoneForCurrentContext(() => 11.June(2017).At(06, 15), TimeZoneInfo.Utc);
            new InFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-12")]
        [TestCase("2048-03-14")]
        public void Date(Date d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-12")]
        [TestCase("2048-03-14")]
        public void Date(DateOnly d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase(2018)]
        [TestCase(2048)]
        public void Year(Year year)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InFutureAttribute().IsValid(year).Should().BeTrue();
        }
    }

    [TestCase("nl", "De waarde van het ExpiryDate veld moet in de toekomst liggen.")]
    [TestCase("en", "The value of the ExpiryDate field should be in the future.")]
    public void fails_with_message(CultureInfo culture, string message)
    {
        using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
        using var _ = culture.Scoped();
        new Model.ClockDependent.InFuture { ExpiryDate = new(2017, 06, 10) }
            .ValidateAnnotations()
            .Should()
            .BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "ExpiryDate"));
    }
}
public class InPast
{
    public class Guards
    {
        [TestCase("2017-06-11T06:14")]
        [TestCase("1999-03-14T15:00")]
        public void DateTime(DateTime d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-11T06:14+00:00")]
        [TestCase("1999-03-14T15:00")]
        public void DateTimeOffset(DateTimeOffset d)
        {
            using var clock = Clock.SetTimeAndTimeZoneForCurrentContext(() => 11.June(2017).At(06, 15), TimeZoneInfo.Utc);
            new InPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-10")]
        [TestCase("1999-03-14")]
        public void Date(Date d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-10")]
        [TestCase("1999-03-14")]
        public void Date(DateOnly d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase(2016)]
        [TestCase(1999)]
        public void Year(Year year)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new InPastAttribute().IsValid(year).Should().BeTrue();
        }
    }

    [TestCase("nl", "De waarde van het YearOfConstruction veld moet in het verleden liggen.")]
    [TestCase("en", "The value of the YearOfConstruction field should be in the past.")]
    public void fails_with_message(CultureInfo culture, string message)
    {
        using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
        using var _ = culture.Scoped();
        new Model.ClockDependent.InPast { YearOfConstruction = 2017.CE() }
            .ValidateAnnotations()
            .Should()
            .BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "YearOfConstruction"));
    }
}

public class NotInFuture
{
    public class Guards
    {
        [TestCase("2017-06-11T06:14")]
        [TestCase("2017-06-11T06:15")]
        [TestCase("1999-03-14T15:00")]
        public void DateTime(DateTime d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-11T06:14+00:00")]
        [TestCase("2017-06-11T06:15+00:00")]
        [TestCase("1999-03-14T15:00")]
        public void DateTimeOffset(DateTimeOffset d)
        {
            using var clock = Clock.SetTimeAndTimeZoneForCurrentContext(() => 11.June(2017).At(06, 15), TimeZoneInfo.Utc);
            new NotInFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-10")]
        [TestCase("2017-06-11")]
        [TestCase("1999-03-14")]
        public void Date(Date d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-10")]
        [TestCase("2017-06-11")]
        [TestCase("1999-03-14")]
        public void Date(DateOnly d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInFutureAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase(2016)]
        [TestCase(2017)]
        [TestCase(1999)]
        public void Year(Year year)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInFutureAttribute().IsValid(year).Should().BeTrue();
        }
    }

    [TestCase("nl", "De waarde van veld DateOfBirth mag niet in de toekomst liggen.")]
    [TestCase("en", "The value of the DateOfBirth field should not be in the future.")]
    public void fails_with_message(CultureInfo culture, string message)
    {
        using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
        using var _ = culture.Scoped();
        new Model.ClockDependent.NotInFuture { DateOfBirth = new(2018, 06, 12) }
            .ValidateAnnotations()
            .Should()
            .BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }
}

public class NotInPast
{
    public class Guards
    {
        [TestCase("2017-06-11T06:15")]
        [TestCase("2017-06-11T06:16")]
        [TestCase("2048-03-14T15:00")]
        public void DateTime(DateTime d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-11T06:15+00:00")]
        [TestCase("2017-06-11T06:16+00:00")]
        [TestCase("2048-03-14T15:00")]
        public void DateTimeOffset(DateTimeOffset d)
        {
            using var clock = Clock.SetTimeAndTimeZoneForCurrentContext(() => 11.June(2017).At(06, 15), TimeZoneInfo.Utc);
            new NotInPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-11")]
        [TestCase("2017-06-12")]
        [TestCase("2048-03-14")]
        public void Date(Date d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase("2017-06-11")]
        [TestCase("2017-06-12")]
        [TestCase("2048-03-14")]
        public void Date(DateOnly d)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInPastAttribute().IsValid(d).Should().BeTrue();
        }

        [TestCase(2017)]
        [TestCase(2018)]
        [TestCase(2048)]
        public void Year(Year year)
        {
            using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
            new NotInPastAttribute().IsValid(year).Should().BeTrue();
        }
    }

    [TestCase("nl", "De waarde van veld ExpiryDate mag niet in het verleden liggen.")]
    [TestCase("en", "The value of the ExpiryDate field should not be in the past.")]
    public void fails_with_message(CultureInfo culture, string message)
    {
        using var clock = Clock.SetTimeForCurrentContext(() => 11.June(2017).At(06, 15));
        using var _ = culture.Scoped();
        new Model.ClockDependent.NotInPast { ExpiryDate = new(2017, 06, 10) }
            .ValidateAnnotations()
            .Should()
            .BeInvalid()
            .WithMessage(ValidationMessage.Error(message, "ExpiryDate"));
    }
}

public class Years
{
    [TestCase(null)]
    [TestCase("?")]
    public void are_ignored_when(Year year)
        => new InPastAttribute().IsValid(year).Should().BeTrue();
}
