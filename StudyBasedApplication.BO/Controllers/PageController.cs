using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Business;
using StudyBasedApplication.BO.Models;
using StudyBasedApplication.Models;
using System.Web.Security;

namespace StudyBasedApplication.BO.Controllers
{
    [Authorize]
    public class PageController : Controller
    {
        //
        // GET: /Page/
        IPageManager pagemanager;

        public PageController(IPageManager _pagemanager)
        {
            this.pagemanager = _pagemanager;
        }
        public ActionResult Group()
        {
            try
            {
                ViewBag.UserGroup = pagemanager.GetAllGroups((int)Session["BOGroupId"]);
                return View("PermissionEditor");
            }
            catch (NullReferenceException ex)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }

        }

        static string Gid;
        public ActionResult PermissionEditor(string GroupID)
        {
            Gid = GroupID;
            PermissionModel permissionmodel = new PermissionModel();
            permissionmodel.MyPermissionAllow = new List<PagePermissionModel>();
            permissionmodel.MyPermissionDeny = new List<PagePermissionModel>();
            try
            {
                var Pages = pagemanager.GetAllPagesForGroups(GroupID);
                foreach (Page p in Pages)
                {
                    PagePermissionModel model = new PagePermissionModel();
                    model.PageID = p.PageID;
                    model.PageName = p.PageName;
                    model.Selected = false;
                    if (pagemanager.GetPagePermissionStatus(p.PageID, Gid))
                    {

                        permissionmodel.MyPermissionDeny.Add(model);
                    }
                    else
                    {

                        permissionmodel.MyPermissionAllow.Add(model);
                    }
                }

                return PartialView("_PageList", permissionmodel);
            }
           
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
        }

        public ActionResult Allow_Deny(PermissionModel mod, string btn)
        {
            try
            {
                if (mod != null)
                {
                    if (btn == "Allow")
                    {
                        return (AllowPermission(mod));

                    }
                    else if (btn == "Deny")
                    {
                        return (DenyPagePermission(mod));

                    }

                }
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View("Error");
            }
        }


        public ActionResult DenyPagePermission(PermissionModel message)
        {
            try
            {
                foreach (PagePermissionModel pm in message.MyPermissionAllow)
                {
                    if (pm.Selected == true)
                    {
                        if (!(pagemanager.DenyPagePermission(Gid, pm.PageName)))
                        {
                            return View("Error");
                        }

                    }
                }
                return RedirectToAction("Group");
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }

        }

        public ActionResult AllowPermission(PermissionModel mod)
        {
            try
            {
                foreach (PagePermissionModel pm in mod.MyPermissionDeny)
                {
                    if (pm.Selected == true)
                    {
                        if (!(pagemanager.AllowPagePermission(Gid, pm.PageName)))
                        {
                            return View("Error");
                        }

                    }
                }
                return RedirectToAction("Group");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View("Error");
            }
        }

    }
}
