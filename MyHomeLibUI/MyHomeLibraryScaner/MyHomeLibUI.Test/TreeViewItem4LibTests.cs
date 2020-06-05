using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyHomeLibFiles;

namespace MyHomeLibUI.Test
{
    [TestClass]
    public class TreeViewItem4LibTests
    {
        [TestMethod]
        public void TreeViewItem4Lib_New_Empty()
        {
            ITreeView4Lib treeItem = new TreeView4Lib();
            Assert.IsInstanceOfType(treeItem, typeof(TreeViewItem));
        }

        [TestMethod]
        public void TreeViewItem4Lib_New_String()
        {
            ITreeView4Lib treeItem = new TreeView4Lib("String");
            Assert.IsInstanceOfType(treeItem, typeof(TreeViewItem));
        }

        [TestMethod]
        public void TreeViewItem4Lib_Header()
        {
            string header = "Header1";
            TreeViewItem treeItem = new TreeView4Lib(header);
            Assert.AreEqual<string>((string)treeItem.Header, header);
        }

        [TestMethod]
        public void TreeViewItem4Lib_New_Empty_LibItem()
        {
            ITreeView4Lib treeItem = new TreeView4Lib();
            Assert.IsInstanceOfType(treeItem.LibItem, typeof(TreeViewItem_Empty));
        }

        [TestMethod]
        public void TreeViewItem4Lib_New_Attribute_LibItem()
        {
            string str = "Test";
            ITreeView4Lib treeItem = new TreeView4Lib(str);
            Assert.IsInstanceOfType(treeItem.LibItem, typeof(TreeViewItem_Attribute));
        }

       /* [TestMethod]
        public void TreeViewItem4Lib_LoadChilds()
        {
            string str = @"C:\1\";
            ITreeView4Lib treeItem = new TreeView4Lib(str);
            treeItem.LoadChilds();
            Assert.AreEqual<int>(((TreeViewItem)treeItem).Items.Count, 8);
        }

        [TestMethod]
        public void TreeViewItem4Lib_IsLoaded()
        {
            string str = @"C:\1\";
            ITreeView4Lib treeItem = new TreeView4Lib(str);
            treeItem.LoadChilds();
            Assert.IsTrue(treeItem.IsChildsLoaded);
        }

        [TestMethod]
        public void TreeViewItem4Lib_SetShowParams()
        {
            string str = @"C:\1\";
            ITreeView4Lib treeItem = new TreeView4Lib(str);
            treeItem.LoadChilds();
            TreeViewItem item = (TreeViewItem)treeItem;
            Assert.AreEqual<FontWeight>(item.FontWeight, FontWeights.Bold);
        }*/

        [TestMethod]
        public void TreeViewItem4Lib_New_File_LibItem()
        {
            string str = @"C:\1\SYSERROR.DBF";
            ITreeView4Lib treeItem = new TreeView4Lib(str);
            //Assert.IsInstanceOfType(treeItem.LibItem, typeof(TreeViewItem_File));
            TreeViewItem item = (TreeViewItem)treeItem;
            Assert.AreEqual<FontWeight>(item.FontWeight, FontWeights.Normal);
        }
    }
}
