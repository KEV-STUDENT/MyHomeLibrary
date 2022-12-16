using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MyHLibBooks;

namespace MyHLibBizLogic.Test
{
    [TestClass]
    public class HLibBizLogicLoadBooksInFileTest
    {
        private string path = @"F:\1\test";
        private string name = "Davydov_Moskovit.454563.fb2";

        private HLibBizLogic bl;

       [TestInitialize]
        public void Setup()
        {
            bl = new HLibBizLogic(path, name);            
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
            Assert.AreEqual<int>(1, books.Count);
        }

    }
}
