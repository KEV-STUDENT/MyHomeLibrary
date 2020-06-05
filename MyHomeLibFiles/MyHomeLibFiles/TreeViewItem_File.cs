using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

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
    }
}