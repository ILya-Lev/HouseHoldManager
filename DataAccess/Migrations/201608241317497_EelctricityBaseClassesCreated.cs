namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EelctricityBaseClassesCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElectricityConsumptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeterReadings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeasurementTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ElectricityTarifs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApplicableSince = c.DateTime(nullable: false),
                        ApplicableTill = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ElectricityTarifs");
            DropTable("dbo.ElectricityConsumptions");
        }
    }
}
