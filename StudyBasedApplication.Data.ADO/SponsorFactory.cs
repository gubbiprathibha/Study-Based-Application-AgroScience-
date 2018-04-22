using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyBasedApplication.Data.ADO
{
    public class SponsorFactory
    {
        private static SponsorFactory sponsorFactoryInstance = new SponsorFactory();
        private SponsorFactory()
        {
        }
        public static SponsorFactory GetInstance()
        {
            return sponsorFactoryInstance;
        }
        public ISponsorRepositry GetSponsorInstance()
        {
            return new SponsorRepositry();
        }
    }
}
