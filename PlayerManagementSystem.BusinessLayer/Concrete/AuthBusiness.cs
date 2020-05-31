using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer;
using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Concrete
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IAuthDataAccess authDataAccess;
        public AuthBusiness()
        {
            authDataAccess = new AuthDataAccess();
        }

        public UserModel ValidateUser(LoginModel logindetails)
        {
            return authDataAccess.Login(logindetails);
        }
    }
}
