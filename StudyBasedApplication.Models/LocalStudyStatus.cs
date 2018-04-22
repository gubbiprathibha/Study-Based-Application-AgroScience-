using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
   public class LocalStudyStatus
    {
       [Key]
        public int LocalStudyStatusID { get; set; }
        public string StudyStatusName { get; set; }
    }
}
