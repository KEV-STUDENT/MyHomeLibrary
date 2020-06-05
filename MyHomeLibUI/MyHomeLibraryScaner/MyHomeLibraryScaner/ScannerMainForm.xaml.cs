using System.Windows;
using MyHomeLibUI;

namespace MyHomeLibraryScaner
{
    /// <summary>
    /// Логика взаимодействия для ScannerMainForm.xaml
    /// </summary>
    public partial class ScannerMainForm : Window
    {
        protected const int DBPage = 1, FB2Page = 2, ProcessResultPage = 3;
        protected ScannerMainFormPrams formParams = new ScannerMainFormPrams();
        /*public ScannerMainFormPrams FormPrams
        {
            get { return formParams; }
        }*/

        private int activePage = 1;
        public int ActivePage
        {
            set {SetValue(ActivePageProperty,value);}
            get { return (int) GetValue(ActivePageProperty); }
        }
        public static readonly DependencyProperty ActivePageProperty =
           DependencyProperty.Register("ActivePage", typeof(int), typeof(ScannerMainForm), 
               new UIPropertyMetadata(1, new PropertyChangedCallback(ActivePageChanged)));

        private static void ActivePageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScannerMainForm form = (ScannerMainForm)d;
            form.activePage = ((int)e.NewValue);
            form.ActivatePage();
        }

        private void ActivatePage()
        {
            if(activePage == DBPage)
            {
                Ok.Visibility = Visibility.Hidden;
                Previous.Visibility = Visibility.Hidden;
                Next.Visibility = Visibility.Visible;
            } else if(activePage == FB2Page)
            {
                Ok.Visibility = Visibility.Visible;
                Previous.Visibility = Visibility.Visible;
                Next.Visibility = Visibility.Hidden;
            }
            else if (activePage == ProcessResultPage)
            {
                Previous.IsEnabled = false;
                Ok.IsEnabled = false;
            }
            else
            {
                Ok.Visibility = Visibility.Hidden;
                Previous.Visibility = Visibility.Visible;
                Next.Visibility = Visibility.Visible;
            }

            switch(activePage)
            {
                case 1:
                    Page1.Visibility = Visibility.Visible;
                    Page2.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Page1.Visibility = Visibility.Collapsed;
                    Page2.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        public ScannerMainForm()
        {
            InitializeComponent();
            ((IOptionPageParams)formParams).FullPath = @"c:\";
            Page1.PageParams = (IOptionPageParams)formParams;
            formParams.SourceType = ItemSourceType.Item;
            ((IFB2PageParams)formParams).ItemPath = @"c:\1\";
            Page2.PageParams = (IFB2PageParams)formParams;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = ActivePage - 1;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = ProcessResultPage;
            //IOptionPageParams p1 = Page1.PageParams;
            //IFB2PageParams p2 = Page2.PageParams;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = ActivePage + 1;
        }
    }
}
