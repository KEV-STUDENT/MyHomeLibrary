using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Data.Entity;
using System.Data.Common;

using System.Data.SQLite;

using SQLite.CodeFirst;

namespace MyDBModel
{
    [DbConfigurationType(typeof( DBSQLiteModelConfiguration ))]
    public class DBSQLiteModel : DbContext
    {
        //"name=MyDBContext"
        public DBSQLiteModel():base()
        {
            Database.SetInitializer<DBSQLiteModel>(null);
        }

        public DBSQLiteModel(string path):base(path)
        {
            Database.SetInitializer<DBSQLiteModel>(new DBSQLiteModelMigrateInitializer(path));
        }

        public DBSQLiteModel(DbConnection connection): base(connection, true)
        {
            Database.SetInitializer<DBSQLiteModel>(new DBSQLiteModelMigrateInitializer(connection));
        }


        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Debug.WriteLine("-=4=-");
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DBModel>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }*/

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<KeyWord> KeyWords { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<DBFile> DBFiles { get; set; }
    }
}
