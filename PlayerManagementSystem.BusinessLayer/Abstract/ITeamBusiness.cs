using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Abstract
{
    public interface ITeamBusiness
    {
        List<TeamModel> GetAllTeams();
        int DeleteTeam(int teamID);
        int GetMaxTeamID();
        TeamModel GetTeamDetails(int teamID);
        int SaveTeamDetails(TeamModel team);
        List<PlayerModel> GetTeamMembers(int TeamID);
        int AssignTeamMembers(int teamID, string playerIDs);
    }
}
