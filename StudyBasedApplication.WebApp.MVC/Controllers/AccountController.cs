using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using StudyBasedApplication.WebApp.MVC.Models;
using StudyBasedApplication.Models;
using System.Data.Entity.SqlServer;
using StudyBasedApplication.Business;

namespace StudyBasedApplication.WebApp.MVC.Controllers
{
    
   [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

       IUserManager usermanager;

       public AccountController(IUserManager _usermanager)
       {
           this.usermanager = _usermanager;
       }

        [AllowAnonymous]
        
        public ActionResult Login(string returnUrl)
        {
            
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel user, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user1 = usermanager.ValidateUser(user);
                    if (user1 != null)
                    {

                        Response.Write("Logged in successfully");
                        Session["FOUserName"] = user1.FirstName;
                        Session["FOGroupId"] = user1.GroupID;
                        Session["FOUserId"] = user1.UserID;
                        FormsAuthentication.SetAuthCookie(user1.LoginID, false);
                        return RedirectToLocal(returnUrl);

                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return View(user);

                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return View(user);

                }
               
            }
            catch (Exception e)
            {
                ViewBag.message = "Invalid credentials";
                return View("Error");
            }
        }

        // POST: /Account/LogOff

        [HttpGet]
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);

        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
