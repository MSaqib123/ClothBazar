namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chanignOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalProducts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalProducts");
        }
    }
}
