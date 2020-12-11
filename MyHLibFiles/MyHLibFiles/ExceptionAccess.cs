using System;

namespace MyHLibFiles
{
    public class ExceptionAccess:Exception
    {
        protected string _path;
        protected string _name;

        public string Path
        {
            get { return _path; }
        }

        public string Name
        {
            get { return _name; }
        }

        public ExceptionAccess(string path, string name) : base("Access denied")
        {
            _path = path;
            _name = name;
        }
    }
}