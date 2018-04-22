namespace StudyBasedApplication.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lakhan_kamala : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSponsors",
                c => new
                    {
                        UserSponsorID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SponsorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSponsorID);
            
            AddColumn("dbo.StudyStatusMappings", "DataSource", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudyStatusMappings", "DataSource");
            DropTable("dbo.UserSponsors");
        }
    }
}
