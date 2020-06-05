using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyHomeLibFiles.Test
{
    [TestClass]
    public class TreeViewItem_FileZIPTest
    {
        [TestMethod]
        public void TreeViewItem_FileZIP_New()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            TreeViewItem_FileZIP zip = new TreeViewItem_FileZIP(str);
            Assert.IsInstanceOfType(zip, typeof(TreeViewItem_File));
        }

        [TestMethod]
        public void TreeViewItem_FileZIP_Type()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            TreeViewItem_FileZIP zip = new TreeViewItem_FileZIP(str);
            Assert.AreEqual<ItemType>(zip.Type, ItemType.Zip);
        }

        [TestMethod]
        public void TreeViewItem_FileZIP_is_File()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            TreeViewItem_FileZIP zip = new TreeViewItem_FileZIP(str);
            Assert.IsTrue(zip is TreeViewItem_File );
        }

        [TestMethod]
        public void TreeViewItem_FileZIP_GetChilds()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            IEnumerable<string> childs = zip.GetChilds();
            foreach(string child in childs)
            {
                Debug.WriteLine(child);
            }
            Assert.IsInstanceOfType(childs, typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void TreeViewItem_FileZIP_GetChilds_Count()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            IEnumerable<string> childs = zip.GetChilds();
            int i = 0;
            foreach (string child in childs)
            {
                Debug.WriteLine(child);
                i++;
            }
            Assert.AreEqual<int>(228, i);
        }

        [TestMethod]
        public void TreeViewItem_GetChilds_Items_Count()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            List<ITreeViewItem> childs = zip.GetChilds_Items();
            Debug.WriteLine(childs.Count);
            foreach (var child in childs)
            {
                Debug.WriteLine(child.Name);
            }
            Assert.AreEqual<int>(228, childs.Count);
        }

        [TestMethod]
        public void TreeViewItem_GetChilds_Items_Type()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem zip = new TreeViewItem_FileZIP(str);
            List<ITreeViewItem> childs = zip.GetChilds_Items();
            Debug.WriteLine(childs.Count);
            Assert.IsInstanceOfType(childs[0], typeof(TreeViewItem_FilesUnion));
        }
    }
}
