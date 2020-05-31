using PlayerManagementSystem.Models;
using System.Collections.Generic;

namespace PlayerManagementSystem.DataAccessLayer.Abstract
{
    public interface IPlayersDataAccess
    {
        List<PlayerModel> GetAllPlayers();

        PlayerModel GetPlayerData(int playerID);

        int DeletePlayer(int playerID);

        bool SavePlayerDetails(PlayerModel player);

        int GetMaxPlayerID();
    }
}
