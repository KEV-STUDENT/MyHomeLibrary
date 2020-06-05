using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
namespace MyHomeLibFiles.Test
{
    [TestClass]
    public class TreeViewItem_FileFB2Test
    {
        public void TreeViewItem_FileFB2_New()
        {
            string str = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            TreeViewItem_FileZIP zip = new TreeViewItem_FileZIP(str);
            Assert.IsInstanceOfType(zip, typeof(TreeViewItem_File));
        }

        [TestMethod]
        public void TreeViewItem_FileFB2_GetInfo()
        {
            string str = @"C:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";           
            TreeViewItem_FileFB2 item = (TreeViewItem_FileFB2)TreeItemsFactory.GetItem(str);
            IEnumerable<string> childs = item.GetChilds();
            foreach (string child in childs)
            {
                Debug.WriteLine(child);
            }
            Assert.IsInstanceOfType(childs, typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void TreeViewItem_FileFB2Zip_GetInfo()
        {
            string zip = @"C:\librus_MyHomeLib\flibusta\Davydov_Moskovit.454563.fb2.zip";
            string fb2 = "Davydov_Moskovit.454563.fb2";
            ITreeViewItem item = TreeItemsFactory.GetItem(zip, fb2);
            IEnumerable<string> childs = item.GetChilds();
            foreach (string child in childs)
            {
                Debug.WriteLine(child);
            }
            Assert.IsInstanceOfType(childs, typeof(IEnumerable<string>));
        }
    }
}
