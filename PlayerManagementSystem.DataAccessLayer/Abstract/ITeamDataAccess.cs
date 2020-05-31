using System;
using System.Collections.Generic;
using PlayerManagementSystem.Models;

namespace PlayerManagementSystem.DataAccessLayer.Abstract
{
    public interface ITeamDataAccess
    {
        List<TeamModel> GetAllTeams();
        int DeleteTeam(int teamID);
        int GetMaxTeamID();
        TeamModel GetTeamData(int teamID);
        int SaveTeamDetails(TeamModel team);
        List<PlayerModel> GetTeamMembers(int TeamID);
        int AssignTeamMembers(int teamID, string playerIDs);
    }
}
