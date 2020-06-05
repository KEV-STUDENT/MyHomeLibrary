using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace MyHomeLibUI
{
    /// <summary>
    /// Логика взаимодействия для SelectPath.xaml
    /// </summary>
    public partial class SelectPath : System.Windows.Controls.UserControl
    {
        private SelectPath_Out outData;
        public event Action PathChanged;

        public string SelectedPath
        {
            get { return outData.Result; }
            set { outData.Result = value; }
        }
        public SelectPath()
        {
            InitializeComponent();
            outData = new SelectPath_Out();
            outData.Result = @"c:\";
            this.DataContext = outData;
        }

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        private int selectionType = 1;
        public int SelectionType
        {
            get { return (int)GetValue(SelectionTypeProperty); }
            set { SetValue(SelectionTypeProperty, value); }
        }

        private string filter = "";
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(SelectPath),
                new UIPropertyMetadata("Select", new PropertyChangedCallback(LabelTextChanged)));

        public static readonly DependencyProperty SelectionTypeProperty =
            DependencyProperty.Register("SelectionType", typeof(int), typeof(SelectPath),
                new UIPropertyMetadata(1, new PropertyChangedCallback(SelectionTypeChanged)));

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(SelectPath),
                new UIPropertyMetadata("", new PropertyChangedCallback(FilterChanged)));

        private static void FilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectPath s = (SelectPath)d;
            s.filter = e.NewValue.ToString();
        }

        private static void LabelTextChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            SelectPath s = (SelectPath)depObj;
            s.selText.Text = args.NewValue.ToString();
        }

        private static void SelectionTypeChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            SelectPath s = (SelectPath)depObj;
            s.selectionType = (int)args.NewValue;
        }

        private void SelLibPath_Click(object sender, RoutedEventArgs e)
        {
            if (selectionType == 1)
            {
                SelectFolder();
            }
            else
            {
                SelectFile();
            }
            this.PathChanged?.Invoke();
        }

        private void SelectFile()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter;
                DialogResult result = openFileDialog.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                outData.Result = openFileDialog.FileName;
            }
        }

        private void SelectFolder()
        {
            using (var folder = new FolderBrowserDialog())
            {
                folder.SelectedPath = outData.Result;
                DialogResult result = folder.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                outData.Result = folder.SelectedPath;
            }
        }
    }
}
