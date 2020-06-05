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
using MyHomeLibUI;

namespace MyHomeLibraryScaner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetLibPath_Click(object sender, RoutedEventArgs e)
        {
            string selPath;
            using (var folder = new FolderBrowserDialog())
            {
                folder.SelectedPath = libPath.Text;
                DialogResult result = folder.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                selPath = folder.SelectedPath;
            }

            libPath.Text = selPath;
            directoryTree.Items.Clear();
            TreeView4Lib root = new TreeView4Lib(selPath);
            directoryTree.Items.Add(root);
        }
    }
}
