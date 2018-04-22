using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.ADO;

namespace StudyBasedApplication.Business
{
   internal class PrimaryDBGetters:IPrimaryDBGetters
    {
        public IEnumerable<Models.DataSourceStudyStatus> GetAllStatus()
        {
            try
            {
                IStatusRepository statusRepo = StatusFactory.GetInstance().GetStatusInstance();
                return statusRepo.GetStatuses();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
