namespace MyDBModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "FirstName111", c => c.String(maxLength: 2147483647));
            //DropColumn("dbo.Authors", "FirstName1_1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "FirstName1_1", c => c.String(maxLength: 2147483647));
            //DropColumn("dbo.Authors", "FirstName111");
        }
    }
}
