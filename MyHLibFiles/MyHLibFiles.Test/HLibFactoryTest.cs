using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFactoryTest
    {
        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibDiscItem()
        {
            string path = @"F:\1\";
            string name = "test";

            HLibDiscItem item = HLibFactory.GetDiskItem(path, name);
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibDirectory()
        {
            string path = @"F:\1\";
            string name = "test";

            HLibDiscItem item = HLibFactory.GetDiskItem(path, name);
            Assert.IsInstanceOfType(item, typeof(HLibDirectory));
        }

        [TestMethod]
        public void HLibFactory_GetDiscItem_HLibFileZIP()
        {
            string path = @"F:\1\test\test1";
            string name = "fb2-203897-204340.zip";

            HLibDiscItem item = HLibFactory.GetDiskItem(path, name);
            Assert.IsInstanceOfType(item, typeof(HLibFileZIP));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionPath))]
        public void HLibFactory_ExceptionPath()
        {
            string path = @"1111111";
            string name = "222222222";

            HLibDiscItem item = HLibFactory.GetDiskItem(path, name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionAccess))]
        public void HLibFactory_ExceptionAccess()
        {
            string path = @"F:\1\test\test1";
            string name = "fb2-203897-204340_EXCEPTED.zip";

            HLibDiscItem item = HLibFactory.GetDiskItem(path, name);
        }
    }
}
