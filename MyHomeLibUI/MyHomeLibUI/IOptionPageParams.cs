using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeLibUI
{
    public interface IOptionPageParams
    {
        ItemUpdateType ActionType { get; set; }
        string FullPath { get; set; }
        string FileName { get; set; }
        string Path { get; set; }
    }
}
