using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Diagnostics;

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
                using (ZipArchive zipArchive = ZipFile.OpenRead(path))
                {
                    string name;
                    List<string> list;
                    if (zipArchive.Entries.Count > _MaxFilesInUnions)
                    {
                        int cnt = 0, start = 0, fin = 0;
                        int nUnions = zipArchive.Entries.Count / _AvgFilesInUnions;
                        int nUnionsAdd = zipArchive.Entries.Count % _AvgFilesInUnions;

                        useUnionsCollection = true;

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
                                list.Add(zipArchive.Entries[j].Name);
                            }
                            cnt = fin + 1;
                            name = string.Format("{0} - {1}", zipArchive.Entries[start].Name, zipArchive.Entries[fin].Name);
                            dict.Add(name, list);
                            yield return name;
                        }
                    }
                    else
                    {
                        foreach (var item in zipArchive.Entries)
                        {
                            yield return item.FullName;
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