using System;
using System.Collections.Generic;

namespace MyHomeLibFiles
{
    public class TreeViewItem_Attribute : ITreeViewItem
    {
        private string name;

        public TreeViewItem_Attribute(string str)
        {
            name = str;
            State = ItemState.Initial;
        }

        public ItemType Type => ItemType.Attribute;
        public string Name => name;

        public ItemState State { get; set; }

        public IEnumerable<string> GetChilds()
        {
            throw new System.NotImplementedException();
        }

        public List<ITreeViewItem> GetChilds_Items()
        {
            throw new NotImplementedException();
        }
    }
}