using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class PagePermission
    {
        [Key]
        public int PagePermissionID { get; set; }
        public int GroupID { get; set; }
        public UserGroup UserGroup { get; set; }


        public int PageID { get; set; }
        public Page Page { get; set; }

        public bool PermissionStatus { get; set; }   
    }
}