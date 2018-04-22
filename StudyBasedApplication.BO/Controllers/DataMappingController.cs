using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyBasedApplication.Business;
using StudyBasedApplication.Models;
using StudyBasedApplication.BO.Models;
using StudyBasedApplication.Exceptions;
using System.Web.Security;

namespace StudyBasedApplication.BO.Controllers
{
    [Authorize]
    public class DataMappingController : Controller
    {
        //
        // GET: /DataMapping/

        //By: Lakhan Soren
        // EID: EITS214
        IPageManager pagemanager ;
        IGenericGetter<UserGroup> groupsGetter;
        IPrimaryDBGetters dsStatusGetter ;
        IContentMapper<DataSourceStudyStatus, LocalStudyStatus> mapManager ;
        IGenericGetter<DataSource> dataSourceGetter;
        IGenericGetter<StudyStatusMapping> statusMap;
         

        public DataMappingController(IPageManager _pagemanager, IGenericGetter<UserGroup> _groupsGetter,
            IPrimaryDBGetters _dsStatusGetter, IGenericGetter<DataSource> _dataSourceGetter,
            IContentMapper<DataSourceStudyStatus, LocalStudyStatus> _mapManager,
            IGenericGetter<StudyStatusMapping> _statusMap)
        {
            this.pagemanager = _pagemanager;
            this.groupsGetter = _groupsGetter;
            this.dsStatusGetter = _dsStatusGetter;
            this.dataSourceGetter = _dataSourceGetter;
            this.mapManager = _mapManager;
            this.statusMap = _statusMap;
        }

        public ActionResult Index()
        {
            
            try
            {
                if (pagemanager.IsPageValid((int)Session["BOGroupId"], 9))
                {
                    return View("PageError");
                }
                else
                {
                    return RedirectToAction("StudyStatusesMapper");
                }
            }
            catch (NullReferenceException ex)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

        }
        
        /// <summary>
        /// This method renders the Study Status Mapping page and displays the User groups list.
        /// </summary>
        [HttpGet]
        public ActionResult StudyStatusesMapper()
        {

            try
            {
                List<UserGroup> groups = groupsGetter.GetAll().ToList<UserGroup>().Where<UserGroup>(x => x.GroupID != 3).ToList<UserGroup>();
                List<SelectListItem> groupItemList = new List<SelectListItem>();
                foreach (var item in groups)
                {
                    SelectListItem tempItem = new SelectListItem();
                    tempItem.Text = item.GroupName;
                    tempItem.Value = item.GroupID.ToString();
                    groupItemList.Add(tempItem);

                }
                ViewBag.groups = groupItemList;


                return View();
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View("Error");
            }
        }

        /// <summary>
        /// This method is the post version of the Study Status Mapper.
        /// It submits a Status Map to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult StudyStatusesMapper(StudyStatusesModel model)
        {
            
            try
            {
                if (model.localStatus == null)
                {
                    TempData["message"] = "local study status cannot be empty";


                    return RedirectToAction("StudyStatusesMapper");

                }
                else
                {
                    mapManager.CreateMapper(new DataSourceStudyStatus() { StudyStatusName = model.dataSourceStatus }, new LocalStudyStatus() { StudyStatusName = model.localStatus }, int.Parse(model.groups), new DataSource { DataSourceName = model.datasource });
                    TempData["message"] = "Mapped successfully";
                    return RedirectToAction("StudyStatusesMapper");
                }
                }
            catch (StatusMappingException ex)
            {
                return View("Error");
            }


           

        }

        /// <summary>
        /// This method deletes a Status Map based on the mapid
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns>Deletion Confirmation</returns>
        public JsonResult DeleteMap(string mapid)
        {
            try
            {
                mapManager.DeleteMapper(int.Parse(mapid));

                return Json("Delete Sucessful");
            }
            catch (Exception e)
            {
                return Json("Delete Unsuccessfull");
            }
        }


        /// <summary>
        /// This method creates a form for creating a StatusMap
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RenderMapper(string groupID)
        {
            
            StudyStatusesModel m = new StudyStatusesModel();
            m.groups = groupID;
            
            List<StudyStatusMapping> mappedStatus;
            try
            {
                mappedStatus = statusMap.GetAll().Where<StudyStatusMapping>(x => x.GroupID == int.Parse(groupID)).ToList<StudyStatusMapping>();

            }
            catch (Exception ex)
            {
                return View("Error");
            }
            List<DataSourceStudyStatus> statuses = dsStatusGetter.GetAllStatus().ToList<DataSourceStudyStatus>();
            List<SelectListItem> statusItemList = new List<SelectListItem>();
            List<DataSourceStudyStatus> tempStatus = new List<DataSourceStudyStatus>();

            foreach (var item in statuses)
            {
                try
                {
                    if (mappedStatus.Where<StudyStatusMapping>(x => x.DataSourceStudyStatus.StudyStatusName == item.StudyStatusName).FirstOrDefault<StudyStatusMapping>() == null)
                        tempStatus.Add(item);
                    //choose the status which havnt been mapped
                }
                catch (Exception ex)
                {
                    return View("Error");
                }

            }

            statuses = tempStatus;

            foreach (var item in statuses)
            {
                SelectListItem tempItem = new SelectListItem();
                tempItem.Text = item.StudyStatusName;
                tempItem.Value = item.StudyStatusName;
                statusItemList.Add(tempItem);

            }

            if (statusItemList.Count == 0)
                ViewBag.dataSourceStatusState = "empty";
            else
                ViewBag.dataSourceStatus = statusItemList;


           
            List<DataSource> dataSources = dataSourceGetter.GetAll().ToList<DataSource>();
            List<SelectListItem> dataSourceItemList = new List<SelectListItem>();
            foreach (var item in dataSources)
            {
                SelectListItem tempItem = new SelectListItem();
                tempItem.Text = item.DataSourceName;
                tempItem.Value = item.DataSourceName;
                dataSourceItemList.Add(tempItem);

            }
            ViewBag.dataSource = dataSourceItemList;
            return PartialView(m);
        }

        /// <summary>
        /// This method lists out the already present study status Mapped
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult RenderListOfMaps(string groupID)
        {
           List<StudyStatusMapping> statusMaps = statusMap.GetAll().Where<StudyStatusMapping>(x => x.GroupID == int.Parse(groupID)).ToList<StudyStatusMapping>();
            List<MapStatusModel> mapstatuses = new List<MapStatusModel>();
            ViewData["groupID"] = groupID;
            try
            {
                foreach (var item in statusMaps)
                {
                    MapStatusModel map = new MapStatusModel();
                    map.GroupID = item.GroupID;
                    map.TDataSourceStudyStatus = item.DataSourceStudyStatus.StudyStatusName;
                    map.TLocalStudyStatus = item.LocalStudyStatus.StudyStatusName;
                    map.TDataSource = item.DataSource;
                    map.StudyStatusMappingID = item.StudyStatusMappingID;
                    mapstatuses.Add(map);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return PartialView(mapstatuses);
        }


        /// <summary>
        /// This method Updates the Status Already mapped.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult UpdateStatusMap(StudyStatusesModel model)
        {
            try
            {
                if (model.localStatus == "" || model.localStatus == null)
                {
                    return Json("Unable to update");
                }
                else
                {
                    mapManager.UpdateMapper(int.Parse(model.MapID), new LocalStudyStatus() { StudyStatusName = model.localStatus });
                    string message = "Sucessfully Updated";
                    return Json(message);
                }
            }
            catch (Exception e)
            {
                return Json("Unable to update");
            }
        }
       


    }
}
