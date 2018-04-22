using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.WebApp.MVC.Models
{
    public class StudyModels
    {

        public int StudyID { get; set; }
        public string StudyCode { get; set; }
        public string StudyName { get; set; }
        public string SponsorName { get; set; }
        public string Status { get; set; }
        public DateTime StudyStartDate { get; set; }
    }
}