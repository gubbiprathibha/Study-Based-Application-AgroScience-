using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class PasswordModel
    {
        
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})",ErrorMessage="Invalid password")]
        
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
        public string ConfirmPassword { get; set; }

    }
}
