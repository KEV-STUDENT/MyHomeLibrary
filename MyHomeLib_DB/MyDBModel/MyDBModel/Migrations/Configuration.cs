namespace MyDBModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;
    using System.Linq;
    using MyHomeLibCommon;

    internal sealed class Configuration : DbMigrationsConfiguration<MyDBModel.DBSQLiteModel>
    {
        private readonly bool _pendingMigrations;
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());            
        }

        public Configuration(string connectionStr):this()
        {
            var migrator = new DbMigrator(this);
            _pendingMigrations = migrator.GetPendingMigrations().Any();

            if (_pendingMigrations)
            {
                migrator.Update();
                Seed(new MyDBModel.DBSQLiteModel(connectionStr));
            }
        }

        protected override void Seed(MyDBModel.DBSQLiteModel context)
        {

            foreach(ItemGenre item in Enum.GetValues(typeof(ItemGenre)))
            {
                if (context.Genres.Find(item) == null)
                {
                    context.Genres.Add(new Genre { Key = item, Code = Enum.GetName(typeof(ItemGenre), item) });
                }
            }
            context.SaveChanges();
        }
    }
}
