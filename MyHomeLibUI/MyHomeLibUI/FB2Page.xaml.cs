using System.Windows;
using System.Windows.Controls;

namespace MyHomeLibUI
{
    /// <summary>
    /// Логика взаимодействия для FB2Page.xaml
    /// </summary>
    public partial class FB2Page : UserControl
    {
        private ItemSourceType sourceType = ItemSourceType.Directory;
        public ItemSourceType SourceType
        {
            get {return (ItemSourceType)GetValue(SourceTypeProperty); }
            set { SetValue(SourceTypeProperty, value); }
        }

        public static readonly DependencyProperty SourceTypeProperty =
            DependencyProperty.Register("SourceType", typeof(ItemSourceType), typeof(FB2Page),
                new UIPropertyMetadata(ItemSourceType.Directory, new PropertyChangedCallback(SourceTypeChanged)));

        private IFB2PageParams pageParams;
        public IFB2PageParams PageParams
        {
            get
            {
                return pageParams;
            }
            set
            {
                pageParams = value;
                UpdateControls();
            }
        }

        private static void SourceTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FB2Page page = (FB2Page)d;
            ItemSourceType newValue = (ItemSourceType)e.NewValue;
            string path;

            page.sourceType = newValue;
            page.ActivateSource();

            if (page.pageParams != null)
            {
                page.PageParams.SourceType = newValue;
                switch(newValue)
                {
                    case ItemSourceType.File:
                        path = page.pageParams.FilePath;
                        break;
                    case ItemSourceType.Item:
                        path = page.pageParams.ItemPath;
                        break;
                    default:
                        path = page.pageParams.DirPath;
                        break;
                }
                page.SetTreeViewPath(path);
            }
        }

        private void ActivateSource()
        {
            switch(sourceType)
            {
                case ItemSourceType.File:
                    SelectFile.IsEnabled = true;
                    ItemsTree.IsEnabled = true;

                    SelectLibDir.IsEnabled = false;
                    SelectItem.IsEnabled = false;
                   
                    break;
                case ItemSourceType.Item:
                    SelectItem.IsEnabled = true;
                    ItemsTree.IsEnabled = true;

                    SelectFile.IsEnabled = false;
                    SelectLibDir.IsEnabled = false;
                    break;
                default:
                    SelectLibDir.IsEnabled = true;

                    SelectFile.IsEnabled = false;
                    SelectItem.IsEnabled = false;
                    ItemsTree.IsEnabled = false;
                    break;
            }
        }

        private void UpdateControls()
        {
            SelectFile.SelectedPath = PageParams.FilePath;
            SelectItem.SelectedPath = PageParams.ItemPath;
            SelectLibDir.SelectedPath = PageParams.DirPath;

            switch (pageParams.SourceType)
            {
                case ItemSourceType.File:
                    LibFile.IsChecked = true;
                    break;
                case ItemSourceType.Item:
                    LibBook.IsChecked = true;
                    break;
                default:
                    LibDirectory.IsChecked = true;
                    break;
            }

            SourceType = pageParams.SourceType;
        }

        public FB2Page()
        {
            InitializeComponent();
        }

        private void LibFile_Checked(object sender, RoutedEventArgs e)
        {
            SourceType = ItemSourceType.File;
        }

        private void LibDirectory_Checked(object sender, RoutedEventArgs e)
        {
            SourceType = ItemSourceType.Directory;
        }

        private void LibBook_Checked(object sender, RoutedEventArgs e)
        {
            SourceType = ItemSourceType.Item;
        }

        private void DirPath_PathChanged()                
        {
            PageParams.DirPath = SelectLibDir.SelectedPath;
            SetTreeViewPath(PageParams.DirPath);
        }

        private void FilePath_PathChanged()
        {
            PageParams.FilePath = SelectFile.SelectedPath;
            SetTreeViewPath(PageParams.FilePath);
        }

        private void ItemPath_PathChanged()
        {
            PageParams.ItemPath = SelectItem.SelectedPath;
            SetTreeViewPath(PageParams.ItemPath);
        }

        private void SetTreeViewPath(string path)
        {
            ItemsTree.Items.Clear();
            TreeView4Lib root = new TreeView4Lib(path);
            ItemsTree.Items.Add(root);
        }
    }
}
