using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using MyDBModel;

namespace MyHomeLibFiles
{
    public class TreeViewItem_FileZIP : TreeViewItem_File
    {
        private const int _AvgFilesInUnions = 100;
        private const int _MaxFilesInUnions = 200;
        private bool useUnionsCollection = false;
        private Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        public TreeViewItem_FileZIP(string str) : base(str)
        {
        }

        public override ItemType Type => ItemType.Zip;

        public override IEnumerable<string> GetChilds()
        {
            if (dict.Count > 0)
            {
                foreach (string item in dict.Keys)
                {
                    yield return item;
                }
            }
            else
            {
                using (Ionic.Zip.ZipFile zipArchive = Ionic.Zip.ZipFile.Read(path))
                {
                    string name;
                    List<string> list;
                    if (zipArchive.Entries.Count > _MaxFilesInUnions)
                    {
                        int cnt = 0, start = 0, fin = 0;
                        int nUnions = zipArchive.Entries.Count / _AvgFilesInUnions;
                        int nUnionsAdd = zipArchive.Entries.Count % _AvgFilesInUnions;

                        useUnionsCollection = true;
                        var listZip = zipArchive.Entries.ToList<Ionic.Zip.ZipEntry>();

                        for (int i = 0; i < nUnions; i++)
                        {
                            list = new List<string>();
                            start = cnt;
                            fin = cnt + _AvgFilesInUnions - 1;
                            if (nUnionsAdd > 0)
                            {
                                nUnionsAdd--;
                                fin++;
                            }

                            for (int j = cnt; j <= fin; j++)
                            {
                                list.Add(listZip[j].FileName);
                            }
                            cnt = fin + 1;
                            name = string.Format("{0} - {1}", listZip[start].FileName, listZip[fin].FileName);
                            dict.Add(name, list);
                            yield return name;
                        }
                    }
                    else
                    {
                        foreach (var item in zipArchive.Entries)
                        {
                            yield return item.FileName;
                        }
                    }
                }
            }      
        }

        public override List<ITreeViewItem> GetChilds_Items()
        {
            List<ITreeViewItem> list = new List<ITreeViewItem>();
            foreach (string item in GetChilds())
            {
                if (useUnionsCollection)
                {
                    list.Add(new TreeViewItem_FilesUnion(path, item, dict[item]));
                }
                else
                {
                    list.Add(TreeItemsFactory.GetItem(path, item));
                }
            }
            return list;
        }

        public override List<Book> GetChilds_Books()
        {
            ITreeViewItem treeViewItem;
            List<Book> books = new List<Book>();
            using (Ionic.Zip.ZipFile zipArchive = Ionic.Zip.ZipFile.Read(path))
            {
                foreach(var entry in zipArchive.Entries)
                {
                    treeViewItem = TreeItemsFactory.GetItem(path, entry.FileName, zipArchive);
                    if (treeViewItem is TreeViewItem_FileFB2 fb2)
                    {
                        books.Add(fb2.GetBook());
                    }
                }
            }

            return books;
        }
        /*class UnionFiles
        {
            private string name;

            public string Name
            {
                set { name = value; }
                get { return name; }
            }
        }*/
    }
}