using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class DataSourceStudyStatus
    {
        [Key]
        public int DataSourceStudyStatusID { get; set; }
        public string StudyStatusName { get; set; }

        


    }
}