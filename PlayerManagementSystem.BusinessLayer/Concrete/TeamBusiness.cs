using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Concrete;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Concrete
{
    public class TeamBusiness : ITeamBusiness
    {
        private readonly ITeamDataAccess teamDataAccess;

        public TeamBusiness()
        {
            teamDataAccess = new TeamDataAccess();
        }
        public List<TeamModel> GetAllTeams()
        {
            return teamDataAccess.GetAllTeams();
        }

        public int DeleteTeam(int teamID)
        {
            return teamDataAccess.DeleteTeam(teamID);
        }

        public int GetMaxTeamID()
        {
            return teamDataAccess.GetMaxTeamID();
        }

        public TeamModel GetTeamDetails(int teamID)
        {
            return teamDataAccess.GetTeamData(teamID);
        }

        public int SaveTeamDetails(TeamModel team)
        {
            return teamDataAccess.SaveTeamDetails(team);
        }

        public List<PlayerModel> GetTeamMembers(int TeamID)
        {
            return teamDataAccess.GetTeamMembers(TeamID);
        }

        public int AssignTeamMembers(int teamID, string playerIDs)
        {
            return teamDataAccess.AssignTeamMembers(teamID, playerIDs);
        }
    }
}
