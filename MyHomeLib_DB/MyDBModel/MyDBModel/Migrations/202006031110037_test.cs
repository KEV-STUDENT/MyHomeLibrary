namespace MyDBModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
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
                        FirstName1_1 = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Key)
                .Index(t => t.LastName, name: "LastName")
                .Index(t => new { t.LastName, t.FirstName }, unique: true, name: "Name")
                .Index(t => t.FirstName, name: "FirstName");
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Key)
                .Index(t => t.Caption, unique: true, name: "Caption");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Books", "Caption");
            DropIndex("dbo.Authors", "FirstName");
            DropIndex("dbo.Authors", "Name");
            DropIndex("dbo.Authors", "LastName");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
