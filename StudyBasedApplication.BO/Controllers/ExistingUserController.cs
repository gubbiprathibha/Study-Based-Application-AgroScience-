using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.BO.Models;
using StudyBasedApplication.Models;
using StudyBasedApplication.Business;
using System.Web.Security;

namespace StudyBasedApplication.BO.Controllers
{
    [Authorize]
    public class ExistingUserController : Controller
    {
        //
        // GET: /ExistingUser/
        IUserManager usermanager ;
         
        ISponsorManager sponsormanager;
        IStudyManager studymanager;
        IPageManager pagemanager;
        public ExistingUserController(IUserManager _usermanager, ISponsorManager _sponsormanager,
            IStudyManager _studymanager, IPageManager _pagemanager)
        {
            this.usermanager = _usermanager;
            this.studymanager = _studymanager;
            this.sponsormanager = _sponsormanager;
            this.pagemanager = _pagemanager;
        }

        public ActionResult ViewExistingUser()
        {
            
            try
            {
                if (pagemanager.IsPageValid((int)Session["BOGroupId"], 5))
                {
                    return View("PageError");
                }
                else
                {
                    List<UserProfileModel> searchuserprofiles = new List<UserProfileModel>();

                    var users = usermanager.GetAllUsers();
                    foreach (StudyBasedApplication.Models.User u in users)
                    {
                        UserProfileModel SUProfile = new UserProfileModel();
                        SUProfile.User = new User();
                        SUProfile.User = u;
                        SUProfile.Sponsors = new List<Sponsor>();
                        SUProfile.Sponsors = sponsormanager.GetAssignedSponsors(u.UserID);
                        searchuserprofiles.Add(SUProfile);

                    }
                    return PartialView("_ViewExistingUser", searchuserprofiles);
                }
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
        

    }
}
