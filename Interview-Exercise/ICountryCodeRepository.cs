namespace Interview_Exercise
{
    public interface ICountryCodeRepository
    {
        void Add(Country country);

        Country Get(string code);

        void Update(Country country);

        void Delete(string code);

        void Clear();
    }
}