using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PlayerManagementSystem.Mappers
{
    public static class UserDetailsMapper
    {
        public static UserModel MapUserDetailsDataTable(this DataTable dt)
        {
            UserModel UsertDetails = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                return new UserModel()
                {
                    Username = Convert.ToString(dt.Rows[0]["TXT_NME_USER"]),
                    ID = Convert.ToInt16(dt.Rows[0]["NUM_ID"]),
                    Role = Convert.ToString(dt.Rows[0]["TXT_ROLE_USER"]),
                };
            }

            return UsertDetails;
        }
    }
}