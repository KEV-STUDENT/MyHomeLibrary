namespace MyDBModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 2147483647),
                        FirstName = c.String(nullable: false, maxLength: 2147483647),
                        FirstName111 = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Key)
                .Index(t => t.LastName, name: "LastName")
                .Index(t => new { t.LastName, t.FirstName }, unique: true, name: "Name")
                .Index(t => t.FirstName, name: "FirstName");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Authors", "FirstName");
            DropIndex("dbo.Authors", "Name");
            DropIndex("dbo.Authors", "LastName");
            DropTable("dbo.Authors");
        }
    }
}
