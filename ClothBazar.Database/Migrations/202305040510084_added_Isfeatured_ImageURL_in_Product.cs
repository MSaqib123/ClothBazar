namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Isfeatured_ImageURL_in_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageURL", c => c.String());
            AddColumn("dbo.Products", "isFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "isFeatured");
            DropColumn("dbo.Products", "ImageURL");
        }
    }
}
