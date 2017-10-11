namespace RepositoryPatternExample.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressType = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemQuantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.Customer_CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Addresses", new[] { "Customer_CustomerId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
