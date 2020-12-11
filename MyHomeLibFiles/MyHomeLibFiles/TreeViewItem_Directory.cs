using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using MyDBModel;
using System.Threading.Tasks;


namespace MyHomeLibFiles
{
    public class TreeViewItem_Directory : ITreeViewItem
    {
        private readonly object locker = new object();
        private ItemState state = ItemState.Initial;
        private string path;

        public TreeViewItem_Directory(string str)
        {            
            this.path = str;
        }

        public ItemType Type => ItemType.Directory;

        public string Name {
            get
            {
                DirectoryInfo info = new DirectoryInfo(path);
                return info.Name;
            }
        }

        public ItemState State {
            get { return state; }
            set { state = value; }
        }

        public IEnumerable<string> GetChilds()
        {
            IEnumerable<string> collectionDir, collectionFile;
            try
            {
                collectionDir = Directory.EnumerateDirectories(path);
                collectionFile = Directory.EnumerateFiles(path);
            }
            catch (System.UnauthorizedAccessException)
            {
                state = ItemState.Error;
                yield break;
            }

            foreach (string dir in collectionDir)
            {
                yield return dir;
            }

            foreach (string file in collectionFile)
            {
                yield return file;
            }
        }

        public List<Book> GetChilds_Books()
        {
            List<Book> books = new List<Book>();
            List<Book> childsBbooks;

            var list = GetChilds_Items();
            //foreach (var item in GetChilds_Items())
            //{
            //    try
            //    {
            //        childsBbooks = item.GetChilds_Books();
            //        books.AddRange(childsBbooks);
            //    }catch (NotImplementedException e)
            //    {
            //        //
            //    }
            //}


            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism =
                      Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
            };

            Parallel.ForEach(list, po, entry =>
            {
                try
                {
                    childsBbooks = entry.GetChilds_Books();
                   //lock(locker)
                    {
                        books.AddRange(childsBbooks);
                    }
                }
                catch (NotImplementedException e)
                {
                    //
                }
            });

            return books;
        }

        public List<MyDBModel.DBFile> GetChilds_Files()
        {
            List<MyDBModel.DBFile> files = new List<MyDBModel.DBFile>();
            List<MyDBModel.DBFile> childsFiles;

            var list = GetChilds_Items();
            Debug.WriteLine("Childs: {0}", list.Count);
            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism =
                        Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
            };

            Parallel.ForEach(list, po, entry =>
            {
                try
                {
                    childsFiles = entry.GetChilds_Files();
                    files.AddRange(childsFiles);
                }
                catch (NotImplementedException e)
                {
                    //
                }
            });
            Debug.WriteLine("Files: {0}", files.Count);
            return files;
        }

        public List<ITreeViewItem> GetChilds_Items()
        {
            List<ITreeViewItem> list = new List<ITreeViewItem>();
            foreach (string item in GetChilds())
            {
                list.Add(TreeItemsFactory.GetItem(item));
            }
            return list;
        }
    }
}