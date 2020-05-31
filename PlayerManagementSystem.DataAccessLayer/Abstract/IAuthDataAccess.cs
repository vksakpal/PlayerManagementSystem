using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.DataAccessLayer
{
    public interface IAuthDataAccess
    {
        UserModel Login(LoginModel logindetails);
    }
}
