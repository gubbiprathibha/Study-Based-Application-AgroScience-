using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Business;
using StudyBasedApplication.Models;
using StudyBasedApplication.BO.Models;
using System.Web.Security;

namespace StudyBasedApplication.BO.Controllers
{
    [Authorize]
    public class SponsorAccessManagementController : Controller
    {
        //
        // GET: /SponsorAccessManagement/

        ISponsorManager sponsorManager ;
        IStudyManager studyManager ;
        static int id1 = 0;
        IPageManager pagemanager;
        public SponsorAccessManagementController(ISponsorManager _sponsorManager,
            IStudyManager _studyManager, IPageManager _pagemanager)
        {
            this.sponsorManager = _sponsorManager;
            this.studyManager = _studyManager;
            this.pagemanager = _pagemanager;
        }
        public ActionResult IndexGet()
        {
           
            try
            {
                if (pagemanager.IsPageValid((int)Session["BOGroupId"], 6))
                {
                    return View("PageError");
                }
                else
                {
                    id1 = (int)Session["AccessUserId"];
                    ViewBag.id = (int)Session["AccessUserId"]; ;
                    var Sponsors = sponsorManager.GetAllSponsors();
                    var assignedsponsors = sponsorManager.GetAssignedSponsors(id1);
                    var sponsorlist = new List<Sponsor>();
                    bool flag = false;
                    foreach (Sponsor s in Sponsors)
                    {
                        foreach (Sponsor p in assignedsponsors)
                        {
                            if (p.SponsorID == s.SponsorID)
                            {
                                flag = true;
                                break;
                            }

                        }

                        if (flag == false)
                        {
                            sponsorlist.Add(s);

                        }
                        flag = false;

                    }

                    ViewBag.Sponsors = from s in sponsorlist
                                       select new SelectListItem
                                       {
                                           Text = s.SponsorName,
                                           Value = s.SponsorID.ToString()

                                       };


                    return PartialView("Index");
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
        [HttpPost]
        public ActionResult Index(List<string> Sponsors)
        {

            SponsorModel model = new SponsorModel();
            model.id = id1;
            model.SponsorPermissions = new List<SponsorPermissionModel>();
            List<Sponsor> assignedsponsors = new List<Sponsor>();
            try
            {
                if (Sponsors.Count != 0)
                {
                    sponsorManager.AssignSponsorToUser(Sponsors, id1);

                    assignedsponsors = sponsorManager.GetAssignedSponsors(id1);


                    foreach (Sponsor s1 in assignedsponsors)
                    {
                        SponsorPermissionModel spm = new SponsorPermissionModel();
                        spm.SponsorID = s1.SponsorID;
                        spm.SponsorName = s1.SponsorName;
                        spm.ShowAllStudies = studyManager.CheckStudyPermission(id1, s1.SponsorID);

                        model.SponsorPermissions.Add(spm);
                    }
                }

                else
                {
                    assignedsponsors = sponsorManager.GetAssignedSponsors(id1);
                    foreach (Sponsor s1 in assignedsponsors)
                    {
                        SponsorPermissionModel spm = new SponsorPermissionModel();
                        spm.SponsorID = s1.SponsorID;
                        spm.SponsorName = s1.SponsorName;
                        spm.ShowAllStudies = studyManager.CheckStudyPermission(id1, s1.SponsorID);
                        model.SponsorPermissions.Add(spm);
                    }


                }


                return PartialView("_sponsors", model);
            }
            catch (NullReferenceException ex)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View("Error");
            }


        }


    }
}
