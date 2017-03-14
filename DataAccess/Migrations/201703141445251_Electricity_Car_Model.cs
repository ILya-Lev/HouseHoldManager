namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Electricity_Car_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Distances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Petrols",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        BonusBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PetrolType = c.Int(nullable: false),
                        GasolinStation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GasolinStations", t => t.GasolinStation_Id)
                .Index(t => t.GasolinStation_Id);
            
            CreateTable(
                "dbo.GasolinStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_Country = c.String(),
                        Address_City = c.String(),
                        Address_Street = c.String(),
                        Address_Building = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TechServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        Date = c.DateTime(nullable: false),
                        MaterialPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Petrols", "GasolinStation_Id", "dbo.GasolinStations");
            DropIndex("dbo.Petrols", new[] { "GasolinStation_Id" });
            DropTable("dbo.TechServices");
            DropTable("dbo.GasolinStations");
            DropTable("dbo.Petrols");
            DropTable("dbo.Distances");
            DropTable("dbo.Accidents");
        }
    }
}
