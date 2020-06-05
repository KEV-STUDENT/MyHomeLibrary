using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyHomeLibFiles.Test
{
    [TestClass]
    public class TreeItemsFactoryTest
    {
        [TestMethod]
        public void GetItem_Empty()
        {
            ITreeViewItem item = TreeItemsFactory.GetItem();
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_Empty));
        }

        [TestMethod]
        public void GetItem_Empty_Status()
        {
            ITreeViewItem item = TreeItemsFactory.GetItem();
            Assert.AreEqual<ItemType>(item.Type, ItemType.Empty);
        }

        [TestMethod]
        public void GetItem_Attribute()
        {
            string str = "Test";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_Attribute));
        }

        [TestMethod]
        public void GetItem_Attribute_Status()
        {
            string str = "Test";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.AreEqual<ItemType>(item.Type, ItemType.Attribute);
        }

        [TestMethod]
        public void GetItem_Attribute_Name()
        {
            string str = "Test";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.AreEqual<string>(item.Name, str);
        }

        [TestMethod]
        public void GetItem_Directory()
        {
            string str = @"C:\1\";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_Directory));
        }

        [TestMethod]
        public void GetItem_Directory_Status()
        {
            string str = @"C:\1\";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.AreEqual<ItemType>(item.Type, ItemType.Directory);
        }

        [TestMethod]
        public void GetItem_Directory_Name()
        {
            string str = @"C:\1\";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.AreEqual<string>(item.Name, "1");
        }

        [TestMethod]
        public void GetItem_File()
        {
            string str = @"C:\1\SYSERROR.DBF";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_File));
        }

        [TestMethod]
        public void GetItem_File_Status()
        {
            string str = @"C:\1\SYSERROR.DBF";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.AreEqual<ItemType>(item.Type, ItemType.File);
        }

        [TestMethod]
        public void GetItem_FileZIP()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.AreEqual<ItemType>(item.Type, ItemType.Zip);
        }

        [TestMethod]
        public void GetItem_FileinZIP()
        {
            string zip = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            string file = "24.fb2";
            ITreeViewItem item = TreeItemsFactory.GetItem(zip, file);
            Assert.AreEqual<ItemType>(item.Type, ItemType.InZip);
        }

        [TestMethod]
        public void GetItem_FileinZIP_isFile()
        {
            string zip = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            string file = "24.fb2";
            ITreeViewItem item = TreeItemsFactory.GetItem(zip, file);
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_File));
        }

        [TestMethod]
        public void GetItem_FileinFB2()
        {
            string str = @"C:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";
            ITreeViewItem item = TreeItemsFactory.GetItem(str);
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_FileFB2));
        }

        [TestMethod]
        public void GetItem_FileinFB2_ZIP()
        {
            string zip = @"C:\librus_MyHomeLib\flibusta\Davydov_Moskovit.454563.fb2.zip";
            string fb2 = "Davydov_Moskovit.454563.fb2";
            ITreeViewItem item = TreeItemsFactory.GetItem(zip, fb2);
            Assert.IsInstanceOfType(item, typeof(TreeViewItem_FileFB2));
        }
    }
}
