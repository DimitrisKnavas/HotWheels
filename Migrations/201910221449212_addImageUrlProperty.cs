namespace HotWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageUrlProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "ImageUrl");
        }
    }
}
