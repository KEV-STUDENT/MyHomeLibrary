using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Common;

namespace MyDBModel
{
    class DBModelMigrateInitializer : MigrateDatabaseToLatestVersion<DBModel, Migrations.Configuration>
    {
        public DBModelMigrateInitializer(string connectionString) 
            : base(true, new Migrations.Configuration()
            { TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SQLite") })
        {
        }

        public DBModelMigrateInitializer(DbConnection connection):this(connection.ConnectionString)
        { }
    }
}
