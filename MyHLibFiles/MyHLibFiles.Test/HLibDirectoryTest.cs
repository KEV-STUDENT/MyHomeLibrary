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
        private int cnt = 5;
        HLibDirectory dir;

        [TestInitialize]
        public void Setup()
        {
            dir = new HLibDirectory(path, name);
        }

        [TestMethod]
        public void HLibDirectory_Constructor()
        {
            Assert.IsInstanceOfType(dir, typeof(HLibDirectory));
        }

        [TestMethod]
        public void HLibDirectory_is_HLDiscItem()
        {
            Assert.IsInstanceOfType(dir, typeof(HLibDiscItem));
        }

        [TestMethod]
        public void HLibDirectory_get_Path()
        {
            Assert.AreEqual(path, dir.Path);
        }

        [TestMethod]
        public void HLibDirectory_get_Name()
        {
            Assert.AreEqual(name, dir.Name);
        }

        [TestMethod]
        public void HLibDirectory_get_FullName()
        {
            Assert.AreEqual(Path.Combine(path, name), dir.FullName);
        }

        [TestMethod]
        public void HLibDirectory_get_InArchive()
        {
            Assert.IsFalse(dir.InArchive);
        }

        [TestMethod]
        public void HLibDirectory_GetDiscItemsEnum_Count()
        {
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
            List<HLibDiscItem> list = dir.GetDiscItemsList();
            foreach (var item in list)
            {
                Debug.WriteLine("{0} , {1}", item.Name, item.Path);
            }
            Assert.AreEqual(cnt, list.Count());
        }
    }
}
