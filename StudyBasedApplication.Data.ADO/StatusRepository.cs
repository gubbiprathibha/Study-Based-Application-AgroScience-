using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.Data.ADO
{
   internal class StatusRepository:IStatusRepository
    {
        private SqlConnection conn = null;
        ISponsorRepositry sponsorrepo = SponsorFactory.GetInstance().GetSponsorInstance();
        private SqlConnection GetConnection()
        {

            conn = new SqlConnection();
            try
            {
                conn.ConnectionString = sponsorrepo.GetDataSource();
                return conn;
            }
            catch (SqlException e)
            {
                throw new Exception("Invalid datasource");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<Models.DataSourceStudyStatus> GetStatuses()
        {
            List<DataSourceStudyStatus> dsStatuses = new List<DataSourceStudyStatus>();

            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql = "Select statusName from Statuses";
                SqlCommand cmd = new SqlCommand(selectSql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DataSourceStudyStatus status = new DataSourceStudyStatus();
                    status.StudyStatusName = reader.GetString(0);
                    dsStatuses.Add(status);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return dsStatuses;
        }
    }
}
