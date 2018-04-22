using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class NavigationLog
    {
        [Key]
        public int NavigationLogID { get; set; }
        public string BrowserInfo { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int PageID { get; set; }
        public Page Page { get; set; }

    }
}