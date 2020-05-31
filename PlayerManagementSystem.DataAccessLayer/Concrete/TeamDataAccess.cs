using PlayerManagementSystem.DataAccessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Mappers;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PlayerManagementSystem.DataAccessLayer.Concrete
{
    public class TeamDataAccess : ITeamDataAccess
    {
        public int AssignTeamMembers(int teamID, string playerIDs)
        {
            int returnValue = 0;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("AssignTeamMembers", sql_conn);
                    sqlComm.Parameters.AddWithValue("TeamID", teamID.ToString());
                    sqlComm.Parameters.AddWithValue("PlayerIDs", playerIDs);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    returnValue = sqlComm.ExecuteNonQuery();
                    sql_conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }

        public int DeleteTeam(int teamID)
        {
            int returnValue = 0;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("DeleteTeam", sql_conn);
                    sqlComm.Parameters.AddWithValue("TeamID", teamID);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    returnValue = sqlComm.ExecuteNonQuery();
                    sql_conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }

        public List<TeamModel> GetAllTeams()
        {
            List<TeamModel> lstTeams = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetAllTeams", sql_conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    lstTeams = dt.MapTeamDetailsDataTable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstTeams;
        }

        public int GetMaxTeamID()
        {
            int returnValue = 0;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("GetMaxTeamID", sql_conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    returnValue = Convert.ToInt32(dt.Rows[0]["MAXID"]);
                    // returnValue = sqlComm.ExecuteNonQuery();
                    sql_conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }

        public TeamModel GetTeamData(int teamID)
        {
            TeamModel team = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetTEAMData", sql_conn);
                    sqlComm.Parameters.AddWithValue("TeamID", teamID);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    team = dt.MapTeamDetailDataTable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return team;
        }

        public List<PlayerModel> GetTeamMembers(int TeamID)
        {
            List<PlayerModel> lstPlayers = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetTeamMembers", sql_conn);
                    sqlComm.Parameters.AddWithValue("TeamID", TeamID);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    lstPlayers = dt.MapPlayerDetailsDataTable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPlayers;
        }

        public int SaveTeamDetails(TeamModel team)
        {
            int returnValue = 1;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("SaveTeamDetails", sql_conn);
                    sqlComm.Parameters.AddWithValue("TeamID", team.TeamID);
                    sqlComm.Parameters.AddWithValue("TeamName", team.TeamName);
                    sqlComm.Parameters.AddWithValue("Ground", team.Ground);
                    sqlComm.Parameters.AddWithValue("Coach", team.Coach);
                    sqlComm.Parameters.AddWithValue("FoundedYear", team.FoundedYear);
                    sqlComm.Parameters.AddWithValue("Region", team.Region);

                    sqlComm.Parameters.Add("@Status", SqlDbType.Int);
                    sqlComm.Parameters["@Status"].Direction = ParameterDirection.Output;

                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.ExecuteNonQuery();

                    returnValue = Convert.ToInt32(sqlComm.Parameters["@Status"].Value);

                    sql_conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }
    }
}
