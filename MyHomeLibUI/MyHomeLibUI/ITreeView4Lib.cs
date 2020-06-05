using MyHomeLibFiles;
using System.Windows;

namespace MyHomeLibUI
{
    public interface ITreeView4Lib
    {
        ITreeViewItem LibItem { get; set; }
        bool IsChildsLoaded { get; }

        void ItemExpanded(object sender, RoutedEventArgs e);
        void UpdateViewAfterLoaded();
        void LoadChildsAsync();
    }
}