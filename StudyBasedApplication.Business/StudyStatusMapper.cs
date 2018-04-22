using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Exceptions;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
    internal class StudyStatusMapper : IContentMapper<DataSourceStudyStatus, LocalStudyStatus>
    {
        IRepository<StudyStatusMapping> maprepo = GenericFactory<StudyStatusMapping>.GetInstance().GetObject();

        public void CreateMapper(DataSourceStudyStatus old, LocalStudyStatus New, int groupid, DataSource dataSource)
        {
            try
            {
                StudyStatusMapping statusMap = new StudyStatusMapping();
                if (true)
                {
                    statusMap.DataSourceStudyStatus = old;
                    statusMap.LocalStudyStatus = New;
                    statusMap.GroupID = groupid;
                    statusMap.DataSource = dataSource.DataSourceName;
                    maprepo.Create(statusMap);
                }

            }
            catch (Exception e)
            {
                throw new Exception("Status already mapped");
            }
        }

        public void UpdateMapper(int Mapid, LocalStudyStatus New)
        {
            try
            {
                StudyStatusMapping statusMap = maprepo.Find(Mapid);
                statusMap.LocalStudyStatus = New;
                maprepo.Update(statusMap);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public void DeleteMapper(int Mapid)
        {

            try
            {
                StudyStatusMapping statusMap = maprepo.Find(Mapid);
                if (statusMap != null)
                {
                    maprepo.Delete(statusMap);
                }
                else
                    throw new StatusMappingException("The Status Map does not exist");
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public List<StudyStatusMapping> GetAllStatusMaps()
        {
            try
            {
                return maprepo.All().ToList<StudyStatusMapping>();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

    }
}
