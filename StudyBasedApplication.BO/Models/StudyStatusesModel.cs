using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.BO.Models
{
    public class StudyStatusesModel
    {
        public string MapID { get; set; }

        public string groups { get; set; }

        public string datasource { get; set; }


        public string dataSourceStatus { get; set; }

        [Required]
        public string localStatus { get; set; }
    }
}