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
    public class PlayersBusiness : IPlayerBusiness
    {
        private readonly IPlayersDataAccess playersDataAccess;

        public PlayersBusiness()
        {
            playersDataAccess = new PlayersDataAccess();
        }

        public int DeletePlayer(int playerID)
        {
            return playersDataAccess.DeletePlayer(playerID);
        }

        public List<PlayerModel> GetAllPlayers()
        {
            return playersDataAccess.GetAllPlayers();
        }

        public int GetMaxPlayerID()
        {
            return playersDataAccess.GetMaxPlayerID();
        }

        public PlayerModel GetPlayerDetails(int playerID)
        {
            return playersDataAccess.GetPlayerData(playerID);
        }

        public bool SavePlayerDetails(PlayerModel player)
        {
            return playersDataAccess.SavePlayerDetails(player);
        }
    }
}
