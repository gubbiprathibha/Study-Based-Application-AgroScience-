using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.BO.Models
{
    public class UserProfileModel
    {
        public User User { get; set; }
        public List<Sponsor> Sponsors { get; set; }
      

    }
}