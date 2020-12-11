using System;
using System.Collections.Generic;

namespace MyHLibFiles
{
    public class HLibFile : HLibDiscItem, IDisposable
    {
        public HLibFile(string path, string name) : base(path, name)
        {
        }

        public HLibFile(string path, string name, bool inArchive) : base(path, name, inArchive)
        {
        }

        public void Dispose()
        {
            CloseFile();
        }

        public virtual void CloseFile()
        { }

        public virtual void OpenFile()
        { }

        public override IEnumerable<HLibDiscItem> GetDiscItemsEnum()
        {
            yield break;
        }
    }
}