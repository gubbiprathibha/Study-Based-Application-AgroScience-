using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.BO.Models
{
    public class PagePermissionModel
    {
        public int PageID{get;set;}
        public string PageName { get; set; }
        public bool Selected { get; set; }
        
    }
}