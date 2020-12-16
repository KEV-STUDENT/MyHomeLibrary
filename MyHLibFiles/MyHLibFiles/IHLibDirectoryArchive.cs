using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHLibFiles
{
    public interface IHLibDirectoryArchive
    {
        IEnumerable<HLibDiscItem> GetDiscItemsEnum();
        List<HLibDiscItem> GetDiscItemsList();
    }
}
