using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.DataAccessLayer.Mappers
{
    public static class TeamDetailsMapper
    {
        public static List<TeamModel> MapTeamDetailsDataTable(this DataTable dt)
        {
            List<TeamModel> lastTeams = new List<TeamModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lastTeams.Add(new TeamModel
                    {
                        TeamID = Convert.ToInt32(row["NUM_ID_TEAM"]),
                        TeamName = row["TXT_ID_PLAYER"].ToString(),
                        Ground = (row["TXT_NME_TEAM_GROUND"]).ToString(),
                        Coach = (row["TXT_NME_COACH"]).ToString(),
                        FoundedYear = (row["TXT_FOUNDED_YEAR"]).ToString(),
                        Region = row["TXT_REGION"].ToString()
                    });
                }
            }
            return lastTeams;
        }

        public static TeamModel MapTeamDetailDataTable(this DataTable dt)
        {
            TeamModel teamDetails = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                return new TeamModel()
                {
                    TeamID = Convert.ToInt32(dt.Rows[0]["NUM_ID_TEAM"]),
                    TeamName = dt.Rows[0]["TXT_ID_PLAYER"].ToString(),
                    Ground = (dt.Rows[0]["TXT_NME_TEAM_GROUND"]).ToString(),
                    Coach = (dt.Rows[0]["TXT_NME_COACH"]).ToString(),
                    FoundedYear = (dt.Rows[0]["TXT_FOUNDED_YEAR"]).ToString(),
                    Region = dt.Rows[0]["TXT_REGION"].ToString()
                };
            }

            return teamDetails;
        }
    }
}
