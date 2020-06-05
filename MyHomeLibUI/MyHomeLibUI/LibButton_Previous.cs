using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibUI
{
    public class LibButton_Previous: LibButton
    {
        public LibButton_Previous()
        {
            imageFile = "prev_16.png";
            text = "Previous";
            Initialize();
            Width = 90;
        }

    }
}
