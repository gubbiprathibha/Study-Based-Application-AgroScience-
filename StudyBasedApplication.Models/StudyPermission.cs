using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class StudyPermission
    {
        [Key]
        public int StudyPermissionID { get; set; }
        public int UserID { get; set; }
       
        public virtual User User { get; set; }
        public int SponsorID { get; set; }
        public int StudyID { get; set; }
        public bool Status { get; set; }

    }
}