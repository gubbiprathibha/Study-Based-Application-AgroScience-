using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.BO.Models
{
    public class StudyModel
    {
        public int StudyID { get; set; }
        public string StudyCode { get; set; }
        public string StudyName { get; set; }
        public string SponsorName { get; set; }
        public bool Status { get; set; }
        public bool Selected { get; set; }
    }
}