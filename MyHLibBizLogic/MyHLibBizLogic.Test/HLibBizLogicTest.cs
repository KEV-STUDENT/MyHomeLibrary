using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyHLibFiles;
namespace MyHLibBizLogic.Test
{
    [TestClass]
    public class HLibBizLogicTest
    {

        [TestMethod]
        public void HLibBizLogic_Constructor_Archive_FileName_InZip()
        {
            string path = @"F:\1\test\test1\fb2-203897-204340.zip";
            string name = "203897.fb2";

            HLibBizLogic bl = new HLibBizLogic(path, name, true);
            Assert.IsInstanceOfType(bl.FirstDiskItem, typeof(HLibFileFB2));
        }

        [TestMethod]
        public void HLibBizLogic_Constructor_Archive_FileName_InArchive()
        {
            string path = @"F:\1\test\test1\fb2-203897-204340.zip";
            string name = "203897.fb2";

            HLibBizLogic bl = new HLibBizLogic(path, name, true);
            Assert.IsTrue(bl.FirstDiskItem.InArchive);
        }

        [TestMethod]
        public void HLibBizLogic_Constructor_Path_FileName()
        {
            string path = @"F:\1\test";
            string name = "Davydov_Moskovit.454563.fb2";

            HLibBizLogic bl = new HLibBizLogic(path, name);
            Assert.IsInstanceOfType(bl.FirstDiskItem, typeof(HLibFileFB2));
        }

        [TestMethod]
        public void HLibBizLogic_Constructor_Path()
        {
            string path = @"F:\1\test\test1";

            HLibBizLogic bl = new HLibBizLogic(path);
            Assert.IsInstanceOfType(bl.FirstDiskItem, typeof(HLibDirectory));
        }
    }
}
