namespace Interview_Exercise
{
    public class Country
    {
        string _name;
        string _code;

        public string Code 
        {
            get { return _code; }
            set { _code = value.ToUpper(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value.ToUpper(); }
        }
    }
}