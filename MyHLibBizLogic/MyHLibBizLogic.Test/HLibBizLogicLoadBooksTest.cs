using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MyHLibBooks;

namespace MyHLibBizLogic.Test
{
    [TestClass]
    public class HLibBizLogicLoadBooksTest
    {
        private string path = @"F:\1\test\test1";
        private HLibBizLogic bl;

       [TestInitialize]
        public void Setup()
        {
            bl = new HLibBizLogic(path);            
        }

        [TestMethod]
        public void HLibBizLogic_LoadBooks()
        {
            IEnumerable<IData> books = bl.GetData();
            Assert.IsNotNull(books);
        }
    }
}
