using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHomeLibFiles;
using MyDBModel;

using System.Diagnostics;

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
        public void MyDBUpdater_ProcessUpdate_ZIP()
        {
            string file_SQLite = @"F:\1\TEST_3.sqlite";
            string file_ZIP = @"E:\librus_MyHomeLib\lib.rus.ec\fb2-203897-204340.zip";


            //MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_ZIP, true);
            MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_ZIP, false, true);
            Assert.IsTrue(dbu.ProcessUpdate());
        }

        [TestMethod]
        public void MyDBUpdater_ProcessUpdate_FB2()
        {
            string file_SQLite = @"F:\1\TEST_3.sqlite";
            string file_FB2 = @"E:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";


            MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_FB2);
            Assert.IsTrue(dbu.ProcessUpdate());
        }

        [TestMethod]
        public void MyDBUpdater_ProcessUpdate_Directory()
        {
            string file_SQLite = @"F:\1\TEST_DIR.sqlite";
            //string file_Dir = @"E:\librus_MyHomeLib";
            //string file_Dir = @"E:\librus_MyHomeLib\flibusta";
            //string file_Dir = @"E:\librus_MyHomeLib\lib.rus.ec";
            string file_Dir = @"F:\1\test";

            MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_Dir, false, true);
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
