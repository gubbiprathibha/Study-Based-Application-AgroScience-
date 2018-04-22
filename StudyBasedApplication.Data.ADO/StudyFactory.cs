using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyBasedApplication.Data.ADO
{
   public class StudyFactory
    {
           private static StudyFactory studyFactoryInstance = new StudyFactory();
           private StudyFactory()
           {
           }
           public static StudyFactory GetInstance()
           {
               return studyFactoryInstance;
           }
           public IStudyRepositry GetSponsorInstance()
           {
               return new StudyRepositry();
           }
       
    }
}
