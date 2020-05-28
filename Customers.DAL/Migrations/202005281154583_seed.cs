namespace Customers.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 150),
                        DateEdited = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        SurName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 100),
                        Position = c.String(maxLength: 100),
                        CompanyId = c.Guid(),
                        DateEdited = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "CompanyId", "dbo.Company");
            DropIndex("dbo.Person", new[] { "CompanyId" });
            DropTable("dbo.Person");
            DropTable("dbo.Company");
        }
    }
}
