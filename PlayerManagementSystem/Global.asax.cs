using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PlayerManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[Constants.AuthenticationCookieName];
            if (authCookie != null)
            {
                // Get the forms authentication ticket.
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var identity = new GenericIdentity(authTicket.Name, "Forms");
                var principal = new PrincipalModel(identity);
                // Get the custom user data encrypted in the ticket.
                if (!string.IsNullOrWhiteSpace(authTicket.UserData))
                {
                    var serializer = new JavaScriptSerializer();
                    principal.User = (UserModel)serializer.Deserialize(authTicket.UserData, typeof(UserModel));
                    Context.User = principal;
                }

            }
        }

        protected void Application_Error()
        {

        }
    }
}
