using System;
using System.Collections.Generic;
using Ionic.Zip;

namespace MyHLibFiles
{
    public abstract class HLibFile : HLibDiscItem, IDisposable
    {
        public HLibFile(string path, string name) : base(path, name)
        {
        }

        public HLibFile(HLibFileZIP zip, ZipEntry entry) : base(zip, entry)
        {
        }

        public void Dispose()
        {
            CloseFile();
        }

        public virtual void CloseFile()
        { }

        public abstract IData GetDataFromFile();

        public virtual void OpenFile()
        { }

        public override IEnumerable<HLibDiscItem> GetDiscItemsEnum()
        {
            yield break;
        }
    }
}