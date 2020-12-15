using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using MyHLibBooks;
using MyHLibFiles;

namespace MyHLibBooks.Test
{
    [TestClass]
    public class HLibBook_FB2_Test
    {
        private string path = @"F:\1\test";
        private string name = "Davydov_Moskovit.454563.fb2";
        private HLibBookFB2 book;
        [TestInitialize]
        public void Setup()
        {
            HLibFile fb2 = (HLibFile)HLibFactory.GetDiskItem(path, name);
            book  = (HLibBookFB2) fb2.GetDataFromFile();
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
            Assert.AreEqual<string>("Московит", book.Title);
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
            Assert.AreEqual(3, book.Authors.Count());
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Genres_NotNull()
        {
            Assert.IsNotNull(book.Genres);
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Genres_Count()
        {
            Assert.AreEqual(4, book.Genres.Count());
        }

        [TestMethod]
        public void HLibBook_GetDataFromFile_Annotation_NotNull()
        {
            Debug.WriteLine(book.Annotation);
            Assert.IsInstanceOfType(book.Annotation, typeof(string));
        }
    }
}
