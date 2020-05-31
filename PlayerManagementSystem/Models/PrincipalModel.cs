using PlayerManagementSystem.Models;
using System.Security.Principal;

namespace PlayerManagementSystem.UI.Models
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