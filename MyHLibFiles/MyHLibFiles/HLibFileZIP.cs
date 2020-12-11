using System.IO;
using System.Collections.Generic;
using Ionic.Zip;

namespace MyHLibFiles
{
    public class HLibFileZIP : HLibFile
    {
        private ZipFile zipArchive;

        public HLibFileZIP(string path, string name) : base(path, name)
        {
            OpenFile();
        }

        public override void OpenFile()
        {
            zipArchive = ZipFile.Read(FullName);
        }

        public override void CloseFile()
        {
            zipArchive.Dispose();
        }

        public override IEnumerable<HLibDiscItem> GetDiscItemsEnum()
        {
            foreach (var item in zipArchive.Entries)
            {
                yield return new HLibFile(zipArchive.Name, item.FileName, true);
            }
            yield break;
        }
    }
}