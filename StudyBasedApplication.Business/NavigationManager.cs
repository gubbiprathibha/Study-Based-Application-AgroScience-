using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Models;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
    internal class NavigationManager:INavigationLog
    {

        IRepository<NavigationLog> LogRepo = GenericFactory<NavigationLog>.GetInstance().GetObject();
        public void Logger(int UserID, int PageID, DateTime Date, string BrowserInfo)
        {
            NavigationLog log = new NavigationLog();
            log.UserID = UserID;
            log.PageID = PageID;
            log.Date = Date;
            log.BrowserInfo = BrowserInfo;
            try
            {

                LogRepo.Create(log);
            }
            catch (Exception e)
            {
                throw new Exception("Not able to create log");
            }
        }

        IRepository<Page> pagerepo = GenericFactory<Page>.GetInstance().GetObject();
        IRepository<User> userrepo = GenericFactory<User>.GetInstance().GetObject();
        public List<LoggerExcel> Report(DateTime d1, DateTime d2)
        {
            try
            {
                var repo = (from r in LogRepo.All() where r.Date.Date <= d2 && d1 >= r.Date.Date select r).ToList<NavigationLog>();
                List<LoggerExcel> excel = new List<LoggerExcel>();
                foreach (var item in repo)
                {
                    LoggerExcel ex = new LoggerExcel();

                    var un = (from n in userrepo.All() where n.UserID == item.UserID select n.LoginID).FirstOrDefault();
                    ex.Username = un;
                    var pn = (from n in pagerepo.All() where n.PageID == item.PageID select n.PageName).FirstOrDefault();
                    ex.Pagename = pn;
                    ex.BrowserInfo = item.BrowserInfo;
                    ex.Date = item.Date;
                    excel.Add(ex);
                }
                return excel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
