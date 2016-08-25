namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElectricityClassesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consumptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeterReadings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeasurementTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tarifs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicableSince = c.DateTime(nullable: false),
                        ApplicableTill = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsumptionRanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountFrom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountTo = c.Decimal(precision: 18, scale: 2),
                        Tarif_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tarifs", t => t.Tarif_Id)
                .Index(t => t.Tarif_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumptionRanges", "Tarif_Id", "dbo.Tarifs");
            DropIndex("dbo.ConsumptionRanges", new[] { "Tarif_Id" });
            DropTable("dbo.ConsumptionRanges");
            DropTable("dbo.Tarifs");
            DropTable("dbo.Consumptions");
        }
    }
}
