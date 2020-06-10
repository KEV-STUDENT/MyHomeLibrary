using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHomeLibFiles;
using MyDBModel;
using System.Data.Common;

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

        [TestMethod]
        public void MyDBUpdater_FillContextFromItemView_FB2()
        {
            string fileSource = @"C:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";
            string fileDB = @"c:\1\TEST_3.sqlite";

            ITreeViewItem item = TreeItemsFactory.GetItem(fileSource);
            int result;

            using (DBModel db = new DBModel(fileDB))
            {
                MyDBUpdater dbu = new MyDBUpdater();
                result = dbu.FillContextFromItemView(dbu, item);
            }
            Assert.AreEqual(1, result);
        }

    }
}
