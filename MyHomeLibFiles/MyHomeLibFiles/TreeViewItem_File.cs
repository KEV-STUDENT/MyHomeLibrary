using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace MyHomeLibFiles
{
    public class TreeViewItem_File : ITreeViewItem
    {
        protected string name;
        protected string path;
        protected ItemType type;

        public TreeViewItem_File(string str)
        {
            path = str;
            FileInfo fileInfo = new FileInfo(path);
            name = fileInfo.Name;
            State = ItemState.Initial;
            type = ItemType.File;
        }

        public TreeViewItem_File(string zip, string file)
        {
            this.path = zip;
            this.name = file;
            State = ItemState.Initial;
            type = ItemType.InZip;
        }

        public virtual ItemType Type => type;
        public string Name => name;
        public string Path => path;
        public ItemState State { get; set; }

        public virtual IEnumerable<string> GetChilds()
        {
            if (type == ItemType.InZip)
            {
                yield return string.Format(" {0} => {1}", name, path);
            }
            else
            {
                yield return path;
            }
        }

        public virtual List<ITreeViewItem> GetChilds_Items()
        {
            List<ITreeViewItem> list = new List<ITreeViewItem>();
            foreach (string item in GetChilds())
            {
                list.Add(new TreeViewItem_Attribute(item));
            }
            return list;
        }

        public virtual byte[] GetFile4Book()
        {
            byte[] array;
            if (type == ItemType.InZip)
            {
                using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read))
                {
                    array = GetFile4Book(archive, name);
                }
            }
            else
            {
                using (FileStream fstream = File.OpenRead(path))
                {
                    array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                }
            }
            return array;
        }

        public virtual byte[] GetFile4Book(ZipArchive archive, string fb2Name)
        {
            return GetFile4Book(archive.GetEntry(name));
        }

        public virtual byte[] GetFile4Book(ZipArchiveEntry entry)
        {
            byte[] array;
            using (Stream st = entry.Open())
            {

                using (var ms = new MemoryStream())
                {
                    st.CopyTo(ms);
                    //st.CopyToAsync(ms);
                    array = ms.ToArray();
                }
            }
            return array;
        }

    }
}