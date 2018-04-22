using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.BO.Models
{
    public class SponsorPermissionModel
    {
        public int SponsorID { get; set; }
        public string SponsorName { get; set; }
        public bool ShowAllStudies { get; set; }
        public bool Remove { get; set; }
    }
}