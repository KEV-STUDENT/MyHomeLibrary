using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Ionic.Zip;

namespace MyHLibFiles
{
    public class HLibDirectory : HLibDiscItem
    {
        public HLibDirectory(string path, string name) : base(path, name)
        {
        }

        public HLibDirectory(HLibFileZIP zip, ZipEntry entry) : base(zip, entry)
        {
        }

        public override IEnumerable<HLibDiscItem> GetDiscItemsEnum()
        {
            IEnumerable<string> collectionDirFile;
            string name;
            FileInfo fileInfo;
            DirectoryInfo dirInfo;
            HLibDiscItem discItem;

            try
            {
                collectionDirFile = Directory.EnumerateFileSystemEntries(FullName);
            }
            catch (System.UnauthorizedAccessException)
            {
                yield break;
            }

            foreach(string entry in collectionDirFile)
            {
                if (File.Exists(entry))
                {
                    fileInfo = new FileInfo(entry);
                    name = fileInfo.Name;
                }
                else
                {
                    dirInfo = new DirectoryInfo(entry);
                    name = dirInfo.Name;
                }

                try
                {
                    discItem = HLibFactory.GetDiskItem(FullName, name);
                }
                catch (ExceptionAccess e) { continue; }
                catch (NotImplementedException e) { continue; }
                catch (NotSupportedException e) { continue; }

                yield return discItem;
            }
            yield break;
        }
    }
}