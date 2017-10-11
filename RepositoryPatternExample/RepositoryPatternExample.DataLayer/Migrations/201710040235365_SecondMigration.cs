namespace RepositoryPatternExample.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Addresses", newSchema: "RepositoryTest");
            MoveTable(name: "dbo.Customers", newSchema: "RepositoryTest");
            MoveTable(name: "dbo.Orders", newSchema: "RepositoryTest");
        }
        
        public override void Down()
        {
            MoveTable(name: "RepositoryTest.Orders", newSchema: "dbo");
            MoveTable(name: "RepositoryTest.Customers", newSchema: "dbo");
            MoveTable(name: "RepositoryTest.Addresses", newSchema: "dbo");
        }
    }
}
