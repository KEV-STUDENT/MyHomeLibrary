using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Ionic.Zip;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFileZIPTest
    {

        private string path = @"F:\1\test\test1";
        private string name = "fb2-203897-204340.zip";
        private string entryName = "203897.fb2";
        HLibFileZIP zip;

        [TestInitialize]
        public void Setup()
        {
            zip = new HLibFileZIP(path, name);
        }

        [TestMethod]
        public void HLibFileZIP_Constructor()
        {
            Assert.IsInstanceOfType(zip, typeof(HLibFileZIP));
        }

        [TestMethod]
        public void HLibFileZIP_is_HLDiscItem()
        {
            Assert.IsInstanceOfType(zip, typeof(HLibFile));
        }

        [TestMethod]
        public void HLibFileZIP_GetDiscItemsEnum_Count()
        {
            IEnumerable<HLibDiscItem> list = zip.GetDiscItemsEnum();
            foreach (var item in list)
            {
                Debug.WriteLine("{0} , {1}", item.Name, item.Path);
            }
            Assert.AreEqual(293, list.Count());
        }

        [TestMethod]
        public void HLibFileZIP_GetDiscItemsList_InArchive()
        {
            List<HLibDiscItem> list = zip.GetDiscItemsList();
            Assert.IsTrue(list[0].InArchive);
        }

        [TestMethod]
        public void HLibFileZIP_GetDiscItemsList_GetEntryByName()
        {
            ZipEntry entry = zip.GetEntryByName(entryName);
            Assert.IsInstanceOfType(entry, typeof(ZipEntry));
        }
    }
}
