using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.ADO;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Models;
using System.Data.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
   internal class StudyManager:IStudyManager
    {
       IRepository<StudyPermission> studyPermissionRepo = GenericFactory<StudyPermission>.GetInstance().GetObject();

       IStudyRepositry studyrepo = StudyFactory.GetInstance().GetSponsorInstance();

        public List<Models.Study> GetStudiesBySponsorID(int id)
        {
            try
            {
                return studyrepo.GetStudiesBySponsorID(id);
            }

            catch (Exception e)
            {
                throw e;
            }
        }
        public bool InsertStudyPermission(List<StudyPermission> studypermissions)
        {
            bool isSuccessfull = true;
            try
            {
                foreach (StudyPermission p in studypermissions)
                {
                    var count = studyPermissionRepo.Find(model => (model.UserID == p.UserID && model.SponsorID == p.SponsorID && model.StudyID == p.StudyID));
                    if (count == null)
                    {
                        studyPermissionRepo.Create(p);
                    }
                    else
                    {
                        if (count.Status != p.Status)
                        {
                            studyPermissionRepo.Delete(count.StudyPermissionID);
                            studyPermissionRepo.Create(p);
                            
                        }
                    }

                }
            }
            catch (SqlException e)
            {
                isSuccessfull = false;
                throw e;
            }
            catch (Exception e1)
            {
                isSuccessfull = false;
                throw e1;
               
            }
            return isSuccessfull;
        }
        public bool RemoveStudyPermission(List<StudyPermission> studypermissions)
        {
            bool issuccessful = true;

            try
            {

                foreach (StudyPermission p in studypermissions)
                {
                    var count = studyPermissionRepo.Find(model => (model.UserID == p.UserID && model.SponsorID == p.SponsorID));
                    if (count != null)
                    {
                        studyPermissionRepo.Delete(count.StudyPermissionID);
                    }

                }
            }
            catch (Exception e)
            {
                issuccessful = false;
                throw e;
            }
            return issuccessful;
        }



        ISponsorRepositry sponsorrepo = SponsorFactory.GetInstance().GetSponsorInstance();

        public bool AllowStudyPermission(int studyid, int userid, string sponsorname)
        {
            bool flag = true;
            try
            {
                var sponsorid = (from s in sponsorrepo.GetAllSponsors() where s.SponsorName == sponsorname select s.SponsorID).FirstOrDefault();
                var sp = (from dd in studyPermissionRepo.All() where (dd.StudyID == studyid && dd.SponsorID == sponsorid && dd.UserID == userid) select dd).FirstOrDefault();
                sp.Status = true;
                studyPermissionRepo.Update(sp);
                //db.Entry(sp).State = EntityState.Modified;
                //db.SaveChanges();
            }
            catch (SqlException e)
            {
                throw e;
            }
            return flag;
        }

        public bool DenyStudyPermission(int studyid, int userid, string sponsorname)
        {
           
            bool flag = true;
            try
            {
                var sponsorid = (from s in sponsorrepo.GetAllSponsors() where s.SponsorName == sponsorname select s.SponsorID).FirstOrDefault();
                var sp = (from dd in studyPermissionRepo.All() where (dd.StudyID == studyid && dd.SponsorID == sponsorid && dd.UserID == userid) select dd).FirstOrDefault();
                sp.Status = false;
                studyPermissionRepo.Update(sp);
                //db.Entry(sp).State = EntityState.Modified;
                //db.SaveChanges();

            }
            catch (SqlException e)
            {
                throw e;
            }
            return flag;
        }
        public bool CheckStudyPermission(int userid, int sponsorid)
        {
            try
            {
                var count = studyPermissionRepo.Get(model => model.UserID == userid && model.SponsorID == sponsorid);
                if (count != null)
                {

                    foreach (StudyPermission s in count)
                    {
                        if (s.Status == true)
                            return true;

                    }
                    return false;
                }
                else
                    return false;


            }
            catch (SqlException e)
            {
                throw e;
            }
        }


        public List<Study> GetGrantedStudiesbyUserID(int id)
        {

            try
            {
                
                List<Study> studies = new List<Study>();
                var studypermissions = studyPermissionRepo.Get(model => model.UserID == id && model.Status == true);
                foreach (StudyPermission s in studypermissions)
                {
                    Study study = new Study();

                    study = studyrepo.GetStudyByStudyID(s.StudyID);

                    studies.Add(study);

                }
                return studies;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<StudyPermission> GetStudiesByUserId(int id)
        {
            try
            {
                return studyPermissionRepo.Get(model => model.UserID == id).ToList<StudyPermission>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Study GetStudyByStudyId(int studyid)
        {
            try
            {
                return studyrepo.GetStudyByStudyID(studyid);
            }
            catch (Exception e)
            {
                throw e;
            }
            }
    }
}
