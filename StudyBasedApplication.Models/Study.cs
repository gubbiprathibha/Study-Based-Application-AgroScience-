using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.Models
{
    public class Study
    {
        public int StudyID { get; set; }
        public string StudyCode { get; set; }
        public string StudyName { get; set; }
        public string StudyReference { get; set; }
        public DateTime StudyStartDate { get; set; }
        public DateTime StudyEndDate { get; set; }
        public string StudyStatus { get; set; }
        public Sponsor Sponsor { get; set; }

        List<Activity> activities = new List<Activity>();
        public void SetActivity(Activity activity)
        {
            this.activities.Add(activity);
        }
        public List<Activity> GetActivities()
        {
            return this.activities;
        }
        public StudyCategory StudyCategory { get; set; }
        public StudyType StudyType { get; set; }
        public StudyStatusMapping StudyStatusMapping { get; set; }
        public StudyPermission StudyPermission { get; set; }
       


    }
}