using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Threading;

using System.Threading;
using System.Diagnostics;
using MyHomeLibFiles;

namespace MyHomeLibUI
{
    public class TreeView4Lib : TreeViewItem, ITreeView4Lib
    {
        protected ITreeViewItem libItem;
        protected bool isLoaded;

        public TreeView4Lib()
        {
            libItem = new TreeViewItem_Empty();
        }

        public TreeView4Lib(string header)
        {
            libItem = TreeItemsFactory.GetItem(header);
            UpdateItem();
        }

        public TreeView4Lib(ITreeViewItem item)
        {
            libItem = item;
            UpdateItem();
        }

        public void UpdateItem()
        {
            this.Header = libItem.Name;
            if ((libItem.Type == ItemType.Attribute) || (libItem.Type == ItemType.Empty) || (libItem.State == ItemState.Error))
            {
                isLoaded = true;
            }
            else
            {
                Items.Add(new TreeView4Lib());
                Expanded += new RoutedEventHandler(ItemExpanded);
            }
            UpdateViewAfterLoaded();
        }

        public void UpdateViewAfterLoaded()
        {
            if (libItem.Type == ItemType.Directory)
            {
                FontWeight = FontWeights.Bold;
            }
            else if (libItem.Type == ItemType.File)
            {
                FontWeight = FontWeights.Normal;
            }
            else if (libItem.Type == ItemType.Zip)
            {
                FontWeight = FontWeights.Bold;
                Foreground = Brushes.DarkBlue;
            }
            else if (libItem.Type == ItemType.FilesUnion)
            {
                FontWeight = FontWeights.Bold;                
                Foreground = Brushes.BlueViolet;
            }
            else
            {
                FontWeight = FontWeights.Normal;
            }

            if (libItem.State == ItemState.Error)
            {
                Foreground = Brushes.Red;
            }
        }


        public void ItemExpanded(object sender, RoutedEventArgs e)
        {
            if ((libItem.Type == ItemType.Attribute) || (libItem.Type == ItemType.Empty) || (libItem.State == ItemState.Error))
                return;

             if (!isLoaded)
             {
                LoadChildsAsync();
             }
        }

        public async void LoadChildsAsync()
        {
            if (isLoaded)
            {
                return;
            }

            if (libItem.Type == ItemType.Attribute)
            {
                isLoaded = true;
                return;
            }
            ClearItem();

            List<ITreeViewItem> list = await Task<List<ITreeViewItem>>.Factory.StartNew(()=>libItem.GetChilds_Items());
                foreach (ITreeViewItem item in list)
                {
                    await Task.Factory.StartNew(
                        () =>
                        {
                            AddTreeViewItem(item);
                        }
                    );
                }
            isLoaded = true;
        }

        private void ClearItem()
        {
            Action clear = ()=>{ Items.Clear(); };
            this.Dispatcher.BeginInvoke(clear);
        }

        private void AddTreeViewItem(ITreeViewItem item)
        {
            if(item == null)
            {
                return;
            }
            Action<ITreeViewItem> add = (ITreeViewItem i) =>
            {
                Items.Add(new TreeView4Lib(i));
            };
            this.Dispatcher.Invoke(add, item);
        }

        public ITreeViewItem LibItem {
            get => libItem;
            set { libItem = value; }
        }

        public bool IsChildsLoaded
        {
            get => isLoaded;
        }


    }
}