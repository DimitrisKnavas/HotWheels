namespace HotWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPropertiesToCarModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "cc", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Cars", "IsNegotiable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Description");
            DropColumn("dbo.Cars", "IsNegotiable");
            DropColumn("dbo.Cars", "Price");
            DropColumn("dbo.Cars", "cc");
        }
    }
}
