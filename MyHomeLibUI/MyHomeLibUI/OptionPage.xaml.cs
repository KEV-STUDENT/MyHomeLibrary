using System.Windows;
using System.Windows.Controls;

namespace MyHomeLibUI
{
    /// <summary>
    /// Логика взаимодействия для OptionPage.xaml
    /// </summary>
    public partial class OptionPage : UserControl
    {
        public OptionPage()
        {
            InitializeComponent();
        }

        private IOptionPageParams pageParams;

        public IOptionPageParams PageParams
        {
            get
            {
                return pageParams;
            }

            set
            {
                pageParams = value;
                UpdateControls();
                ActionTypeChanged();
            }
        }

        private void UpdateControls()
        {
            switch(pageParams.ActionType)
            {
                case ItemUpdateType.Create:
                    Create.IsChecked = true;
                    break;
                default:
                    Update.IsChecked = true;
                    break;
            }

            NewDBPath.SelectedPath = pageParams.Path;
            NewDBName.Text = pageParams.FileName;
            SelectedDBPath.SelectedPath = pageParams.FullPath;
        }

        private void SelPath_Checked(object sender, RoutedEventArgs e)
        {
            if (pageParams == null)
                return;

            pageParams.ActionType = ItemUpdateType.Create;
            ActionTypeChanged();
        }

        private void SelDB_Checked(object sender, RoutedEventArgs e)
        {
            if (pageParams == null)
                return;

            pageParams.ActionType = ItemUpdateType.Update;
            ActionTypeChanged();
        }

        private void ActionTypeChanged()
        {
            bool? isChecked;

            switch (pageParams.ActionType)
            {
                case ItemUpdateType.Create:
                    isChecked = Create.IsChecked;

                    NewDBPath.IsEnabled = isChecked.HasValue ? isChecked.Value : false;
                    NewDBName.IsEnabled = NewDBPath.IsEnabled;
                    SelectedDBPath.IsEnabled = !NewDBPath.IsEnabled;

                    break;
                default:
                    isChecked = Update.IsChecked;
                    SelectedDBPath.IsEnabled = isChecked.HasValue ? isChecked.Value : false;
                    NewDBPath.IsEnabled = !SelectedDBPath.IsEnabled;
                    NewDBName.IsEnabled = !SelectedDBPath.IsEnabled;

                    break;
            }
        }

        private void NewDBPath_PathChanged()
        {
            pageParams.Path = NewDBPath.SelectedPath;
        }

        private void SelectedDBPath_PathChanged()
        {
            pageParams.FullPath = SelectedDBPath.SelectedPath;
        }

        private void NewDBName_TextChanged()
        {
            pageParams.FileName = NewDBName.Text;
        }
    }
}
