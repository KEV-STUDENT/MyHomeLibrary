using MyHLibBooks;
using System.Threading.Tasks;

namespace MyHLibFiles
{
    public interface IHLibFileWithData
    {
        IData GetDataFromFile();
        Task<IData> GetDataFromFileAsync();
    }
}
