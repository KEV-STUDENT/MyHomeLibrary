using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Common;

namespace MyDBModel
{
    class DBSQLiteModelMigrateInitializer : MigrateDatabaseToLatestVersion<DBSQLiteModel, Migrations.Configuration>
    {
        public DBSQLiteModelMigrateInitializer(string connectionString) 
            : base(true, new Migrations.Configuration(connectionString)
            { TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SQLite") })
        {
        }

        public DBSQLiteModelMigrateInitializer(DbConnection connection):this(connection.ConnectionString)
        { }
    }
}
