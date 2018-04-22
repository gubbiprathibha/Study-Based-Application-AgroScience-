using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Data.EFRepository
{
    public class PrimaryDBContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PagePermission> PagePermissions { get; set; }
        public DbSet<StudyPermission> StudyPermissions { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceStudyStatus> DataSourceStudyStatuses { get; set; }
        public DbSet<LocalStudyStatus> LocalStudyStatuses { get; set; }
        public DbSet<StudyStatusMapping> StudyStatusMappings { get; set; }
        public DbSet<NavigationLog> NavigationLogs { get; set; }
        public DbSet<UserSponsor> UserSponsors { get; set; }

    }
}