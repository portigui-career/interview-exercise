namespace Interview_Exercise
{
    using System;

    public class CountryCodeRepository : ICountryCodeRepository
    {
        readonly ICountryCodeRepository _repository;
        readonly IValidateCountry _validateCountry;

        public CountryCodeRepository(ICountryCodeRepository repository, IValidateCountry validateCountry)
        {
            _repository = repository;
            _validateCountry = validateCountry;
        }

        public void Add(Country country)
        {
            _validateCountry.Execute(country);

            var existing = _repository.Get(country.Code);
            if (null != existing)
                throw new Exception($"Country Code '{country.Code}' already exists.");
            _repository.Add(country);
        }

        public Country Get(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("Invalid country code.");

            var country = _repository.Get(code);
            if (null == country)
                throw new ArgumentException($"Code '{code}' does not exist.");

            return country;
        }


        public void Update(Country country)
        {
            _validateCountry.Execute(country);

            var existing = _repository.Get(country.Code);
            if (null == existing)
                throw new ArgumentException($"Code '{country.Code}' does not exist.");

            _repository.Update(country);
        }

        public void Delete(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("Invalid country code.");

            var existing = _repository.Get(code);
            if (null == existing)
                throw new ArgumentException($"Code '{code}' does not exist.");
            _repository.Delete(code);
        }

        public void Clear()
        {
            _repository.Clear();
        }
    }
}
