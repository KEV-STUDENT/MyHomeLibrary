using System;
using System.Collections.Generic;

namespace MyHomeLibFiles
{
    public class TreeViewItem_Attribute : ITreeViewItem
    {
        private string name;
        private string attributeValue;

        private List<ITreeViewItem> treeViews;
        public readonly AttributeType AttributeType = AttributeType.Empty;

        public TreeViewItem_Attribute(string str, AttributeType attributeType)
        {
            name = str;
            attributeValue = str;
            AttributeType = attributeType;
            treeViews = new List<ITreeViewItem>();
            State = ItemState.Initial;
        }

        public TreeViewItem_Attribute(string str)
        {
            name = str;
            string[] words = str.Split(new char[] { ':' });
            if (words.Length == 2)
            {
                attributeValue = words[1];

                AttributeType attribute;
                if (Enum.TryParse<AttributeType>(words[0], true, out attribute))
                {
                    AttributeType = attribute;
                }
                else if(string.Compare(words[0], "Genre[FB2]", true) == 0)
                {
                    AttributeType = AttributeType.GenreFB2;
                }
            }
            else 
            {
                attributeValue = str;
            }

            treeViews = new List<ITreeViewItem>();
            State = ItemState.Initial;
        }

        public ItemType Type => ItemType.Attribute;
        public string Name => name;

        public ItemState State { get; set; }
        public string AttributeName => AttributeType.ToString();
        public string AttributeValue => attributeValue;

        public IEnumerable<string> GetChilds()
        {
            foreach (ITreeViewItem item in treeViews)
            {
                yield return item.Name;
            }
        }

        public void AddChild(ITreeViewItem item)
        {
            treeViews.Add(item);
        }
        public List<ITreeViewItem> GetChilds_Items()
        {
            return treeViews;
        }
    }
}