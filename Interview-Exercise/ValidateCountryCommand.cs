namespace Interview_Exercise
{
    using System;
    using System.Text.RegularExpressions;

    public class ValidateCountryCommand : IValidateCountry
    {
        const string REGEX = "^(?!.*(AA[A-Z])|(aa[a-z])|(Q[M-Z][A-Z])|(q[m-z][a-z])|(X[A-Z][A-Z])|(x[a-z][a-z])|(ZZ[A-Z])|(zz[a-z])).*$";

        public void Execute(Country country)
        {
            if (country == null)
                throw new ArgumentException("Country cannot be null.");

            if (country.Code.Length != 3)
                throw new ArgumentException("Invalid Country Code Length.");

            if (!Regex.IsMatch(country.Code, REGEX))
                throw new ArgumentException("Invalid Country Code.");
        }
    }
}