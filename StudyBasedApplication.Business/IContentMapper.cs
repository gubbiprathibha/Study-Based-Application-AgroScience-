using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Business
{
    public interface IContentMapper<TObjectOld, TObjectNew>
    {
        void CreateMapper(TObjectOld old, TObjectNew New, int groupID, DataSource ds);
        void UpdateMapper(int Mapid, TObjectNew New);
        void DeleteMapper(int Mapid);
        List<StudyStatusMapping> GetAllStatusMaps();
    }
}
