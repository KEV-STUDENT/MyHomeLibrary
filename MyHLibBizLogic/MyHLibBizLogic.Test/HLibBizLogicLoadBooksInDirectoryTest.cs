using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MyHLibBooks;

namespace MyHLibBizLogic.Test
{
    [TestClass]
    public class HLibBizLogicLoadBooksInDirectoryTest
    {
        //private string path = @"F:\1\test\test1";
        private string path = @"F:\1\test";

        private HLibBizLogic bl;

       [TestInitialize]
        public void Setup()
        {
            bl = new HLibBizLogic(path);            
        }

        [TestMethod]
        public void HLibBizLogic_LoadBooks()
        {
            List<IData> books = bl.GetData();
            Assert.IsNotNull(books);
        }

        [TestMethod]
        public void HLibBizLogic_LoadBooks_Count()
        {
            List<IData> books = bl.GetData();
            Assert.AreNotEqual<int>(1, books.Count);
        }

    }
}
