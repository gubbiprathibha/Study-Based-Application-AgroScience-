using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.ADO;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Models;
using System.Data.Common;
using System.Data.SqlClient;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
   internal class SponsorManager:ISponsorManager
    {
       ISponsorRepositry sponsorrepo = SponsorFactory.GetInstance().GetSponsorInstance();

        public List<Models.Sponsor> GetAllSponsors()
        {
            try
            {
                return sponsorrepo.GetAllSponsors();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        IRepository<UserSponsor> usersponsorrepo = GenericFactory<UserSponsor>.GetInstance().GetObject();


        IStudyRepositry repo = StudyFactory.GetInstance().GetSponsorInstance();
        public bool AssignSponsorToUser(List<string> sponsors, int userid)
        {
            bool isValid = true;
            bool isSuccessful = true;
            //Check whether sponsor is already assigned to the user
            try
            {
                foreach (string s in sponsors)
                {
                    int si = int.Parse(s);
                    if (usersponsorrepo.Find(model => (model.SponsorID == si && model.UserID == userid)) != null)

                        isValid = false;
                    if (isValid)
                    {
                        UserSponsor us = new UserSponsor();

                        us.UserID = userid;
                        us.SponsorID = si;
                        usersponsorrepo.Create(us);
                    }
                    else
                        isSuccessful = false;
                }
            }
            catch (Exception e)
            {
                isSuccessful = false;
                throw e;
            }
            return isSuccessful;

        }
        public Sponsor GetSponsorById(string sponsor)
        {
            try
            {
                return sponsorrepo.GetSponsorBySponsorID(int.Parse(sponsor));
            }
            catch (Exception e)
            {
                throw e;
            }
            }
        public List<Sponsor> GetAssignedSponsors(int userid)
        {
            List<UserSponsor> us = new List<UserSponsor>();
            try
            {
                us = usersponsorrepo.Get(model => model.UserID == userid).ToList<UserSponsor>();
                List<int> sponsorids = new List<int>();
                sponsorids = (from c in us
                              select c.SponsorID).ToList();
                List<Sponsor> sponsors = new List<Sponsor>();
                foreach (int i in sponsorids)
                {
                    sponsors.Add(sponsorrepo.GetSponsorBySponsorID(i));
                }
                return sponsors;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool RemoveAssignedSponsors(int sponsorid, int userid)
        {
            bool issuccessful = true;
            try
            {
                var usersponsor = usersponsorrepo.Find(model => model.UserID == userid && model.SponsorID == sponsorid);
                if (usersponsor != null)
                {
                    usersponsorrepo.Delete(usersponsor.UserSponsorID);
                }
            }
            catch (Exception e)
            {
                issuccessful = false;
                throw e;
            }
            return issuccessful;
        }
    }
}
