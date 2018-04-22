using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class DataSource
    {
        [Key]
        public int DataSourceID { get; set; }
        public string DataSourceName { get; set; }
        public string ConnectionString { get; set; }

        
    }
}