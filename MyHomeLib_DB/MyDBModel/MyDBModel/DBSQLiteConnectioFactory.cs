using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Diagnostics;

namespace MyDBModel
{
    public class DBSQLiteConnectioFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            Debug.WriteLine("DBSQLiteConnectioFactory :" + nameOrConnectionString);
            return new SQLiteConnection(nameOrConnectionString);
        }
    }
}