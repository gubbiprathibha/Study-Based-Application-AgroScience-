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
using StudyBasedApplication.BO.Filters;
using StudyBasedApplication.BO.Models;
using StudyBasedApplication.Models;
using System.Data.Entity.SqlServer;
using StudyBasedApplication.Business;
using System.Data.Common;
using EmailServiceLibrary;


namespace StudyBasedApplication.BO.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
            //
            // GET: /Account/Login

        IUserManager usermanager;
        IPageManager pagemanager;
        public AccountController(IUserManager _usermanager, IPageManager _pagemanager)
        {
            this.usermanager = _usermanager;
            this.pagemanager = _pagemanager;

        }
            //Returns Login View for Back Office
            [AllowAnonymous]
            public ActionResult Login(string returnUrl)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            //
            // POST: /Account/Login
            //Passes Login model from view to controller and checks whether user is valid
            //Normal user groups are restricted from Logging in back office
            
        
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public ActionResult Login(LoginModel model, string returnUrl)
            {
                 try
                {
                    if (ModelState.IsValid)
                    {
                        var user = usermanager.ValidateUser(model);

                        if (user == null)
                        {
                            ModelState.AddModelError("", "The user name or password provided is incorrect.");

                            return View(model);
                        }
                        else
                        {
                            if (user.GroupID != 1)
                            {
                                Response.Write("Logged in successfully");
                                Session["BOUserName"] = user.FirstName;
                                Session["BOGroupId"] = user.GroupID;
                                Session["BOUserId"] = user.UserID;
                                FormsAuthentication.SetAuthCookie(model.LoginID, false);

                                return RedirectToLocal(returnUrl);
                            }
                            else
                                return View("Error");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");

                        return View(model);
                    }
                                
                                 }
                catch (Exception e)
                {
                    ViewBag.message("Invalid credentials");
                    return View("Error");

                }
                
            }
            
            //
            // POST: /Account/LogOff
            public ActionResult LogOff()
            {
                FormsAuthentication.SignOut();
                return Redirect(FormsAuthentication.LoginUrl);

            }

            //
            // GET: /Account/Register

            [Authorize]
            public ActionResult RegisterGet()
            {
                TempData["registersuccess"] = null;
                
                try
                {

                    if (pagemanager.IsPageValid((int)Session["BOGroupId"], 4))
                    {
                        return View("Error");
                    }
                    else
                    {
                        ViewBag.GroupIDValue = pagemanager.GetAllGroups(3);

                        return PartialView("_Register");
                    }
                }
                catch (NullReferenceException ex)
                {
                    return Redirect(FormsAuthentication.LoginUrl);
                }
                catch (Exception e)
                {
                    TempData["message"] = "Invaled credentials";
                    return View("Error");
                }
                
 
               
            }

            //
            // POST: /Account/Register

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public ActionResult Register(User model, string GroupIDValue)
            {
                EmailService es = new EmailService();

                try
                {
                    model.GroupID = int.Parse(GroupIDValue);
                   

                    if (ModelState.IsValid)
                    {
                       
                        int UserID = usermanager.InsertUser(model);

                        Mail mail = new Mail();
                        mail.FromAddress = "StudyBasedApplication@gmail.com";

                        mail.Subject = "Password Registration";
                        mail.ToAddress = model.EmailID;
                        mail.Cc = "lakhansrn@gmail.com";
                        string body = null;
                        if (model.GroupID == 1)
                            body = "Hi   " + model.FirstName + ",\nYour account has been created,your Login Id is " + model.LoginID + " Please define your password by clicking the below link http://192.168.30.9:97/UserDefined/DefinePassword/" + UserID + "\n\nThankYou,\nSBA";
                        else
                            body = "Hi   " + model.FirstName + ",\nYour account has been created,your Login Id is " + model.LoginID + " Please define your password by clicking the below link http://192.168.30.9:96/UserDefined/DefinePassword/" + UserID + "\n\nThankYou,\nSBA";
                        mail.Body = body;
                        es.SendEmail(mail);
                        TempData["registersuccess"] = "Registered successfully.";
                        return RedirectToAction("UsersEditor", "Home");
                    }
                   
                    return View(model);
                }
                catch(Exception ex)
                {   
                   TempData["registersuccess"] = ex.Message;
                   return RedirectToAction("UsersEditor", "Home");
                }
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

