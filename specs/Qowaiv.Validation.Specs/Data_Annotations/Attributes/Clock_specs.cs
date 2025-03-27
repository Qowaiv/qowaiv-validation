using FluentAssertions.Extensions;
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
}

public class Years
{
    [TestCase(null)]
    [TestCase("?")]
    public void are_ignored_when(Year year)
        => new InPastAttribute().IsValid(year).Should().BeTrue();
}
