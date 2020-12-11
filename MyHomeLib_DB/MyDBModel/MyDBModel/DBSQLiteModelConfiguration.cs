
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.Entity.Core.Common;

namespace MyDBModel
{
    public class DBSQLiteModelConfiguration : DbConfiguration
    {
        public DBSQLiteModelConfiguration()
        {
            SetDefaultConnectionFactory(new DBSQLiteConnectioFactory());

            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}
