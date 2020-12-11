using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using MyDBModel;

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

        public virtual List<Book> GetChilds_Books()
        {
            throw new NotImplementedException();
        }

        public List<MyDBModel.DBFile> GetChilds_Files()
        {
            List<MyDBModel.DBFile> list = new List<MyDBModel.DBFile>();
            MyDBModel.DBFile file;
            List<Book> books;
            if (type != ItemType.InZip)
            {
                try
                {
                    books = GetChilds_Books();
                    file = new MyDBModel.DBFile() { Path = path, Books = books};

                    //file = new MyDBModel.DBFile() { Path = path};
                    Debug.WriteLine(file.Path);
                    list.Add(file);
                }
                catch ( NotImplementedException e)
                { }
            }
            return list;
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
        //public virtual byte[] GetFile4Book()
        //{
        //    byte[] array;
        //    if (type == ItemType.InZip)
        //    {
        //        using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read))
        //        {
        //            array = GetFile4Book(archive, name);
        //        }
        //    }
        //    else
        //    {
        //        using (FileStream fstream = File.OpenRead(path))
        //        {
        //            array = new byte[fstream.Length];
        //            // считываем данные
        //            fstream.Read(array, 0, array.Length);
        //        }
        //    }
        //    return array;
        //}

        //public virtual byte[] GetFile4Book(ZipArchive archive, string fb2Name)
        //{
        //    return GetFile4Book(archive.GetEntry(name));
        //}

        //public virtual byte[] GetFile4Book(Ionic.Zip.ZipEntry entry)
        //{
        //    byte[] array;
        //    using (Stream st = entry.OpenReader())
        //    {

        //        using (var ms = new MemoryStream())
        //        {
        //            st.CopyTo(ms);
        //            //st.CopyToAsync(ms);
        //            array = ms.ToArray();
        //        }
        //    }
        //    return array;
        //}

        //public virtual byte[] GetFile4Book(ZipArchiveEntry entry)
        //{
        //    byte[] array;
        //    using (Stream st = entry.Open())
        //    {

        //        using (var ms = new MemoryStream())
        //        {
        //            st.CopyTo(ms);
        //            //st.CopyToAsync(ms);
        //            array = ms.ToArray();
        //        }
        //    }
        //    return array;
        //}

    }
}