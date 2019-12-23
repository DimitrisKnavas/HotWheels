namespace HotWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModelTableAndRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            AddColumn("dbo.Cars", "ModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "ModelId");
            AddForeignKey("dbo.Cars", "ModelId", "dbo.Models", "Id", cascadeDelete: true);
            DropColumn("dbo.Cars", "Brand");
            DropColumn("dbo.Cars", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Model", c => c.String());
            AddColumn("dbo.Cars", "Brand", c => c.String());
            DropForeignKey("dbo.Cars", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "ModelId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropColumn("dbo.Cars", "ModelId");
            DropTable("dbo.Models");
        }
    }
}
