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

        public HLibFile(HLibFileZIP zip, ZipEntry entry, bool exclusive) : base(zip, entry, exclusive){}

        public void Dispose()
        {
            CloseFile();
        }

        public abstract void CloseFile();

        public abstract void OpenFile();
    }
}