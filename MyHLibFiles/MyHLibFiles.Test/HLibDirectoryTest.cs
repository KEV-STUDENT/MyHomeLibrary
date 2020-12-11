using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibDirectoryTest
    {
        private string path = @"F:\1\";
        private string name = "test";
        private int cnt = 4;

        [TestMethod]
        public void HLibDirectory_Constructor()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            Assert.IsInstanceOfType(dir, typeof(HLibDirectory));
        }

        [TestMethod]
        public void HLibDirectory_is_HLDiscItem()
        {
            HLibDirectory di = new HLibDirectory(path, name);
            Assert.IsInstanceOfType(di, typeof(HLibDiscItem));
        }

        [TestMethod]
        public void HLibDirectory_get_Path()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            Assert.AreEqual(path, dir.Path);
        }

        [TestMethod]
        public void HLibDirectory_get_Name()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            Assert.AreEqual(name, dir.Name);
        }

        [TestMethod]
        public void HLibDirectory_get_FullName()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            Assert.AreEqual(Path.Combine(path, name), dir.FullName);
        }

        [TestMethod]
        public void HLibDirectory_get_InArchive_False()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            Assert.IsFalse(dir.InArchive);
        }

        [TestMethod]
        public void HLibDirectory_get_InArchive_True()
        {
            HLibDirectory dir = new HLibDirectory(path, name, true);
            Assert.IsTrue(dir.InArchive);
        }

        [TestMethod]
        public void HLibDirectory_GetDiscItemsEnum()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            IEnumerable<HLibDiscItem> list = dir.GetDiscItemsEnum();
            Assert.IsInstanceOfType(list, typeof(IEnumerable<HLibDiscItem>));
        }

        [TestMethod]
        public void HLDiscItem_GetDiscItemsEnum()
        {
            HLibDiscItem dir = new HLibDirectory(path, name);
            IEnumerable<HLibDiscItem> list = dir.GetDiscItemsEnum();
            Assert.IsInstanceOfType(list, typeof(IEnumerable<HLibDiscItem>));
        }

        [TestMethod]
        public void HLibDirectory_GetDiscItemsEnum_Count()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            IEnumerable<HLibDiscItem> list = dir.GetDiscItemsEnum();
            foreach(var item in list)
            {
                Debug.WriteLine("{0} , {1}", item.Name, item.Path);
            }
            Assert.AreEqual(cnt, list.Count());
        }

        [TestMethod]
        public void HLibDirectory_GetDiscItemsList_Count()
        {
            HLibDirectory dir = new HLibDirectory(path, name);
            List<HLibDiscItem> list = dir.GetDiscItemsList();
            foreach (var item in list)
            {
                Debug.WriteLine("{0} , {1}", item.Name, item.Path);
            }
            Assert.AreEqual(cnt, list.Count());
        }
    }
}
