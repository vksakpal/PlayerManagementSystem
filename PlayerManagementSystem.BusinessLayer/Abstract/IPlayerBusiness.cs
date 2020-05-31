using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Abstract
{
    public interface IPlayerBusiness
    {
        List<PlayerModel> GetAllPlayers();

        int DeletePlayer(int playerID);

        PlayerModel GetPlayerDetails(int playerID);

        bool SavePlayerDetails(PlayerModel player);

        int GetMaxPlayerID();
    }
}
