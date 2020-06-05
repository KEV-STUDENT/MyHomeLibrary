using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace MyDBModel.Test
{
    [TestClass]
    public class DBSQLiteConnectioFactoryTest
    {
        string file = @"c:\1\TEST_2.sqlite";

        [TestMethod]
        public void DBSQLiteConnectioFactory_Constructor()
        {
            DBSQLiteConnectioFactory connFactory = new DBSQLiteConnectioFactory();
            Assert.IsInstanceOfType(connFactory, typeof(IDbConnectionFactory));
        }

        [TestMethod]
        public void DBSQLiteConnectioFactory_CreateConnection()
        {
            DBSQLiteConnectioFactory connFactory = new DBSQLiteConnectioFactory();
            DbConnection dbConnection = connFactory.CreateConnection(file);

            Assert.IsInstanceOfType(dbConnection, typeof(DbConnection));
        }
    }
}
