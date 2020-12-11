using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHLibFiles
{
    public abstract class HLibDiscItem
    {
        protected string _name;
        protected string _path;
        protected string _fullName;
        protected bool _inArchive;

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

        public HLibDiscItem(string path, string name, bool inArchive):this(path, name)
        {
            InArchive = inArchive;
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
