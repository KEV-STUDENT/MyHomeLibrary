using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyHomeLibFiles.Test
{
    [TestClass]
    public class TreeViewItem_FileFB2Test
    {
        public void TreeViewItem_FileFB2_New()
        {
            string str = @"E:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            TreeViewItem_FileZIP zip = new TreeViewItem_FileZIP(str);
            Assert.IsInstanceOfType(zip, typeof(TreeViewItem_File));
        }

        [TestMethod]
        public void TreeViewItem_FileFB2_GetInfo()
        {
            string str = @"E:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";           
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
            string zip = @"E:\librus_MyHomeLib\flibusta\Davydov_Moskovit.454563.fb2.zip";
            string fb2 = "Davydov_Moskovit.454563.fb2";
            ITreeViewItem item = TreeItemsFactory.GetItem(zip, fb2);
            IEnumerable<string> childs = item.GetChilds();
            foreach (string child in childs)
            {
                Debug.WriteLine(child);
            }
            Assert.IsInstanceOfType(childs, typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void TreeViewItem_FileFB2_GetChilds_Items()
        {
            string str = @"E:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";
            TreeViewItem_FileFB2 item = (TreeViewItem_FileFB2)TreeItemsFactory.GetItem(str);
            
            IEnumerable<ITreeViewItem> childs = item.GetChilds_Items();
            foreach (ITreeViewItem child in childs)
            {
                if(child is TreeViewItem_Attribute)
                {
                    Debug.WriteLine(((TreeViewItem_Attribute)child).AttributeType);
                }
                Debug.WriteLine("========================");
                IEnumerable<string> childs1 = child.GetChilds();
                foreach (string child1 in childs1)
                {
                    Debug.WriteLine(child1);
                }
                Debug.WriteLine(child.Name);
            }
            Assert.IsInstanceOfType(childs, typeof(IEnumerable<ITreeViewItem>));
        }

        [TestMethod]
        public void TreeViewItem_FileFB2_GetAuthors()
        {
            string str = @"E:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";
            TreeViewItem_FileFB2 item = (TreeViewItem_FileFB2)TreeItemsFactory.GetItem(str);

            Debug.WriteLine("============1============");

            var authors = item.GetAuthors();
            foreach (ITreeViewItem child in authors)
            {
                Debug.WriteLine(child.Name);
                Debug.WriteLine("========================");
                foreach(var itm in child.GetChilds_Items())
                {
                    Debug.WriteLine(itm.Name);
                }
                Debug.WriteLine("========================");
            }
            Assert.AreEqual(3, authors.Count());
        }
    }
}
