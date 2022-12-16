using MyHLibBooks;
using MyHLibFiles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

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

        public List<IData> GetData()
        {
            return LoadDataFromDiskItem(_firstDiskItem);
        }

        private List<IData> LoadDataFromDiskItem(HLibDiscItem diskItem)
        {
            List<IData> result = new List<IData>();
            List<Task<IData>> tasks;
            Task finalTask;

            if (diskItem is IHLibDirectoryArchive dirArch)
            {                
                tasks = new List<Task<IData>>();
                
                Debug.WriteLine(diskItem.FullName);

                foreach(var item in dirArch.GetDiscItemsEnum())
                {
                    if (item is IHLibFileWithData fileData)
                    {
                        tasks.Add(fileData.GetDataFromFileAsync());                        
                    }
                    else if(item is IHLibDirectoryArchive dirZip)
                    {                       
                        foreach (var task in GetTaskForDirectory(dirZip))
                        {
                            tasks.Add(task);
                        }
                    }
                }

                if (tasks.Count > 0)
                {
                    finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(),
                        comlitedTasks =>
                        {
                            foreach (var t in comlitedTasks)
                            {
                                result.Add(t.Result);
                            }
                        });
                    finalTask.Wait();
                }
            }
            else if(diskItem is IHLibFileWithData fileData)
            {
                result.Add(fileData.GetDataFromFile());
            }
            else
            {
                throw new NotSupportedException();
            }
            
            return result;
        }

        private IEnumerable<Task<IData>> GetTaskForDirectory(IHLibDirectoryArchive dirArch)
        {
            foreach (var item in dirArch.GetDiscItemsEnum())
            {
                if (item is IHLibFileWithData fileData)
                {
                    yield return fileData.GetDataFromFileAsync();
                }
                else if (item is IHLibDirectoryArchive dirZip)
                {
                    foreach (var task in GetTaskForDirectory(dirZip))
                    {
                        yield return task;
                    }
                }
            }
            yield break;
        }
    }
}