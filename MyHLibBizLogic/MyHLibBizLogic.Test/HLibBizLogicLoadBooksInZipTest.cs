using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using MyHLibBooks;

namespace MyHLibBizLogic.Test
{
    [TestClass]
    public class HLibBizLogicLoadBooksInZIPTest
    {
        string path = @"F:\1\test\test1";
        string name = "fb2-203897-204340.zip";

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
            Debug.WriteLine("===================== {0} ==========================", books.Count) ;
            foreach(var b in books)
            {
                Debug.WriteLine(((HLibBookFB2)b).Title);
            }
            Assert.AreEqual<int>(293, books.Count);
        }

    }
}
