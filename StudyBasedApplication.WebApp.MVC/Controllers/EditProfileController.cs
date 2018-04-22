using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Models;
using System.Data.Entity;
using StudyBasedApplication.Business;
using System.Web.Security;

namespace StudyBasedApplication.WebApp.MVC.Controllers
{
    [Authorize]
    public class EditProfileController : Controller
    {
        
            //
            // GET: /EditProfile/
            IUserManager usermanager;
            IPageManager pagemanager;
            INavigationLog NavManager;
            
        public EditProfileController(IUserManager _usermanager, IPageManager _pagemanager,
                INavigationLog _NavManager)
            {
                this.usermanager = _usermanager;
                this.NavManager = _NavManager;
                this.pagemanager = _pagemanager;
            }
            public ActionResult Edit()
            {
                
                try
                {
                    if (pagemanager.IsPageValid((int)Session["FOGroupId"],12))
                    {
                        return View("PageError");
                    }
                    else
                    {
                        
                        NavManager.Logger((int)Session["FOUserId"], 12, DateTime.Now, Request.Url.AbsoluteUri);
                        var Id = Session["FOUserId"];
                        ViewBag.Message = Id;
                        var user = usermanager.GetUserByID((int)Id);

                        return View(user);
                    }
                }
                catch (NullReferenceException ex)
                {
                    return Redirect(FormsAuthentication.LoginUrl);
                }
                
            }
            [HttpPost]
            public ActionResult Edit(User user)
            {
                try
                {
                    usermanager.UpdateProfile(user);

                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Unable to update user";
                    return View("Error");
                }

            }

        }

    }

