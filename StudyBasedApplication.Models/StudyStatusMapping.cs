using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class StudyStatusMapping
    {
        [Key]
        public int StudyStatusMappingID { get; set; }
        public int GroupID { get; set; }
        public int DataSourceStudyStatusID { get; set; }
        public virtual DataSourceStudyStatus DataSourceStudyStatus{get;set;}
        public int LocalStudyStatusID { get; set; }
        public virtual LocalStudyStatus LocalStudyStatus { get; set; }
        public string DataSource { get; set; }
    }
}