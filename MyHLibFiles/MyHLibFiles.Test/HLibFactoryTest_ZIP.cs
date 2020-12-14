using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ionic.Zip;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFactoryTest_ZIP
    {
        private string path = @"F:\1\test\test1";
        private string name = "fb2-203897-204340.zip";
        private string fullPath = @"F:\1\test\test1\fb2-203897-204340.zip";

        private HLibDiscItem item;

        [TestInitialize]
        public void Setup()
        {   
            item = HLibFactory.GetDiskItem(path, name);
        }

        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibFileZIP()
        {
            Assert.IsInstanceOfType(item, typeof(HLibFileZIP));
        }

        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibFileFB2_in_ZIP()
        {
            HLibDiscItem fb2;
            using (ZipFile zip = ZipFile.Read(fullPath))
            {
                ZipEntry[] zipEntries = new ZipEntry[zip.Entries.Count];
                zip.Entries.CopyTo(zipEntries, 0);
                fb2 = HLibFactory.GetDiskItem((HLibFileZIP)item, zipEntries[0]);
            }
            Assert.IsInstanceOfType(fb2, typeof(HLibFileFB2));
        }
    }
}
