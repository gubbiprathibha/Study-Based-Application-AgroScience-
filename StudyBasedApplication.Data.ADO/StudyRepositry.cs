using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Models;
using System.Data.SqlClient;

namespace StudyBasedApplication.Data.ADO
{
   internal class StudyRepositry:IStudyRepositry
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
                throw new Exception("DataSource not present");
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



        

        public List<Study> GetStudiesBySponsorID(int MasterSponsor)
        {
            List<Study> Studies = new List<Study>();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql1 = "Select * from Studies where masterSponsor=@MasterSponsor";
                SqlCommand cmd1 = new SqlCommand(selectSql1, conn);
                SqlParameter p1 = new SqlParameter("@MasterSponsor", MasterSponsor);
                cmd1.Parameters.Add(p1);
                SqlDataReader reader = cmd1.ExecuteReader();

                while (reader.Read())
                {
                    Study s = new Study();
                    s.StudyID = (int)reader[0];
                    s.StudyCode = (string)reader[1];
                    s.StudyName = (string)reader[2];
                    //s.StudyStartDate = Convert.ToDateTime(reader["studyStartDate"]);
                    //s.StudyEndDate = reader.GetDateTime(4);

                    Studies.Add(s);
                }
                conn.Close();

                return Studies;
            }
            catch (SqlException e)
            {
                throw new Exception("No studies present");
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


        public List<StudyCategory> GetAllCategories()
        {
            List<StudyCategory> StudyCategories = new List<StudyCategory>();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql = "Select * from StudyCategories";
                SqlCommand cmd = new SqlCommand(selectSql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudyCategory cat = new StudyCategory();
                    cat.StudyCategoryID = (int)reader[0];
                    cat.StudyCategoryCode = (string)reader[1];
                    cat.StudyCategoryName = (string)reader[2];
                    StudyCategories.Add(cat);
                }
                conn.Close();
                return StudyCategories;
            }
            catch (SqlException e)
            {
                throw new Exception("No categories present");
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

        public List<Study> GetAllStudies()
        {
            List<Study> Studies = new List<Study>();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql = @"select studyId,studyCode,studyName,studyStartDate,studyTypeName,studyTypeCode,
                                         studyCategoryName,studyCategoryCode,statusName 
                                         from Studies,StudyTypes,StudyCategories,Statuses
                                          where Studies.studyTypeId=StudyTypes.studyTypeId
                                          and Studies.studyCategoryId=StudyCategories.studyCategoryId
                                         and Studies.studyStatusId=statuses.statusId";
                SqlCommand cmd = new SqlCommand(selectSql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Study s = new Study();
                    s.StudyID = (int)reader[0];
                    s.StudyCode = (string)reader[1];
                    s.StudyName = (string)reader[2];
                    s.StudyStartDate = Convert.ToDateTime(reader["studyStartDate"]);
                    s.StudyType = new StudyType();
                    s.StudyType.StudyTypeName = (string)reader[4];
                    s.StudyType.StudyTypeCode = (string)reader[5];
                    s.StudyCategory = new StudyCategory();
                    s.StudyCategory.StudyCategoryName = (string)reader[6];
                    s.StudyCategory.StudyCategoryCode = (string)reader[7];

                    s.StudyStatus = (string)reader[8];


                    List<Activity> activities = new List<Activity>();
                    activities = GetActivities(s.StudyID);
                    foreach (Activity a in activities)
                    {
                        s.SetActivity(a);
                    }

                    Sponsor sponsor = GetSpoonsor(s.StudyID);
                    s.Sponsor = sponsor;

                    Studies.Add(s);

                }

                conn.Close();
                return Studies;
            }
            catch (SqlException e)
            {
                throw new Exception("No studies present");
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



        public List<Activity> GetActivities(int StudyID)
        {
            {
                List<Activity> Activities = new List<Activity>();
                try
                {
                    conn = GetConnection();
                    conn.Open();
                    string selectSql1 = "Select * from Activities where studyId=@StudyID ";
                    SqlCommand cmd1 = new SqlCommand(selectSql1, conn);
                    SqlParameter p1 = new SqlParameter("@StudyID", StudyID);
                    cmd1.Parameters.Add(p1);
                    SqlDataReader reader = cmd1.ExecuteReader();

                    while (reader.Read())
                    {
                        Activity act = new Activity();
                        act.ActivityID = (int)reader[0];
                        act.ActivityName = (string)reader[2];
                        act.ActivityStatus = (string)reader["activityStatus"];

                        Activities.Add(act);
                    }
                    conn.Close();

                    return Activities;
                }
                catch (SqlException e)
                {
                    throw new Exception("No activities present for this study");
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

        }


        public Sponsor GetSpoonsor(int StudyID)
        {
            Sponsor sponsor = new Sponsor();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql1 = "Select * from Sponsors where sponsorId=(select masterSponsor from Studies where studyId=@StudyID)";
                SqlCommand cmd1 = new SqlCommand(selectSql1, conn);
                SqlParameter p1 = new SqlParameter("@StudyID", StudyID);
                cmd1.Parameters.Add(p1);
                SqlDataReader reader = cmd1.ExecuteReader();

                while (reader.Read())
                {
                    sponsor.SponsorID = (int)reader[0];
                    sponsor.SponsorCode = (string)reader[1];
                    sponsor.SponsorName = (string)reader[2];
                }
             

                return sponsor;
            }
            catch (SqlException e)
            {
                throw new Exception("sponsors not available");
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


        public Study GetStudyByStudyID(int StudyID)
        {
            Study s = new Study();
            try
            {
                conn = GetConnection();
                conn.Open();
                string selectSql1 = @" select studyId,studyCode,studyName,studyStartDate,studyTypeName,studyTypeCode,
                                         studyCategoryName,studyCategoryCode,statusName ,sponsorName
                                         from Studies,StudyTypes,StudyCategories,Statuses,Sponsors
                                          where Studies.studyId=@StudyId and Studies.studyTypeId=StudyTypes.studyTypeId
                                          and Studies.studyCategoryId=StudyCategories.studyCategoryId
                                         and Studies.studyStatusId=statuses.statusId and studies.masterSponsor=Sponsors.sponsorId";
                SqlCommand cmd1 = new SqlCommand(selectSql1, conn);
                SqlParameter p1 = new SqlParameter("@StudyID", StudyID);
                cmd1.Parameters.Add(p1);
                SqlDataReader reader = cmd1.ExecuteReader();

                while (reader.Read())
                {

                    s.StudyID = (int)reader[0];
                    s.StudyCode = (string)reader[1];
                    s.StudyName = (string)reader[2];
                    s.StudyStartDate = Convert.ToDateTime(reader["studyStartDate"]);
                    s.StudyType = new StudyType();
                    s.StudyType.StudyTypeName = (string)reader[4];
                    s.StudyType.StudyTypeCode = (string)reader[5];
                    s.StudyCategory = new StudyCategory();
                    s.StudyCategory.StudyCategoryName = (string)reader[6];
                    s.StudyCategory.StudyCategoryCode = (string)reader[7];
                    s.StudyStatus = (string)reader[8];
                    s.Sponsor = new Sponsor();
                    s.Sponsor.SponsorName = (string)reader[9];
                    List<Activity> activities = new List<Activity>();
                    activities = GetActivities(s.StudyID);
                    foreach (Activity a in activities)
                    {
                        s.SetActivity(a);
                    }



                }
             

                return s;
            }
            catch (SqlException e)
            {
                throw new Exception("No studies present");
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
    }
}
