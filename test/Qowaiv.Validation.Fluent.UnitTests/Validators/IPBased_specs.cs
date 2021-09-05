﻿using FluentAssertions;
using NUnit.Framework;
using Qowaiv;
using Qowaiv.Globalization;
using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.Fluent.UnitTests.Models;
using System.Globalization;

namespace Validation.IPBased_specs
{
    public class Valid_for
    {
        [Test]
        public void NoIPBase_IsValid()
            => new NoIPBasedEmailAddressModel { Email = EmailAddress.Parse("test@qowaiv.org") }
            .Should().BeValidFor(new NoIPBasedEmailAddressModelValidator());

        [TestCase("'Email' has a IP address based domain.", "en-GB")]
        [TestCase("'Email' heeft een IP-adres als domein.", "nl-BE")]
        public void IPBased(string message, CultureInfo culture)
        {
            using (culture.Scoped())
            {
               new NoIPBasedEmailAddressModel { Email = EmailAddress.Parse("qowaiv@172.16.254.1") }
               .Should().BeInvalidFor(new NoIPBasedEmailAddressModelValidator())
               .WithMessage(ValidationMessage.Error(message, "Email"));
            }
        }
    }
}
