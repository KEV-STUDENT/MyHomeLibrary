using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyHomeLibFiles;
using MyDBModel;

using System.Diagnostics;

namespace MyHomeLibBizLogic
{
    public class MyDBUpdater
    {
        private string fileDestination;
        private string fileSource;

        public MyDBUpdater()
        {
        }

        public MyDBUpdater(string file_SQLite, string file_ZIP)
        {
            this.fileDestination = file_SQLite;
            this.fileSource = file_ZIP;
        }

        public string FileSource
        {
            get {return fileSource; }
            set { fileSource = value; }
        }

        public string FileDestination
        {
            get {return fileDestination; }
            set {fileDestination = value; }
        }

        public bool ProcessUpdate()
        {
            if(string.IsNullOrEmpty(fileDestination))
            {
                throw new ArgumentOutOfRangeException("fileDestination", "Wrong database name");
            }

            if(string.IsNullOrEmpty(fileSource))
            {
                throw new ArgumentOutOfRangeException("fileSource", "Wrong source file name");
            }

            ITreeViewItem item = TreeItemsFactory.GetItem(fileSource);
            if(item.State == ItemState.Error)
            {
                throw new ArgumentOutOfRangeException("fileSource", "Wrong source file name");
            }

            using (DBModel db = new DBModel(fileDestination))
            {
                foreach(var i in item.GetChilds_Items())
                {

                    Debug.WriteLine(i.Type);
                }
            }
            return false;
        }
    }
}
