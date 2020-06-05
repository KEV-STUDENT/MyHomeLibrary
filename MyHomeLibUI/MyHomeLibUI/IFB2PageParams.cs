using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHomeLibFiles;

namespace MyHomeLibUI
{
    public interface IFB2PageParams
    {
        ItemSourceType SourceType { get; set; }
        string DirPath { get; set; }
        string FilePath { get; set; }
        string ItemPath { get; set; }
        ITreeViewItem LibItem { get; set; }
    }
}
