using System;

namespace MyHLibFiles
{
    public class ExceptionPath: Exception
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

        public ExceptionPath(string path, string name) : base("Wrong Path")
        {
            _path = path;
            _name = name;
        }
    }
}
