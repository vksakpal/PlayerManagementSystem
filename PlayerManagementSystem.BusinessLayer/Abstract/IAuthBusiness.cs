using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Abstract
{
    public interface IAuthBusiness
    {
        UserModel ValidateUser(LoginModel logindetails);
    }
}
