using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace MyHLibFiles.Test
{
    [TestClass]
    public class HLibBookTest
    {
        private string path = @"F:\1\test";
        private string name = "Davydov_Moskovit.454563.fb2";
        private IData data;
        [TestInitialize]
        public void Setup()
        {
            HLibFile fb2 = (HLibFile)HLibFactory.GetDiskItem(path, name);
            data  = fb2.GetDataFromFile();
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_IData()
        {
            Assert.IsInstanceOfType(data, typeof(IData));
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_HLibBook()
        {
            Assert.IsInstanceOfType(data, typeof(HLibBook));
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_HLibBookFB2()
        {
            Assert.IsInstanceOfType(data, typeof(HLibBookFB2));
        }
    }
}
