
using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.BusinessLayer.Concrete;
using PlayerManagementSystem.Models;
using PlayerManagementSystem.UI.Models;
using PlayerManagementSystem.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PlayerManagementSystem.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthBusiness authBusiness;

        public AuthController()
        {
            authBusiness = new AuthBusiness();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            ViewBag.RoleList = new List<SelectListItem>
            { 
                new SelectListItem
                {
                    Text="User",
                    Value="User"
                },
                new SelectListItem
                {
                    Text="Admin",
                    Value="Admin"
                }
            };
            loginModel.Role = "User";
            return View("LoginView", loginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateUser(LoginModel loginModel)
        {
            if (loginModel != null)
            {
                if (ValidateUser(loginModel, HttpContext.Response))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
                        
            return View("LoginView", loginModel);
        }

        private bool ValidateUser(LoginModel loginModel, HttpResponseBase response)
        {
            bool result = false;
            
            UserModel user = authBusiness.ValidateUser(loginModel);
            if (user != null)
            {
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        loginModel.UserName,
                        DateTime.Now,
                        DateTime.Now.AddDays(30),
                        true,
                        userData,
                        FormsAuthentication.FormsCookiePath);
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);
                // Create the cookie.
                response.Cookies.Add(new HttpCookie(Constants.AuthenticationCookieName, encTicket));
                result = true;
            }
            return result;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            HttpCookie cookie = new HttpCookie(Constants.AuthenticationCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Login", "Auth");
        }
    }
}