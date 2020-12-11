using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFileTest
    {
        private string path = @"F:\1\test";
        private string name = "Davydov_Moskovit.454563.fb2";

        [TestMethod]
        public void HLibFile_Constructor()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.IsInstanceOfType(libFile, typeof(HLibFile));
        }

        [TestMethod]
        public void HLibFile_is_HLDiscItem()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.IsInstanceOfType(libFile, typeof(HLibDiscItem));
        }

        [TestMethod]
        public void HLibFile_is_IDispsable()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.IsInstanceOfType(libFile, typeof(IDisposable));
        }

        [TestMethod]
        public void HLibFile_get_Path()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.AreEqual(path, libFile.Path);
        }

        [TestMethod]
        public void HLibFile_get_Name()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.AreEqual(name, libFile.Name);
        }

        [TestMethod]
        public void HLibFile_get_FullName()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.AreEqual(Path.Combine(path, name), libFile.FullName);
        }

        [TestMethod]
        public void HLibFile_get_InArchive_False()
        {
            HLibFile libFile = new HLibFile(path, name);
            Assert.IsFalse(libFile.InArchive);
        }

        [TestMethod]
        public void HLibFile_get_InArchive_True()
        {
            HLibFile libFile = new HLibFile(path, name, true);
            Assert.IsTrue(libFile.InArchive);
        }


        [TestMethod]
        public void HLibFile_GetDiscItemsEnum()
        {
            HLibFile libFile = new HLibFile(path, name);
            IEnumerable<HLibDiscItem> list = libFile.GetDiscItemsEnum();
            Assert.IsInstanceOfType(list, typeof(IEnumerable<HLibDiscItem>));
        }

        [TestMethod]
        public void HLDiscItem_GetDiscItemsEnum()
        {
            HLibFile libFile = new HLibFile(path, name);
            IEnumerable<HLibDiscItem> list = libFile.GetDiscItemsEnum();
            Assert.IsInstanceOfType(list, typeof(IEnumerable<HLibDiscItem>));
        }
    }
}
