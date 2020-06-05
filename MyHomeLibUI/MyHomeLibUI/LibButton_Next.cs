using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MyHomeLibUI
{
    public class LibButton_Next : LibButton
    {
        public LibButton_Next()
        {
            imageFile = "next_16.png";
            text = "Next";
            flowDirection = FlowDirection.RightToLeft;
            Initialize();
        }
    }
}
