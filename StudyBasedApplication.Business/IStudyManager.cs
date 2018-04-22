using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Business
{
   public interface IStudyManager
    {
        bool AllowStudyPermission(int studyid, int userid,string sponsorname);
         bool DenyStudyPermission(int studyid, int userid, string sponsorname);
         bool CheckStudyPermission(int userid, int sponsorid);
         bool InsertStudyPermission(List<StudyPermission> studypermissions);
         List<Study> GetStudiesBySponsorID(int id);
         bool RemoveStudyPermission(List<StudyPermission> studypermissions);
         List<Study> GetGrantedStudiesbyUserID(int id);
         List<StudyPermission> GetStudiesByUserId(int id);
         Study GetStudyByStudyId(int studyid);
        
    }
}
