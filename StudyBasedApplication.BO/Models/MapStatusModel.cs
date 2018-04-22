using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace StudyBasedApplication.BO.Models
{
    public class MapStatusModel
    {
        public int StudyStatusMappingID { get; set; }
        public int GroupID { get; set; }
        public int DataSourceStudyStatusID { get; set; }
        [DisplayName("DataSourceStudyStatus")]
        public string TDataSourceStudyStatus { get; set; }
        public int LocalStudyStatusID { get; set; }
        [DisplayName("LocalStudyStatus")]
        public string TLocalStudyStatus { get; set; }
        [DisplayName("DataSource")]
        public string TDataSource { get; set; }
    }
}