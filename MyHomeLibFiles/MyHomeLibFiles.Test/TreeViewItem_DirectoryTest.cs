using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyHomeLibFiles.Test
{
    [TestClass]
    public class TreeViewItem_DirectoryTest
    {       
        [TestMethod]
        public void TreeViewItem_Directory_GetChilds()
        {
            string str = @"C:\1\";
            ITreeViewItem dir = new TreeViewItem_Directory(str);
            Assert.IsInstanceOfType(dir.GetChilds(), typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void TreeViewItem_Directory_GetChilds_Count()
        {
            string str = @"C:\1\";
            ITreeViewItem dir = new TreeViewItem_Directory(str);
            IEnumerable<string> collection = dir.GetChilds();
            int i = 0;
            foreach(var d in collection)
            {
                i++;
            }
            Assert.AreEqual<int>(12, i);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.UnauthorizedAccessException))]
        public void TreeViewItem_Directory_Access()
        {
            string str = @"C:\Config.Msi\";
            ITreeViewItem dir = new TreeViewItem_Directory(str);
            IEnumerable<string> collection = dir.GetChilds();
            foreach(var str1 in collection)
            {
                Debug.WriteLine(str1);
            }
            Assert.AreEqual(dir.State, ItemState.Error);
        }       
    }
}
