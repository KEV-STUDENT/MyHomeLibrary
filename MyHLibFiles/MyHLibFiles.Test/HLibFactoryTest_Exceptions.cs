using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibFactoryTest_Exceptions
    {
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
