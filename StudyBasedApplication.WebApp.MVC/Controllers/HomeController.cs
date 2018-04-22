using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Business;
using System.Web.Security;

namespace StudyBasedApplication.WebApp.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        INavigationLog NavManager;

        public HomeController(INavigationLog _NavManager)
        {
            this.NavManager = _NavManager;
        }
       
        public ActionResult Index()
        {
            try
            {

                NavManager.Logger((int)Session["FOUserId"], 13, DateTime.Now, Request.Url.AbsoluteUri);
                return View();
            }
            catch (NullReferenceException e)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
            
           
        }


      

       
    }
}
