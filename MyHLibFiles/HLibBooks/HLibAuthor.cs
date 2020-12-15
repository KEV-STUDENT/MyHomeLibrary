namespace MyHLibBooks
{
    public struct HLibAuthor
    {
        private string _lastName;
        private string _firstName;
        private string _middleName;

        public HLibAuthor(string lastName, string firstName, string middleName)
        {
            _lastName = lastName;
            _firstName = firstName;
            _middleName = middleName;
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string MiddleName
        {
            get { return _middleName; }
        }
    }
}