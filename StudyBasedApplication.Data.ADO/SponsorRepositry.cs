using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using StudyBasedApplication.Models;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Data.ADO
{
   internal class SponsorRepositry:ISponsorRepositry
    {
        private SqlConnection conn = null;
        private SqlConnection GetConnection()
        {
            conn = new SqlConnection();
            try
            {
                conn.ConnectionString = GetDataSource();
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
        public List<Sponsor> GetAllSponsors()
        {

            List<Sponsor> Sponsors = new List<Sponsor>();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql = "Select * from Sponsors";
                SqlCommand cmd = new SqlCommand(selectSql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Sponsor s = new Sponsor();
                    s.SponsorID = (int)reader[0];
                    s.SponsorCode = (string)reader[1];
                    s.SponsorName = (string)reader[2];
                    Sponsors.Add(s);
                }
              
                return Sponsors;
            }
            catch (SqlException e)
            {
                throw new Exception("No sponsors present");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public Sponsor GetSponsorBySponsorID(int SponsorID)
        {
            Sponsor Sponsor = new Sponsor();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql = "Select * from Sponsors where sponsorId=@SponsorID";
                SqlCommand cmd = new SqlCommand(selectSql, conn);
                SqlParameter p1 = new SqlParameter("@SponsorID", SponsorID);
                cmd.Parameters.Add(p1);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Sponsor.SponsorID = (int)reader[0];
                    Sponsor.SponsorCode = (string)reader[1];
                    Sponsor.SponsorName = (string)reader[2];

                }
                return Sponsor;
            }
            catch (SqlException e)
            {
                throw new Exception("No sponsors present");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }
        
        IRepository<DataSource> datasourcerepo = GenericFactory<DataSource>.GetInstance().GetObject();

        public string GetDataSource()
        {
          

            try
            {
                
                DataSource ds = datasourcerepo.Find(model => model.DataSourceName.ToUpper() == "ESM");
                return ds.ConnectionString;
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

    }
}
