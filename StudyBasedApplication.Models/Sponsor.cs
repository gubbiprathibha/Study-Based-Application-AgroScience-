using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.Models
{
    public class Sponsor
    {
        public int SponsorID { get; set; }
        public string SponsorCode { get; set; }
        public string SponsorName { get; set; }
        List<Study> studies = new List<Study>();
        public void SetStudy(Study study)
        {
            this.studies.Add(study);
        }
        public List<Study> GetStudies()
        {
            return this.studies;
        }
        List<User> users = new List<User>();
        public void SetUser(User user)
        {
            this.users.Add(user);
        }
        public List<User> GetUsers()
        {
            return this.users;
        }
    }

}