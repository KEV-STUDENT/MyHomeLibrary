using System;
using System.IO;
using System.Collections.Generic;
using Ionic.Zip;
using MyHLibBooks;

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
            HLibDiscItem discItem;
            foreach (var item in zipArchive.Entries)
            {
                try
                {
                    discItem = HLibFactory.GetDiskItem(this, item);
                }
                catch (ExceptionAccess) { continue; }
                catch (NotImplementedException) { continue; }
                catch (NotSupportedException) { continue; }

                yield return discItem;                
            }
            yield break;
        }

        public override IData GetDataFromFile()
        {
            throw new NotSupportedException();
        }
    }
}