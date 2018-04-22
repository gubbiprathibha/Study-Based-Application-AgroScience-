using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.BO.Models
{
    public class SponsorModel
    {
        public int id { get; set; }
        public List<SponsorPermissionModel> SponsorPermissions { get; set; }
    }
}