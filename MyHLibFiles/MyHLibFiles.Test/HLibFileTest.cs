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
        private HLibFile libFile;

        [TestInitialize]
        public void Setup()
        {
            libFile = new HLibFileFB2(path, name);
        }

        [TestMethod]
        public void HLibFile_Constructor()
        {
            Assert.IsInstanceOfType(libFile, typeof(HLibFile));
        }

        [TestMethod]
        public void HLibFile_is_HLDiscItem()
        {
            Assert.IsInstanceOfType(libFile, typeof(HLibDiscItem));
        }

        [TestMethod]
        public void HLibFile_is_IDispsable()
        {
            Assert.IsInstanceOfType(libFile, typeof(IDisposable));
        }

        [TestMethod]
        public void HLibFile_get_Path()
        {
            Assert.AreEqual(path, libFile.Path);
        }

        [TestMethod]
        public void HLibFile_get_Name()
        {
            Assert.AreEqual(name, libFile.Name);
        }

        [TestMethod]
        public void HLibFile_get_FullName()
        {
            Assert.AreEqual(Path.Combine(path, name), libFile.FullName);
        }

        [TestMethod]
        public void HLibFile_get_InArchive()
        {
            Assert.IsFalse(libFile.InArchive);
        }
    }
}
