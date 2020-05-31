using System;

namespace PlayerManagementSystem.Models
{
    public class PlayerModel
    {
        public int IDNumber { get; set; }
        public string PlayerName { get; set; }
        public string DateOfBirth { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string PlaceOfBirth { get; set; }
        public string AssignedTeam { get; set; }
        public int AssignedTeamId { get; set; }

        public int Age { get; set; }

    }
}
