using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Business
{
    public interface INavigationLog
    {
        void Logger(int UserID, int PageID, DateTime Date, string BrowserInfo);
        List<LoggerExcel> Report(DateTime d1, DateTime d2);
    }
}
