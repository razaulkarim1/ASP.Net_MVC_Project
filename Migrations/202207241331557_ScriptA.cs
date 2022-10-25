namespace work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        CarName = c.String(nullable: false, maxLength: 30),
                        TypeId = c.Int(nullable: false),
                        Make = c.Int(nullable: false),
                        RecordDate = c.DateTime(nullable: false, storeType: "date"),
                        isAvailable = c.Boolean(nullable: false),
                        PicturePath = c.String(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        CarId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        RentFee = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 14),
                        Email = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        CarId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        ReturnDate = c.DateTime(nullable: false, storeType: "date"),
                        Fine = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 10),
                        ConfirmPassword = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Rents", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Returns", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Returns", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Rents", "CarId", "dbo.Cars");
            DropIndex("dbo.Returns", new[] { "CustomerId" });
            DropIndex("dbo.Returns", new[] { "CarId" });
            DropIndex("dbo.Rents", new[] { "CustomerId" });
            DropIndex("dbo.Rents", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "TypeId" });
            DropTable("dbo.Registrations");
            DropTable("dbo.Types");
            DropTable("dbo.Returns");
            DropTable("dbo.Customers");
            DropTable("dbo.Rents");
            DropTable("dbo.Cars");
        }
    }
}
