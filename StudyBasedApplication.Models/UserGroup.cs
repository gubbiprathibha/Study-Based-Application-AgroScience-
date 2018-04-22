using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudyBasedApplication.Models
{
    public class UserGroup
    {
        
        [Key]
        public int GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
       
      
        public  List<User> Users { get; set; }
        public List<PagePermission> PagePermission { get; set; }
        
        
        
    }
}