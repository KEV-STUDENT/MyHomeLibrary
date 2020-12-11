using System.Collections.Generic;
using MyDBModel;
using System;

namespace MyHomeLibFiles
{
    public class TreeViewItem_FilesUnion : ITreeViewItem
    {
        private string path;
        private string name;
        private List<string> list;

        public TreeViewItem_FilesUnion(string path, string name, List<string> list)
        {
            this.name = name;
            this.path = path;
            this.list = list;
            State = ItemState.Initial;
        }

        public ItemType Type => ItemType.FilesUnion;

        public string Name => name;

        public ItemState State { get; set; }

        public IEnumerable<string> GetChilds()
        {
            foreach(var child in list)
            {
                yield return child;
            }
        }

        public List<ITreeViewItem> GetChilds_Items()
        {
            List<ITreeViewItem> list = new List<ITreeViewItem>();
            foreach (string item in this.list)
            {
                list.Add(TreeItemsFactory.GetItem(path, item));
            }
            return list;
        }

        public List<Book> GetChilds_Books()
        {
            List<Book> books = new List<Book>();
            List<Book> childsBbooks;

            foreach (var item in GetChilds_Items())
            {
                try
                {
                    childsBbooks = item.GetChilds_Books();
                    books.AddRange(childsBbooks);
                }
                catch (NotImplementedException e)
                {
                    //
                }
            }

            return books;
        }

        public List<DBFile> GetChilds_Files()
        {
            throw new NotImplementedException();
        }
    }
}