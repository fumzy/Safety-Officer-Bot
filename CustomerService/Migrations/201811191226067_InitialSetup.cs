namespace CustomerService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportingActivities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HazardCategory = c.String(),
                        Preventive = c.String(),
                        Corrective = c.String(),
                        SafetyOfficer = c.String(),
                        StaffName = c.String(),
                        Department = c.String(),
                        DateOfObservation = c.DateTime(),
                        DateLogged = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReportingActivities");
        }
    }
}
