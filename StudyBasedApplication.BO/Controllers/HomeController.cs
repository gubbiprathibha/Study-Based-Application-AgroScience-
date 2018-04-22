using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Models;
using StudyBasedApplication.Business;
using StudyBasedApplication.BO.Models;
using System.Web.Security;

namespace StudyBasedApplication.BO.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        IPageManager pagemanager;

        public HomeController(IPageManager _pagemanager)
        {
            this.pagemanager = _pagemanager;
        }

        public ActionResult Index()
        {
         
                return View();
           
        }

        
       

        public ActionResult UsersEditor()
        {
           
            try
            {
                if (pagemanager.IsPageValid((int)Session["BOGroupId"], 3))
                {
                    return View("PageError");
                }
                else
                {

                    return View();
                }
            }
            catch (NullReferenceException ex)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
        }

       
    }
}
