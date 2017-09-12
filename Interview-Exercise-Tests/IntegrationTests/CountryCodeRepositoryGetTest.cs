using System;
using System.IO;
using Interview_Exercise;
using NUnit.Framework;

namespace Interview_Exercise_Tests.IntegrationTests
{
    public class CountryCodeRepositoryGetTest
    {
        public class Success_with_valid_code : TestFixtureBase
        {
            ICountryCodeRepository _repository;
            Country _actualResult;

            protected override void Arrange()
            {
                var repository = new CountryCodeFileHandler();
                var validateCountry = new ValidateCountryCommand();
                _repository = new CountryCodeRepository(repository, validateCountry);

                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "InterviewExercise");
                path = "/Users/mmello/InterviewExercise";
                string filename = Path.Combine(path, "U.csv");
                File.WriteAllText(filename, "USA;UNITED STATES");
            }

            protected override void Act()
            {
                _actualResult = _repository.Get("USA");
            }

            [Test]
            public void Should_return_proper_country()
            {
                AssertAll(
                    () => Assert.That(_actualResult, Is.Not.Null),
                    () => Assert.That(_actualResult.Code, Is.EqualTo("USA")),
                    () => Assert.That(_actualResult.Name, Is.EqualTo("UNITED STATES"))
                );
            }
        }

        public class Fails_with_invalid_code : TestFixtureBase
        {
            ICountryCodeRepository _repository;

            protected override void Arrange()
            {
                var repository = new CountryCodeFileHandler();
                var validateCountry = new ValidateCountryCommand();

                _repository = new CountryCodeRepository(repository, validateCountry);

                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "InterviewExercise");
                path = "/Users/mmello/InterviewExercise";
                string filename = Path.Combine(path, "U.csv");
                File.Delete(filename);
            }

            protected override void Act()
            {
                _repository.Get("UNK");
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