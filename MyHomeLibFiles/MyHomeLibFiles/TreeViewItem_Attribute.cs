using System;
using System.Collections.Generic;

namespace MyHomeLibFiles
{
    public class TreeViewItem_Attribute : ITreeViewItem
    {
        private string name;
        private string attributeName;
        private string attributeValue;

        public TreeViewItem_Attribute(string str)
        {
            name = str;
            string[] words = str.Split(new char[] { ':' });
            if (words.Length == 2)
            {
                attributeName = words[0];
                attributeValue = words[1];
            }
            else {
                attributeName = "Unknown";
                attributeValue = str;
            }

            State = ItemState.Initial;
        }

        public ItemType Type => ItemType.Attribute;
        public string Name => name;

        public ItemState State { get; set; }
        public string AttributeName => attributeName;
        public string AttributeValue => attributeValue;

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