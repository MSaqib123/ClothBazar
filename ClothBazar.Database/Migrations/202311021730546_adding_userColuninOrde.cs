namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_userColuninOrde : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaymentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaymentId");
        }
    }
}
