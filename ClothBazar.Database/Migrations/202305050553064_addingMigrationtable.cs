namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMigrationtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Confs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.configs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.configs",
                c => new
                    {
                        key = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.key);
            
            DropTable("dbo.Confs");
        }
    }
}
