using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.BO.Models
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public List<PagePermissionModel> MyPermissionAllow { get; set; }
        public List<PagePermissionModel> MyPermissionDeny { get; set; }
    }
}