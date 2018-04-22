using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Business
{
    public interface ISponsorManager
    {
        List<Sponsor> GetAllSponsors();
        bool AssignSponsorToUser(List<string> sponsors, int userid);
        Sponsor GetSponsorById(string sponsor);
        List<Sponsor> GetAssignedSponsors(int userid);
        bool RemoveAssignedSponsors(int sponsorid, int userid);
       
    }
}
