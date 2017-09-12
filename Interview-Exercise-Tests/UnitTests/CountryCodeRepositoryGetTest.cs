using System;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class CountryCodeRepositoryGetTest
    {
        public class Success_with_valid_code : TestFixtureBase
        {
            ICountryCodeRepository _repository;
            Country _actualResult;

            protected override void Arrange()
            {
                var repository = Mock<ICountryCodeRepository>();
                repository.Expect(x => x.Get("USA")).Return(new Country { Code = "USA", Name = "United States" });

                var validateCountry = Mock<IValidateCountry>();

                _repository = new CountryCodeRepository(repository, validateCountry);
            }

            protected override void Act()
            {
                _actualResult = _repository.Get("USA");
            }

            [Test]
            public void Should_have_used_the_dependency()
            {
                AssertAll(
                    () => Assert.That(_actualResult, Is.Not.Null),
                    () => Assert.That(_actualResult.Code, Is.EqualTo("USA"))
                );
            }
        }

        public class Fails_with_invalid_code : TestFixtureBase
        {
            ICountryCodeRepository _repository;
            IValidateCountry _validateCountry;

            protected override void Arrange()
            {
                _repository = Mock<ICountryCodeRepository>();
                _validateCountry = Mock<IValidateCountry>();
            }

            protected override void Act()
            {
                var testSubject = new CountryCodeRepository(_repository, _validateCountry);
                testSubject.Get("UNK");
            }

            [Test]
            public void Should_set_actual_exception()
            {
                Assert.That(ActualException, Is.Not.Null);
            }

            [Test]
            public void Should_have_expected_message()
            {
                AssertAll(
                    () => Assert.That(ActualException.Message, Is.Not.Null),
                    () => Assert.That(ActualException.Message, Is.EqualTo($"Code 'UNK' does not exist.")));
            }
        }

        public class Success_with_lower_case_code : TestFixtureBase { }
    }

    public class CountryCodeRepositoryAddTest
    {
        public class Success_with_lower_case_code : TestFixtureBase { }
        public class Success_with_valid_code : TestFixtureBase { }
        public class Fails_with_invalid_code : TestFixtureBase { }
    }

    public class CountryCodeRepositoryDeleteTest
    {
        public class Success_with_lower_case_code : TestFixtureBase { }
        public class Success_with_valid_code : TestFixtureBase { }
        public class Fails_with_invalid_code : TestFixtureBase { }
    }

    public class CountryCodeRepositoryUpdateTest
    {
        public class Success_with_lower_case_code : TestFixtureBase { }
        public class Success_with_valid_code : TestFixtureBase { }
        public class Fails_with_invalid_code : TestFixtureBase { }
    }

    public class CountryCodeRepositoryClearTest
    {
        public class Success : TestFixtureBase { }
    }
}