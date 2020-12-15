using System;
using System.Collections.Generic;
using Ionic.Zip;
using MyHLibBooks;

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

        public abstract void CloseFile();

        public abstract IData GetDataFromFile();

        public abstract void OpenFile();

        public override IEnumerable<HLibDiscItem> GetDiscItemsEnum()
        {
            yield break;
        }
    }
}