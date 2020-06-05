using System;
using System.Collections.Generic;

namespace MyHomeLibFiles
{
    public interface ITreeViewItem
    {
        ItemType Type { get; }
        string Name { get; }
        ItemState State { get; set; }

        IEnumerable<string> GetChilds();
        List<ITreeViewItem> GetChilds_Items();
    }
}