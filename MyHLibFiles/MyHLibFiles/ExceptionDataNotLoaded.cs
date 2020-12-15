using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHLibFiles
{
    public class ExceptionDataNotLoaded : Exception
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

        public ExceptionDataNotLoaded(string path, string name) : base("Data not loaded.")
        {
            _path = path;
            _name = name;
        }
    }
}
