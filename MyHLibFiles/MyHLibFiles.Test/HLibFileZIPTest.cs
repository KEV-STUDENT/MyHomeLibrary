using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFileZIPTest
    {

        private string path = @"F:\1\test\test1";
        private string name = "fb2-203897-204340.zip";

        [TestMethod]
        public void HLibFileZIP_Constructor()
        {
            HLibFileZIP zip = new HLibFileZIP(path, name);
            Assert.IsInstanceOfType(zip, typeof(HLibFileZIP));
        }

        [TestMethod]
        public void HLibFileZIP_is_HLDiscItem()
        {
            HLibFileZIP zip = new HLibFileZIP(path, name);
            Assert.IsInstanceOfType(zip, typeof(HLibFile));
        }

        [TestMethod]
        public void HLibFileZIP_GetDiscItemsEnum_Count()
        {
            HLibFileZIP zip = new HLibFileZIP(path, name);
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
            HLibFileZIP zip = new HLibFileZIP(path, name);
            List<HLibDiscItem> list = zip.GetDiscItemsList();
            Assert.IsTrue(list[0].InArchive);
        }
    }
}
