using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MyHomeLibUI
{
    public class LibButton:Button
    {
        protected string imageFile = "";
        protected string text = "";
        protected FlowDirection flowDirection = FlowDirection.LeftToRight;
       
        public LibButton()
        {
            FontSize = 12;
            FontFamily = new FontFamily("Times New Roman");

            Initialize();
        }

        public virtual void Initialize()
        {
            bool isImg = !string.IsNullOrEmpty(imageFile);
            bool isTxt = !string.IsNullOrEmpty(text);
            Width = 80;

            if(isImg)
            {

                Image img = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/MyHomeLibUI;component/Sources/" + imageFile))
                };

                StackPanel stackPnl = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    FlowDirection = this.flowDirection
                };

                stackPnl.Children.Add(img);
                if(isTxt)
                {
                    TextBlock txt = new TextBlock
                    {
                        Text = text,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(5)
                    };
                    stackPnl.Children.Add(txt);
                }
                this.Content = stackPnl; ;
            }else if(isTxt)
            {
                this.Content = text;
            }
        }

    }
}
