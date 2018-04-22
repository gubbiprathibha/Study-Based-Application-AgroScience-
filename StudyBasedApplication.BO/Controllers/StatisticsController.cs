using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Business;
using System.Web.Security;
using StudyBasedApplication.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace StudyBasedApplication.BO.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/
        IPageManager pagemanager ;
        INavigationLog log ;
        public StatisticsController(IPageManager _pagemanager, INavigationLog _log)
        {
            this.pagemanager = _pagemanager;
            this.log = _log;
        }

        public ActionResult GetLog()
        {
            
            try
            {
                if (pagemanager.IsPageValid((int)Session["BOGroupId"], 2))
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


        
        public ActionResult GenerateReport(DateTime d1,DateTime d2)
        {   
            try
            {
                if (d2 < d1)
                {
                    throw new Exception("Date is required");
                }
                List<LoggerExcel> list = log.Report(d1, d2);

                GridView gv = new GridView();
                gv.DataSource = list;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=NavigationLog.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter writer = new StringWriter();
                HtmlTextWriter textwriter = new HtmlTextWriter(writer);
                gv.RenderControl(textwriter);
                Response.Output.Write(writer.ToString());
                Response.Flush();
                Response.End();
                return View();
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
        }

    }
}
