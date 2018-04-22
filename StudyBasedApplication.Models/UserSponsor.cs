using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
   public class UserSponsor
    {
        [Key]
        public int UserSponsorID { get; set; }
        public int UserID { get; set; }
        public int SponsorID { get; set; }
    }
}
