using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Business
{
    public interface IPrimaryDBGetters
    {
        IEnumerable<DataSourceStudyStatus> GetAllStatus();

    }
}
