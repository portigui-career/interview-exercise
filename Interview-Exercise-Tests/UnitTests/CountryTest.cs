using System;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class CountryTest
    {
        public class When_testing_a_class : TestFixtureBase
        {
            Country _actualResult;

            protected override void Act()
            {
                _actualResult = new Country {Code = "usa", Name = "united states"};
            }

            [Test]
            public void Should_return_upper_case()
            {
                Assert.That(_actualResult.Code, Is.EqualTo("USA"));
                Assert.That(_actualResult.Name, Is.EqualTo("UNITED STATES"));
            }
        }
    }
}