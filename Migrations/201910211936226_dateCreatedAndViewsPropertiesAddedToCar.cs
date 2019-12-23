namespace HotWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateCreatedAndViewsPropertiesAddedToCar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cars", "Views", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Views");
            DropColumn("dbo.Cars", "Created");
        }
    }
}
