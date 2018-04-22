using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Business
{
   public interface IPageManager
    {
       IEnumerable<System.Web.Mvc.SelectListItem> GetAllGroups(int groupid);
       IEnumerable<System.Web.Mvc.SelectListItem> GetAllPage();
       bool DenyPagePermission(string groupid, string pagename);
       bool AllowPagePermission(string groupid, string pagename);
       List<Page> GetAllPagesForGroups(string GroupID);
       bool GetPagePermissionStatus(int pageid,string groupid);
       bool IsPageValid(int groupid, int pageid);
    }
}
