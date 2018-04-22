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
    public class StudyAccessManagementController : Controller
    {
        //
        // GET: /AccessManagement/


       
        ISponsorManager sponsormanager ;
        IStudyManager studymanager;
        GrantDenyStudyModel GrantDenyStudyModel = new GrantDenyStudyModel();
        IUserManager usermanager ;
        IPageManager pagemanager ;
        public StudyAccessManagementController(ISponsorManager _sponsormanager, IStudyManager _studymanager,
            IUserManager _usermanager, IPageManager _pagemanager)
        {
            this.sponsormanager = _sponsormanager;
            this.studymanager = _studymanager;
            this.usermanager = _usermanager;
            this.pagemanager = _pagemanager;
        }

        public ActionResult Sponsor_StudyManagement(int UserId)
        {
            
                Session["AccessUserId"] = UserId;
                return View();
            
        }

        public ActionResult GetStudy()
        {
            
            try
            {
                if (pagemanager.IsPageValid((int)Session["BOGroupId"], 7))
                {
                    return View("PageError");
                }
                else
                {
                    int UserID = (int)Session["AccessUserId"];
                    var user = usermanager.GetUserByID(UserID);

                    var studyperm = studymanager.GetStudiesByUserId(UserID);
                    GrantDenyStudyModel.studymodelDenied = new List<StudyModel>();
                    GrantDenyStudyModel.studymodelGranted = new List<StudyModel>();
                    foreach (var item in studyperm)
                    {

                        StudyModel studymodel = new StudyModel();
                        Study std = new Study();
                        std = studymanager.GetStudyByStudyId(item.StudyID);
                        studymodel.StudyID = std.StudyID;
                        studymodel.StudyName = std.StudyName;
                        studymodel.StudyCode = std.StudyCode;
                        Sponsor spnr = new Sponsor();
                        spnr = sponsormanager.GetSponsorById(item.SponsorID.ToString());
                        studymodel.SponsorName = spnr.SponsorName;
                        if (item.Status == true)
                        {
                            studymodel.Status = item.Status;
                            studymodel.Selected = false;
                            GrantDenyStudyModel.studymodelGranted.Add(studymodel);
                        }
                        if (item.Status == false)
                        {
                            studymodel.Status = item.Status;
                            studymodel.Selected = false;
                            GrantDenyStudyModel.studymodelDenied.Add(studymodel);
                        }

                    }

                    return PartialView("_StudyList", GrantDenyStudyModel);
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

        


        public ActionResult Allow_DenyStudy(GrantDenyStudyModel model, string btn)
        {
            if (model != null)
            {
                if (btn == "Grant")
                {
                    return(AllowPermission(model));
                  
                }
                else if (btn == "Deny")
                {
                   return(DenyPagePermission(model));
                    
                }
               
            }
            return View("Error");
          }

       
        public ActionResult AllowPermission(GrantDenyStudyModel mod)
        {
            try
            {
                int UserID = (int)Session["AccessUserId"];
                foreach (StudyModel gs in mod.studymodelDenied)
                {
                    if (gs.Selected == true)
                    {
                        if (!(studymanager.AllowStudyPermission(gs.StudyID, UserID, gs.SponsorName)))
                        {
                            return View("Error");
                        }

                    }
                }

                return View("Sponsor_StudyManagement");
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

        public ActionResult DenyPagePermission(GrantDenyStudyModel model)
        {
            try
            {
                int UserID = (int)Session["AccessUserId"];
                foreach (StudyModel ds in model.studymodelGranted)
                {
                    if (ds.Selected == true)
                    {
                        if (!(studymanager.DenyStudyPermission(ds.StudyID, UserID, ds.SponsorName)))
                        {
                            return View("Error");
                        }

                    }
                }

                return View("Sponsor_StudyManagement");
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
        public ActionResult AssignAllStudiesToUser(SponsorModel Model)
        {

            List<StudyPermission> removestudypermissions = new List<StudyPermission>();
            try
            {
                foreach (SponsorPermissionModel s in Model.SponsorPermissions)
                {
                    if (s.Remove == false)
                    {

                        List<Study> studies = new List<Study>();
                        List<StudyPermission> studypermissions = new List<StudyPermission>();
                        studies = studymanager.GetStudiesBySponsorID(s.SponsorID);

                        foreach (Study s1 in studies)
                        {
                            StudyPermission sp = new StudyPermission();
                            sp.UserID = Model.id;
                            sp.SponsorID = s.SponsorID;
                            sp.Status = s.ShowAllStudies;
                            sp.StudyID = s1.StudyID;
                            studypermissions.Add(sp);




                        }

                        if (studymanager.InsertStudyPermission(studypermissions))
                        {
                            ViewBag.message = "Changes saved";
                        }
                    }
                    else
                    {
                        sponsormanager.RemoveAssignedSponsors(s.SponsorID, Model.id);

                        StudyPermission sp = new StudyPermission();
                        sp.UserID = Model.id;
                        sp.SponsorID = s.SponsorID;
                        removestudypermissions.Add(sp);
                    }





                }
                if (removestudypermissions.Count != 0)
                {

                    if (studymanager.RemoveStudyPermission(removestudypermissions))
                    {
                        ViewBag.message = "removed";
                    }
                }
                return View("Sponsor_StudyManagement");
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
        }

    }
}
