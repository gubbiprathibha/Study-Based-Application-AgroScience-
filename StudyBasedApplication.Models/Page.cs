using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class Page
    {
        [Key]
        public int PageID { get; set; }
        public string PageCode { get; set; }
        public string PageName { get; set; }
        public List<PagePermission> PagePermissions { get; set; }


    }
}
