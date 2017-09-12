namespace Interview_Exercise
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class CountryCodeFileHandler : ICountryCodeRepository
    {
        string _path { get; set; }

        public CountryCodeFileHandler()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "InterviewExercise");
            _path = "/Users/mmello/InterviewExercise";
            Directory.CreateDirectory(_path);
        }

        public void Add(Country country)
        {
            string filename = Path.Combine(_path, country.Code.Substring(0, 1) + ".csv");
            var data = File.ReadAllLines(filename);
            if (data.Any(d => Regex.IsMatch(d, "^(" + country.Code + ");")))
                throw new Exception($"Country Code '{country.Code}' already exists.");
            File.AppendText($"{country.Code};{country.Name}");
        }

        public Country Get(string code)
        {
            var newCode = code.ToUpper();
            string filename = Path.Combine(_path, newCode.Substring(0, 1) + ".csv");
            if (!File.Exists(filename))
                throw new ArgumentException($"Code '{code}' does not exist.");

            var data = File.ReadAllLines(filename).ToList();
            var country = data.FirstOrDefault(d => Regex.IsMatch(d, "^(" + newCode + ");"));
            if (country == null)
                throw new ArgumentException($"Code '{code}' does not exist.");

            return new Country { Code = newCode, Name = country.Substring(country.IndexOf(';') + 1) };
        }


        public void Update(Country country)
        {
            string filename = Path.Combine(_path, country.Code.Substring(0, 1) + ".csv");
            var data = File.ReadAllLines(filename).ToList();
            var d1 = data.FirstOrDefault(d => Regex.IsMatch(d, "^(" + country.Code + ");"));
            if (d1 == null)
                throw new ArgumentException($"Code '{country.Code}' does not exist.");
            data.Remove(d1);
            data.Add($"{country.Code};{country.Name}");
            File.WriteAllLines(filename, data);
        }

        public void Delete(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("Invalid country code.");

            var newCode = code.ToUpper();
            string filename = Path.Combine(_path, newCode.Substring(0, 1) + ".csv");
            var data = File.ReadAllLines(filename).ToList();
            var d1 = data.FirstOrDefault(d => Regex.IsMatch(d, "^(" + newCode + ");"));
            if (d1 == null)
                throw new ArgumentException($"Code '{code}' does not exist.");
            data.Remove(d1);
            File.WriteAllLines(filename, data);
        }

        public void Clear()
        {
            var directoryInfo = new DirectoryInfo(_path);
            foreach (var file in directoryInfo.GetFiles("*.csv", SearchOption.TopDirectoryOnly))
            {
                file.Delete();
            }
        }
    }
}
