using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace MyHLibFiles
{
    public abstract class HLibDiscItem
    {
        protected string _name;
        protected string _path;
        protected string _fullName;
        protected bool _inArchive;
        protected HLibFileZIP _zip;
        protected ZipEntry _zip_Entry;
        protected bool _exclusive;

        public virtual string Name
        {
            get => _name;
        }

        public virtual string Path
        {
            get => _path;
        }

        public virtual string FullName 
        { 
            get=>_fullName; 
        }

        public virtual bool InArchive
        {
            get => _inArchive;
        }

        public virtual ZipEntry Zip_Entry
        {
            get => _zip_Entry;
        }

        public HLibDiscItem(string path, string name)
        {
            _name = name;
            _path = path;
            _fullName = System.IO.Path.Combine(Path, Name);
        }

        public HLibDiscItem(HLibFileZIP zip, ZipEntry entry) : this(zip, entry, false) { }

        public HLibDiscItem(HLibFileZIP zip, ZipEntry entry, bool exclusive) : this(zip.FullName, entry.FileName)
        {
            _inArchive = true;
            _zip = zip;
            _zip_Entry = entry;
            _exclusive = exclusive;
        }
    }
}
