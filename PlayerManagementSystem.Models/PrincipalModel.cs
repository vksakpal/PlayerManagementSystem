using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.Models
{
    public class PrincipalModel : IPrincipal
    {
        public PrincipalModel(IIdentity identity)
        {
            Identity = identity;
        }
        public IIdentity Identity
        {
            get;
            private set;
        }
        public UserModel User { get; set; }
        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
