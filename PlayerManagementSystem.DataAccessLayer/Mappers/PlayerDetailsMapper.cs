using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PlayerManagementSystem.DataAccessLayer.Mappers
{
    public static class PlayerDetailsMapper
    {
        public static List<PlayerModel> MapPlayerDetailsDataTable(this DataTable dt)
        {
            List<PlayerModel> lstPlayers = new List<PlayerModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lstPlayers.Add(new PlayerModel
                    {
                        IDNumber = Convert.ToInt32(row["NUM_ID_PLAYER"]),
                        PlayerName = row["TXT_NME_PLAYER"].ToString(),
                        DateOfBirth = Convert.ToDateTime(row["DT_BIRTH"]).ToString("yyyy-MM-dd"),
                        Height = Convert.ToDecimal(row["NUM_HEIGHT"]),
                        Weight = Convert.ToDecimal(row["NUM_WEIGHT"]),
                        PlaceOfBirth = row["TXT_PLACE_BIRTH"].ToString(),                        
                        AssignedTeam = row["AssignedTeam"].ToString(),
                        Age =  Convert.ToInt32( row["AGE"])
                        
                    });
                }
            }
            return lstPlayers;
        }

        public static PlayerModel MapPlayerDetailDataTable(this DataTable dt)
        {
            PlayerModel playerDetails = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                return new PlayerModel()
                {
                    IDNumber = Convert.ToInt32(dt.Rows[0]["NUM_ID_PLAYER"]),
                    PlayerName = dt.Rows[0]["TXT_NME_PLAYER"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dt.Rows[0]["DT_BIRTH"]).ToString("yyyy-MM-dd"),
                    Height = Convert.ToDecimal(dt.Rows[0]["NUM_HEIGHT"]),
                    Weight = Convert.ToDecimal(dt.Rows[0]["NUM_WEIGHT"]),
                    PlaceOfBirth = dt.Rows[0]["TXT_PLACE_BIRTH"].ToString()  ,
                    AssignedTeam=dt.Rows[0]["NUM_ID_TEAM"] .ToString()
                };
            }

            return playerDetails;
        }
    }
}
