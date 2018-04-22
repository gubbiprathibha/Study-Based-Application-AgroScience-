using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class LoginModel
    {
        [Required]
        
        public string LoginID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
        
        public string Password { get; set; }
    }
}
