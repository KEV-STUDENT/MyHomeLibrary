using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyHomeLibBizLogic.Test
{
    [TestClass]
    public class MyDBUpdaterTest
    {
        [TestMethod]
        public void MyDBUpdater_Constructor()
        {
            MyDBUpdater dbu = new MyDBUpdater();
            Assert.IsInstanceOfType(dbu, typeof(MyDBUpdater));
        }

        [TestMethod]
        public void MyDBUpdater_Constructor_SQLiteFile_ZipFile()
        {
            string file_SQLite = @"c:\1\TEST_3.sqlite";
            string file_ZIP = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";

            MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_ZIP);
            Assert.IsInstanceOfType(dbu, typeof(MyDBUpdater));
        }

        [TestMethod]
        public void MyDBUpdater_ProcessUpdate()
        {
            string file_SQLite = @"c:\1\TEST_3.sqlite";
            string file_ZIP = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";

            MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_ZIP);
            Assert.IsTrue(dbu.ProcessUpdate());
        }

        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        [TestMethod]
        public void MyDBUpdater_ProcessUpdate_Exception_fileSource()
        {
            string file_SQLite = @"c:\1\TEST_3.sqlite";
            MyDBUpdater dbu = new MyDBUpdater();
            dbu.FileDestination = file_SQLite;
            Assert.IsTrue(dbu.ProcessUpdate());
        }

        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        [TestMethod]
        public void MyDBUpdater_ProcessUpdate_Exception_fileDestination()
        {
            string file_ZIP = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip";
            MyDBUpdater dbu = new MyDBUpdater();
            dbu.FileSource = file_ZIP;
            Assert.IsTrue(dbu.ProcessUpdate());
        }

        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        [TestMethod]
        public void MyDBUpdater_ProcessUpdate_Exception_fileDestination_WrongFile()
        {
            string file_ZIP = @"C:\librus_MyHomeLib\lib.rus.ec\fb2-000024-030559.zip111";
            MyDBUpdater dbu = new MyDBUpdater();
            dbu.FileSource = file_ZIP;
            Assert.IsTrue(dbu.ProcessUpdate());
        }
    }
}
