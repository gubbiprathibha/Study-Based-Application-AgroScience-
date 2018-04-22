namespace StudyBasedApplication.Data.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataSources",
                c => new
                    {
                        DataSourceID = c.Int(nullable: false, identity: true),
                        DataSourceName = c.String(),
                        ConnectionString = c.String(),
                    })
                .PrimaryKey(t => t.DataSourceID);
            
            CreateTable(
                "dbo.DataSourceStudyStatus",
                c => new
                    {
                        DataSourceStudyStatusID = c.Int(nullable: false, identity: true),
                        StudyStatusName = c.String(),
                    })
                .PrimaryKey(t => t.DataSourceStudyStatusID);
            
            CreateTable(
                "dbo.LocalStudyStatus",
                c => new
                    {
                        LocalStudyStatusID = c.Int(nullable: false, identity: true),
                        StudyStatusName = c.String(),
                    })
                .PrimaryKey(t => t.LocalStudyStatusID);
            
            CreateTable(
                "dbo.NavigationLogs",
                c => new
                    {
                        NavigationLogID = c.Int(nullable: false, identity: true),
                        BrowserInfo = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        PageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NavigationLogID)
                .ForeignKey("dbo.Pages", t => t.PageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageID = c.Int(nullable: false, identity: true),
                        PageCode = c.String(),
                        PageName = c.String(),
                    })
                .PrimaryKey(t => t.PageID);
            
            CreateTable(
                "dbo.PagePermissions",
                c => new
                    {
                        PagePermissionID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        PageID = c.Int(nullable: false),
                        PermissionStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PagePermissionID)
                .ForeignKey("dbo.Pages", t => t.PageID, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupCode = c.String(),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Gender = c.String(nullable: false),
                        EmailID = c.String(nullable: false, maxLength: 150),
                        Company = c.String(),
                        Job = c.String(),
                        Department = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(nullable: false),
                        LoginID = c.String(nullable: false, maxLength: 10),
                        Password = c.String(maxLength: 200),
                        PasswordSalt = c.String(maxLength: 200),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserGroups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.EmailID, unique: true)
                .Index(t => t.LoginID, unique: true)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.StudyPermissions",
                c => new
                    {
                        StudyPermissionID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SponsorID = c.Int(nullable: false),
                        StudyID = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudyPermissionID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.StudyStatusMappings",
                c => new
                    {
                        StudyStatusMappingID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        DataSourceStudyStatusID = c.Int(nullable: false),
                        LocalStudyStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudyStatusMappingID)
                .ForeignKey("dbo.DataSourceStudyStatus", t => t.DataSourceStudyStatusID, cascadeDelete: true)
                .ForeignKey("dbo.LocalStudyStatus", t => t.LocalStudyStatusID, cascadeDelete: true)
                .Index(t => t.DataSourceStudyStatusID)
                .Index(t => t.LocalStudyStatusID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudyStatusMappings", "LocalStudyStatusID", "dbo.LocalStudyStatus");
            DropForeignKey("dbo.StudyStatusMappings", "DataSourceStudyStatusID", "dbo.DataSourceStudyStatus");
            DropForeignKey("dbo.StudyPermissions", "UserID", "dbo.Users");
            DropForeignKey("dbo.NavigationLogs", "UserID", "dbo.Users");
            DropForeignKey("dbo.NavigationLogs", "PageID", "dbo.Pages");
            DropForeignKey("dbo.Users", "GroupID", "dbo.UserGroups");
            DropForeignKey("dbo.PagePermissions", "GroupID", "dbo.UserGroups");
            DropForeignKey("dbo.PagePermissions", "PageID", "dbo.Pages");
            DropIndex("dbo.StudyStatusMappings", new[] { "LocalStudyStatusID" });
            DropIndex("dbo.StudyStatusMappings", new[] { "DataSourceStudyStatusID" });
            DropIndex("dbo.StudyPermissions", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "GroupID" });
            DropIndex("dbo.Users", new[] { "LoginID" });
            DropIndex("dbo.Users", new[] { "EmailID" });
            DropIndex("dbo.PagePermissions", new[] { "PageID" });
            DropIndex("dbo.PagePermissions", new[] { "GroupID" });
            DropIndex("dbo.NavigationLogs", new[] { "PageID" });
            DropIndex("dbo.NavigationLogs", new[] { "UserID" });
            DropTable("dbo.StudyStatusMappings");
            DropTable("dbo.StudyPermissions");
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
            DropTable("dbo.PagePermissions");
            DropTable("dbo.Pages");
            DropTable("dbo.NavigationLogs");
            DropTable("dbo.LocalStudyStatus");
            DropTable("dbo.DataSourceStudyStatus");
            DropTable("dbo.DataSources");
        }
    }
}
