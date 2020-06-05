using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.Entity;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.Common;
using System.Linq;

namespace MyDBModel.Test
{
    [TestClass]
    public class DBModelTest
    {
        [TestMethod]
        public void DBModel_Constructor()
        {
            var db = new DBModel();
            Assert.IsInstanceOfType(db, typeof(System.Data.Entity.DbContext));
        }

        [TestMethod]
        public void DBModel_Constructor_Connection_Test()
        {
            string file = @"c:\1\TEST_1.sqlite";
            string connString = "Data Source=" + file + "; version = 3 ";
            SQLiteConnection connection = new SQLiteConnection(connString);

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (var dB = new DBModel(connection))
            {
                dB.Database.CreateIfNotExists();
                var authors = dB.Authors.ToListAsync();
            }

            Assert.IsTrue(File.Exists(file));
        }

        [TestMethod]
        public void DBModel_Constructor_String_Test()
        {
            string file = @"c:\1\TEST_2.sqlite";
            string libPathTest = "Data Source=" + file + "; version = 3 ";

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (var dB = new DBModel(libPathTest))
            {
                dB.Database.CreateIfNotExists();
                var authors = dB.Authors.ToListAsync();
            }
            Assert.IsTrue(File.Exists(file));
        }


        [TestMethod]
        public void DBModel_Constructor_ExistedDB_String_Test()
        {
            string file = @"c:\1\TEST_3.sqlite";
            string libPathTest = "Data Source=" + file + "; version = 3 ";

            using (var dB = new DBModel(libPathTest))
            {
                dB.Database.CreateIfNotExists();
                var authors = dB.Authors.ToList();
                foreach(var a in authors)
                {
                    Debug.WriteLine("{0} : {1} {2} {3}", a.Key, a.FirstName, a.FirstName, a.FirstName111);
                }
            }
            Assert.IsTrue(File.Exists(file));
        }
    }
}

