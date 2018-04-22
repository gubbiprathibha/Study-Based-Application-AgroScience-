using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Data.ADO
{
   public interface IStudyRepositry
    {
        //List<Study> GetAllStudies();
        List<Study> GetStudiesBySponsorID(int SponsorID);
        List<StudyCategory> GetAllCategories();
        //List<Study> GetAllCategories();
        List<Activity> GetActivities(int StudyID);
        Sponsor GetSpoonsor(int StudyID);
        Study GetStudyByStudyID(int StudyID);
    }
}
