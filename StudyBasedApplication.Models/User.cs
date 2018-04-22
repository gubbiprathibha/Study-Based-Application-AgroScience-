using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyBasedApplication.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [RegularExpression(@"\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3}",ErrorMessage="Invalid Email Address")]
       
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        [Index(IsUnique=true)]
        public string EmailID { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        [StringLength(10,MinimumLength=3)]
        [Index(IsUnique=true)]
        public string LoginID { get; set; }
        [DataType(DataType.Password)]
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(200)]
        public string PasswordSalt { get; set; }
        [Required]
        public int GroupID { get; set; }
        public UserGroup UserGroup { get; set; }
        List<Sponsor> Sponsors = new List<Sponsor>();
        public void SetSponsor(Sponsor sponsor)
        {
            this.Sponsors.Add(sponsor);
        }
        public List<Sponsor> GetSponsors()
        {
            return this.Sponsors;
        }


    }
}