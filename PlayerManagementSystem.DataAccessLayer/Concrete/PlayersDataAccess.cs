using PlayerManagementSystem.DataAccessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Mappers;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace PlayerManagementSystem.DataAccessLayer.Concrete
{
    public class PlayersDataAccess : IPlayersDataAccess
    {
        public List<PlayerModel> GetAllPlayers()
        {
            List<PlayerModel> lstPlayers = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetAllPlayers", sql_conn);
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

        public int DeletePlayer(int playerID)
        {
            int returnValue = 0;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("DeletePlayer", sql_conn);
                    sqlComm.Parameters.AddWithValue("PlayerID", playerID);
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

        public PlayerModel GetPlayerData(int playerID)
        {
            PlayerModel player = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetPlayerData", sql_conn);
                    sqlComm.Parameters.AddWithValue("PlayerID", playerID);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    player = dt.MapPlayerDetailDataTable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return player;
        }

        public bool SavePlayerDetails(PlayerModel player)
        {
            bool isSaved = false;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("SavePlayerDetails", sql_conn);

                    List<DbParameter> dbParameters = new List<DbParameter>
                    {
                        new SqlParameter
                        {
                            ParameterName = "@PlayerID",
                            SqlDbType = SqlDbType.Int,
                            Value = player.IDNumber,
                             Direction = ParameterDirection.Input
                        },
                        new SqlParameter
                        {
                            ParameterName = "@PlayerName",
                            SqlDbType = SqlDbType.VarChar,
                            Value = player.PlayerName,
                             Direction = ParameterDirection.Input
                        },
                        new SqlParameter
                        {
                            ParameterName = "@Weight",
                            SqlDbType = SqlDbType.Decimal,
                            Value = player.Weight,  
                            Precision = 5,
                            Scale = 2     ,                         
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter
                        {
                            ParameterName = "@Height",
                           SqlDbType = SqlDbType.Decimal,
                            Value = player.Height,
                            Precision = 5,
                            Scale = 2,
                             Direction = ParameterDirection.Input
                        },
                        new SqlParameter
                        {
                            ParameterName = "@PlaceOfBirth",
                           SqlDbType = SqlDbType.VarChar,
                            Value = player.PlaceOfBirth,
                             Direction = ParameterDirection.Input
                        },
                        new SqlParameter
                        {
                            ParameterName = "@DateOfBirth",
                            SqlDbType = SqlDbType.DateTime,
                            Value = player.DateOfBirth,
                             Direction = ParameterDirection.Input
                        },
                        new SqlParameter
                        {
                            ParameterName = "@AssignedTeamID",
                           SqlDbType = SqlDbType.Int,
                            Value = player.AssignedTeam,
                             Direction = ParameterDirection.Input
                        }

                    };

                    sqlComm.Parameters.AddRange(dbParameters.ToArray());

                    sqlComm.CommandType = CommandType.StoredProcedure;
                    int returnValue = sqlComm.ExecuteNonQuery();
                    if (returnValue == 1)
                    {
                        isSaved = true;
                    }
                    sql_conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return isSaved;
        }

        public int GetMaxPlayerID()
        {
            int returnValue = 0;
            try
            {
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    DataTable dt = new DataTable();
                    sql_conn.Open();
                    SqlCommand sqlComm = new SqlCommand("GetMaxPlayerID", sql_conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);
                    returnValue = Convert.IsDBNull(dt.Rows[0]["MAXID"]) ? 100000 : Convert.ToInt32(dt.Rows[0]["MAXID"]);
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
