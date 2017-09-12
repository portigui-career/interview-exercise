using System;
using Interview_Exercise;
using NUnit.Framework;

namespace Interview_Exercise_Tests.UnitTests
{
    public class ValidateCountryTest
    {
        public class Succes_with_acceptable_code : TestFixtureBase
        {
            protected override void Act()
            {
                var validateCountry = new ValidateCountryCommand();
                validateCountry.Execute(new Country { Code = "usa" });
            }

            [Test]
            public void Should_execute_with_success()
            {
                Assert.That(ActualException, Is.Null);
            }
        }

        public class When_invalid_code_length : TestFixtureBase
        {
            protected override void Act()
            {
                var validateCountry = new ValidateCountryCommand();
                validateCountry.Execute(new Country { Code = "usa1" });
            }

            [Test]
            public void Should_set_actual_exception()
            {
                AssertAll(
                    () => Assert.That(ActualException, Is.Not.Null),
                    () => Assert.That(ActualException, Is.InstanceOf(typeof(ArgumentException)))
                );
            }

            [Test]
            public void Should_have_expected_message()
            {
                AssertAll(
                    () => Assert.That(ActualException.Message, Is.Not.Null),
                    () => Assert.That(ActualException.Message, Is.EqualTo("Invalid Country Code Length.")));
            }
        }

        public class When_country_is_null : TestFixtureBase { }
        public class When_country_is_user_assigned_code : TestFixtureBase { }
    }
}