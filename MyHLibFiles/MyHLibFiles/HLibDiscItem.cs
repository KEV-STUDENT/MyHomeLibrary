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
        protected ZipEntry _zipEntry;

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
            set { _inArchive = value; }
        }

        public HLibDiscItem(string path, string name)
        {
            _name = name;
            _path = path;
            _fullName = System.IO.Path.Combine(Path, Name);
        }

        public HLibDiscItem(HLibFileZIP zip, ZipEntry entry) : this(zip.FullName, entry.FileName)
        {
            InArchive = true;
            _zip = zip;
            _zipEntry = entry;
        }

        public abstract IEnumerable<HLibDiscItem> GetDiscItemsEnum();
        
        public virtual List<HLibDiscItem> GetDiscItemsList()
        {
            List<HLibDiscItem> list = new List<HLibDiscItem>();
            foreach(var item in GetDiscItemsEnum())
            {
                list.Add(item);
            }
            return list;
        }
    }
}
