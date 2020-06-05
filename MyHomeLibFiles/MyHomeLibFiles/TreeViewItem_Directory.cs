using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace MyHomeLibFiles
{
    public class TreeViewItem_Directory : ITreeViewItem
    {
        private ItemState state = ItemState.Initial;
        private string path;

        public TreeViewItem_Directory(string str)
        {            
            this.path = str;
        }

        public ItemType Type => ItemType.Directory;

        public string Name {
            get
            {
                DirectoryInfo info = new DirectoryInfo(path);
                return info.Name;
            }
        }

        public ItemState State {
            get { return state; }
            set { state = value; }
        }

        public IEnumerable<string> GetChilds()
        {
            IEnumerable<string> collectionDir, collectionFile;
            try
            {
                collectionDir = Directory.EnumerateDirectories(path);
                collectionFile = Directory.EnumerateFiles(path);
            }
            catch (System.UnauthorizedAccessException)
            {
                state = ItemState.Error;
                yield break;
            }

            foreach (string dir in collectionDir)
            {
                yield return dir;
            }

            foreach (string file in collectionFile)
            {
                yield return file;
            }
        }

        public List<ITreeViewItem> GetChilds_Items()
        {
            List<ITreeViewItem> list = new List<ITreeViewItem>();
            foreach (string item in GetChilds())
            {
                list.Add(TreeItemsFactory.GetItem(item));
            }
            return list;
        }
    }
}