using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Models;
using System.Data.Common;
using System.Data.SqlClient;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
   internal class PageManager:IPageManager
    {

       IRepository<PagePermission> PagePermissionRepo = GenericFactory<PagePermission>.GetInstance().GetObject();
        IRepository<Page> PageRepo = GenericFactory<Page>.GetInstance().GetObject();
        IRepository<UserGroup> GroupRepo = GenericFactory<UserGroup>.GetInstance().GetObject();
        public IEnumerable<System.Web.Mvc.SelectListItem> GetAllGroups(int groupid)
        {

            try
            {
                if (groupid == 2)
                {
                    var groups = GroupRepo.All().Where(g => g.GroupName != "Global Administrattor" && g.GroupName != "Administrator").ToList<UserGroup>();
                    var Group = from c in groups

                                select new SelectListItem
                                {
                                    Text = c.GroupName,

                                    Value = c.GroupID.ToString()

                                };
                    return Group;
                }
                else
                {
                    var groups = GroupRepo.All().Where(g => g.GroupName != "Global Administrattor").ToList<UserGroup>();
                    var Group = from c in groups

                                select new SelectListItem
                                {
                                    Text = c.GroupName,

                                    Value = c.GroupID.ToString()

                                };
                    return Group;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<System.Web.Mvc.SelectListItem> GetAllPage()
        {
            try
            {
                var Pages = PageRepo.All().ToList<Page>();
                var Page = from c in Pages

                            select new SelectListItem
                            {
                                Text = c.PageName,

                                Value = c.PageID.ToString()

                            };
                return Page;
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public bool DenyPagePermission(string groupid, string pagename)
        {
            bool flag = true;
            try
            {
                PagePermission pagepermission = new PagePermission();

                int uid =int.Parse(groupid);
                var pid = (from pg in PageRepo.All() where pg.PageName == pagename select pg.PageID).FirstOrDefault();
                var record = PagePermissionRepo.All().FirstOrDefault(model => (model.PageID == pid && model.GroupID == uid));

                if (record == null)
                {
                    pagepermission.GroupID = uid;
                    pagepermission.PageID = pid;
                    pagepermission.PermissionStatus = true;
                    PagePermissionRepo.Create(pagepermission);
                }
               
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;

            

        }

        public bool AllowPagePermission(string groupid, string pagename)
        {
            
            bool flag = true;
            try
            {
                PagePermission pagepermission = new PagePermission();

                int uid = int.Parse(groupid);
                int pid = (from pg in PageRepo.All() where pg.PageName == pagename select pg.PageID).FirstOrDefault();

                var record = PagePermissionRepo.All().FirstOrDefault(model => (model.PageID == pid && model.GroupID == uid));
                PagePermissionRepo.Delete(record);
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }


        public List<Page> GetAllPagesForGroups(string GroupID)
        {
            try
            {
                var Pages = (List<Page>)null;
                if (GroupID == "1")
                {
                    Pages = (from p in PageRepo.All() where p.PageCode == "123" select p).ToList<Page>();
                }
                else if (GroupID == "2")
                {
                    Pages = (from p in PageRepo.All() where p.PageCode == "123" || p.PageCode == "23" select p).ToList<Page>();
                }
                return Pages;
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool GetPagePermissionStatus(int pageid,string groupid)
        {
            try
            {
                int gid = int.Parse(groupid);
                var status = (PagePermissionRepo.All().Where(m => m.PageID == pageid && m.GroupID == gid).Select(m => m.PermissionStatus)).FirstOrDefault();
                return status;
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool IsPageValid(int groupid, int pageid)
        {
            try
            {
                var flag = (from ps in PagePermissionRepo.All()
                            where ps.GroupID == groupid && ps.PageID == pageid
                            select ps.PermissionStatus).FirstOrDefault();
                return flag;
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
