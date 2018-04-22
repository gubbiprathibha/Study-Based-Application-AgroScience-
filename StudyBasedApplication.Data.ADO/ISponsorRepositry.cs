using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Data.ADO
{
    public interface ISponsorRepositry
    {
        List<Sponsor> GetAllSponsors();
       Sponsor GetSponsorBySponsorID(int SponsorID);
       string GetDataSource();
    }
}
