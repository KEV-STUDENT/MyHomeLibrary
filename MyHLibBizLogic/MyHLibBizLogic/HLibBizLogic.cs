using MyHLibFiles;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using MyHLibBooks;
using System.Threading.Tasks;

namespace MyHLibBizLogic
{
    public class HLibBizLogic
    {
        private HLibDiscItem _firstDiskItem;

        public HLibBizLogic() : this("") { }
        public HLibBizLogic(string path) : this(path, "") { }

        public HLibBizLogic(string path, string name) : this(path, name, false) { }

        public HLibBizLogic(string path, string name, bool inArchive)
        {
            _firstDiskItem = HLibFactory.GetDiskItem(path, name, inArchive);
        }

        public HLibDiscItem FirstDiskItem { get => _firstDiskItem; }

        public IEnumerable<IData> GetData()
        {
            //var collection = new Coll
            if(_firstDiskItem is IHLibDirectoryArchive dirArch)
            {
                //List<Task<IData>> tasks = new List<Task<IData>>();

                foreach(var item in dirArch.GetDiscItemsEnum())
                {
                    if(item is IHLibFileWithData fileData)
                    {
                        //tasks.Add(fileData.GetDataFromFileAsync());
                        yield return fileData.GetDataFromFile();
                    }
                }
            }
            yield break;
        }
    }
}