using PlayerManagementSystem.Mappers;
using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.DataAccessLayer
{
    public class AuthDataAccess : IAuthDataAccess
    {
        public UserModel Login(LoginModel logindetails)
        {
            UserModel ud = null;

            if (logindetails == null)
            {
                return ud;
            }

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("Login", sql_conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@username", logindetails.UserName);
                    sqlComm.Parameters.AddWithValue("@password", logindetails.Password);                   
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);
                    ud = dt.MapUserDetailsDataTable();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ud;
        }
    }
}
