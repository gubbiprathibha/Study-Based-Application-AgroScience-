using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Data.ADO
{
    public interface IStatusRepository
    {
        IEnumerable<DataSourceStudyStatus> GetStatuses();
    }
}
