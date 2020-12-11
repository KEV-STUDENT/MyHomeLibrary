using System.Collections.Generic;
using MyDBModel;

namespace MyHomeLibFiles
{
    public interface ITreeViewItem
    {
        ItemType Type { get; }
        string Name { get; }
        ItemState State { get; set; }

        IEnumerable<string> GetChilds();
        List<ITreeViewItem> GetChilds_Items();

        List<Book> GetChilds_Books();
        List<DBFile> GetChilds_Files();
    }
}