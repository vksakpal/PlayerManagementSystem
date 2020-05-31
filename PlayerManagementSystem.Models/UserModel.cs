
using System;

namespace PlayerManagementSystem.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Role { get; set; }

        public string Name
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(Username))
                {
                    return Username.Split('@')[0];
                }
                return "Guest";
            }
        }
    }
}