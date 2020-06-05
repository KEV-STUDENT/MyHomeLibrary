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
    /// Логика взаимодействия для SelectParamText.xaml
    /// </summary>
    public partial class SelectParamText : System.Windows.Controls.UserControl
    {
        public event Action TextChanged;
        public string Text
        {
            get { return libText.Text; }
            set { libText.Text = value; }
        }

        public string LabelText
        {
            set { SetValue(LabelTextProperty, value); }
            get { return (string)GetValue(LabelTextProperty); }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
            "LabelText", typeof(string), typeof(SelectParamText),
            new UIPropertyMetadata("Select", new PropertyChangedCallback(LabelTextChanged) ));

        private static void LabelTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectParamText s = (SelectParamText)d;
            s.selText.Text = e.NewValue.ToString();
        }

        public SelectParamText()
        {
            InitializeComponent();
        }

        private void LibText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TextChanged?.Invoke();
        }
    }
}
