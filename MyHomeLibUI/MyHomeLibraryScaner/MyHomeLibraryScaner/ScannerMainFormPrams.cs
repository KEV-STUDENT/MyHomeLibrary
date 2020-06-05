using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHomeLibFiles;
using MyHomeLibUI;

namespace MyHomeLibraryScaner
{
    public class ScannerMainFormPrams:IOptionPageParams, IFB2PageParams
    {
        private ItemUpdateType actionType = ItemUpdateType.Create;
        public ItemUpdateType ActionType
        {
            set { actionType = value; }
            get { return actionType; }
        }

        private string fullPathOptionPage = "";
        public string FullPath {
             get { return fullPathOptionPage; }
             set { fullPathOptionPage = value; }
         }

        private string fileName = "";
        public string FileName {
            get { return fileName; }
            set { fileName = value; }
        }

        private string path = "";
        public string Path {
            get { return path; }
            set { path = value; }
        }

        private string itemPath = "";
        public string ItemPath
        {
            get { return itemPath; }
            set { itemPath = value; }
        }

        private string dirPath = "";
        public string DirPath
        {
            get { return dirPath; }
            set { dirPath = value; }
        }

        private string filePath = "";
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private ITreeViewItem libItem = new TreeViewItem_Empty();
        public ITreeViewItem LibItem {
            get { return libItem; }
            set { libItem = value; }
        }

        private ItemSourceType sourceType = ItemSourceType.Directory;
        public ItemSourceType SourceType
        {
            get { return sourceType; }
            set { sourceType = value; }
        }
    }
}
