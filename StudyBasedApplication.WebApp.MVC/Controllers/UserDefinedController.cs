using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.WebApp.MVC.Models;
using StudyBasedApplication.Models;
using StudyBasedApplication.Business;

namespace StudyBasedApplication.WebApp.MVC.Controllers
{
    
   public class UserDefinedController : Controller
    {
        //
        // GET: /UserDefined/
        IUserManager usermanager;

        public UserDefinedController(IUserManager _usermanager)
        {
            this.usermanager = _usermanager;
        }


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DefinePassword(int id=0)
        {
            try
            {
                ViewBag.UserId = id;
                var user = usermanager.GetUserByID(id);
                if (user.Password == null)
                {
                    return View();


                }
                else
                {
                    ViewBag.message = "Password already defined";
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
            
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DefinePassword(PasswordModel pwd)
        {
            try
            {
                if (pwd.Password != pwd.ConfirmPassword)
                {
                    ModelState.AddModelError("confirmpassword", "password do not match");
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var user = usermanager.GetUserByID(pwd.UserId);
                        user.Password = pwd.Password;
                        usermanager.UpdateUser(user);
                        TempData["errormessage"] = "Password defined.Please Log in";
                        return RedirectToAction("Login", "Account");

                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.message = "Unable to define password";
                return View("Error");
            }

        }

        

    }

    }

