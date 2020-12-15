using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHLibFiles;
using System.Collections.Generic;
using System.Linq;

namespace MyHLibBooks.Test
{
    [TestClass]
    public class HLibBook_FB2_ZIPTest
    {
        private string path = @"F:\1\test\test1";
        private string name = "fb2-203897-204340.zip";
        private HLibBookFB2 book;

        [TestInitialize]
        public void Setup()
        {
            HLibFileZIP zip = (HLibFileZIP)HLibFactory.GetDiskItem(path, name);

            List<HLibDiscItem> list = zip.GetDiscItemsList();
            HLibFileFB2 fb2 = (HLibFileFB2)list[0];
            book = (HLibBookFB2)fb2.GetDataFromFile();
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_HLibBook()
        {
            Assert.IsInstanceOfType(book, typeof(HLibBook));
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_HLibBookFB2()
        {
            Assert.IsInstanceOfType(book, typeof(HLibBookFB2));
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Title_NotNull()
        {
            Assert.IsNotNull(book.Title);
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Title()
        {
            Assert.AreEqual<string>("Вторая книга", book.Title);
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_KeyWords_NotNull()
        {
            Assert.IsNotNull(book.KeyWords);
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Authors_NotNull()
        {
            Assert.IsNotNull(book.Authors);
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Authors_Count()
        {
            Assert.AreEqual(1, book.Authors.Count());
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Genres_NotNull()
        {
            Assert.IsNotNull(book.Genres);
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Genres_Count()
        {
            Assert.AreEqual(1, book.Genres.Count());
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Annotation_NotNull()
        {
            Assert.IsInstanceOfType(book.Annotation, typeof(string));
        }
    }
}
