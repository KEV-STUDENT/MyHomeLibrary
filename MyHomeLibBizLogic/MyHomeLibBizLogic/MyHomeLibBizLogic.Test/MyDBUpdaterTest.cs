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

            
            MyDBUpdater dbu = new MyDBUpdater(file_SQLite, file_ZIP);
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
            string fileSource = @"E:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";
            string fileDB = @"F:\1\TEST_3.sqlite";

            string libPathTest = "Data Source=" + fileDB + "; version = 3 ";

            ITreeViewItem item = TreeItemsFactory.GetItem(fileSource);
            int result = 0;

            using (DBModel db = new DBModel(libPathTest))
            {
               MyDBUpdater dbu = new MyDBUpdater(true);
               dbu.FillContextFromItemView(db, item);
                try
                {
                    result = db.SaveChanges();
                }
                catch(SystemException e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            Assert.AreNotEqual<int>(0, result);
        }

        [TestMethod]
        public void MyDBUpdater_FillContextFromItemView_FB2_DuplicatedBook()
        {
            string fileSource = @"E:\librus_MyHomeLib\Davydov_Moskovit.454563.fb2";
            string fileDB = @"F:\1\TEST_3.sqlite";

            string libPathTest = "Data Source=" + fileDB + "; version = 3 ";

            ITreeViewItem item = TreeItemsFactory.GetItem(fileSource);
            int result;

            using (DBModel db = new DBModel(libPathTest))
            {
                MyDBUpdater dbu = new MyDBUpdater(true);
                result = dbu.FillContextFromItemView(db, item);

                dbu = new MyDBUpdater();
                result = dbu.FillContextFromItemView(db, item);
            }
            Assert.AreEqual<int>(0, result);
        }

    }
}
