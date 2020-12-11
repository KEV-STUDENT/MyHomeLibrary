using MyDBModel;
using System;
using System.Collections.Generic;
namespace MyHomeLibFiles
{
    public class TreeViewItem_Empty : ITreeViewItem
    {
        public ItemType Type => ItemType.Empty;

        public string Name => string.Empty;

        public ItemState State { get; set; }

        public IEnumerable<string> GetChilds()
        {
            yield break;
        }

        public List<Book> GetChilds_Books()
        {
            throw new NotImplementedException();
        }

        public List<DBFile> GetChilds_Files()
        {
            throw new NotImplementedException();
        }

        public List<ITreeViewItem> GetChilds_Items()
        {
            throw new NotImplementedException();
        }
    }
}