namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderUserInfoes", "userName", c => c.String());
            AlterColumn("dbo.OrderItems", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderUserInfoes", "userId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderUserInfoes", "userId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "TotalPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "OrderId", c => c.String());
            DropColumn("dbo.OrderUserInfoes", "userName");
        }
    }
}
