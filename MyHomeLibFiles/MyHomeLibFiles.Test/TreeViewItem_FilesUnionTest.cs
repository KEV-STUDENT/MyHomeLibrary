using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Diagnostics;

namespace MyHomeLibFiles.Test
{
    [TestClass]
    public class TreeViewItem_FilesUnionTest
    {
        [TestMethod]
        public void TreeViewItem_FilesUnion_Items_Type()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            List<ITreeViewItem> childs = zip.GetChilds_Items();            
            Assert.AreEqual<ItemType>(childs[0].Type, ItemType.FilesUnion);
        }

        [TestMethod]
        public void TreeViewItem_FilesUnion_GetChilds()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            List<ITreeViewItem> childs = zip.GetChilds_Items();
            IEnumerable<string> files = childs[0].GetChilds();
            foreach (string file in files)
            {
                Debug.WriteLine(file);
            }
            Assert.IsInstanceOfType(files, typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void TreeViewItem_FilesUnion_GetChilds_Items_Count()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            List<ITreeViewItem> childs = zip.GetChilds_Items();
            List<ITreeViewItem> files = childs[0].GetChilds_Items();
            foreach (var file in files)
            {
                Debug.WriteLine(file.Name);
            }
            Assert.AreEqual<int>(101, files.Count);
        }
    }
}
