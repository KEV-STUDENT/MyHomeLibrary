using System;
using System.IO;
using System.Collections.Generic;
using Ionic.Zip;
using MyHLibBooks;

namespace MyHLibFiles
{
    public class HLibFileZIP : HLibFile, IHLibDirectoryArchive
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

        public IEnumerable<HLibDiscItem> GetDiscItemsEnum()
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

        public List<HLibDiscItem> GetDiscItemsList()
        {
            List<HLibDiscItem> list = new List<HLibDiscItem>();
            foreach (var item in GetDiscItemsEnum())
            {
                list.Add(item);
            }
            return list;
        }

        public ZipEntry GetEntryByName(string entryName)
        {
            foreach (var item in zipArchive.Entries)
            {
                if(entryName.CompareTo(item.FileName) == 0)
                {
                    return item;
                }
            }
            throw new ExceptionPath(FullName, entryName);
        }
    }
}