using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.WebApp.MVC.Models;
using StudyBasedApplication.Models;
using StudyBasedApplication.Business;
using System.Web.Security;

namespace StudyBasedApplication.WebApp.MVC.Controllers
{
    [Authorize]
    public class StudyController : Controller
    {
        //
        // GET: /Study/
        
        IUserManager usermanager;
        IStudyManager studymanager;
        IPageManager pagemanager;
        INavigationLog NavManager ;
        IGenericGetter<User> usergetter;
        IGenericGetter<StudyStatusMapping> mapGetter;
                  

        public StudyController(IUserManager _usermanager, IStudyManager _studymanager,
            IPageManager _pagemanager, INavigationLog _NavManager, IGenericGetter<User> _usergetter,
            IGenericGetter<StudyStatusMapping> _mapGetter )
        {
            this.usermanager = _usermanager;
            this.studymanager = _studymanager;
            this.pagemanager = _pagemanager;
            this.NavManager = _NavManager;
            this.usergetter = _usergetter;
            this.mapGetter = _mapGetter;
        }

        public ActionResult GetStudy(string SearchText)
        {
            
            try
            {
                if (pagemanager.IsPageValid((int)Session["FOGroupId"], 11))
                {
                    return View("PageError");
                }
                else
                {
                    
                    NavManager.Logger((int)Session["FOUserId"], 11, DateTime.Now, Request.Url.AbsoluteUri);
                    int id = (int)(Session["FOUserId"]);
                    var user = usermanager.GetUserByID(id);
                    
                    var studies = studymanager.GetGrantedStudiesbyUserID(id);
                    GrantDenyStudyModels grant = new GrantDenyStudyModels();

                    foreach (var item in studies)
                    {
                        StudyModels std = new StudyModels();
                        Study sm = new Study();
                        var presentgroup = user.GroupID;
                        List<StudyStatusMapping> studyStatusMaps = mapGetter.GetAll().ToList<StudyStatusMapping>();

                        sm = studymanager.GetStudyByStudyId(item.StudyID);
                        var newStatus = (from status in studyStatusMaps
                                         where (status.DataSourceStudyStatus.StudyStatusName == sm.StudyStatus && status.GroupID == presentgroup)
                                         select status.LocalStudyStatus.StudyStatusName).FirstOrDefault<string>();
                        if (newStatus != null)
                        {

                            std.StudyName = sm.StudyName;
                            std.StudyCode = sm.StudyCode;
                            std.StudyID = sm.StudyID;
                            std.SponsorName = sm.Sponsor.SponsorName;
                            std.StudyStartDate = sm.StudyStartDate;
                            std.Status = newStatus;
                            grant.studymodel.Add(std);
                        }
                    }

                    if (SearchText != null && SearchText != "")
                    {
                        List<StudyModels> searchStudyList = grant.studymodel.Where(x => x.StudyName.ToLower().Contains(SearchText.ToLower())).ToList<StudyModels>();
                        GrantDenyStudyModels grantDenyStudyModels = new GrantDenyStudyModels();
                        grantDenyStudyModels.studymodel = searchStudyList;
                        return View(grantDenyStudyModels);


                    }
                    return View(grant);
                }
            }
                catch(NullReferenceException ex)
            {
                    return Redirect(FormsAuthentication.LoginUrl);

                }
            catch(Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
            }
            

            

        public ActionResult StudyDetails(int StudyID)
        {
            
            try
            {
                if (pagemanager.IsPageValid((int)Session["FOGroupId"], 15))
                {
                    return View("PageError");
                }
                else
                {
                    
                    NavManager.Logger((int)Session["FOUserId"], 15, DateTime.Now, Request.Url.AbsoluteUri);
                    Study study = studymanager.GetStudyByStudyId(StudyID);
                    ViewBag.Activities = study.GetActivities();
                    ViewBag.Study = study;
                    return View(study);
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
        }


        }
    }



