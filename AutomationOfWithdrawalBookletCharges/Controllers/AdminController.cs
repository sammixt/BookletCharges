using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.Models;
using AutomationOfWithdrawalBookletCharges.LIB;
using System.Threading;
using System.Security.Principal;

namespace AutomationOfWithdrawalBookletCharges.Controllers
{
    public class AdminController : Controller
    {
        [AllowAnonymous]
        public ActionResult Process(string urlReturn)
        {
            ViewBag.returnUrl = urlReturn;
            return View();
        }

        // GET: /Admin/Create
        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Process(LoginViewModel model, string urlReturn)
        {
            ConnectionService connectService = new ConnectionService();
            //var pass = AuthenticationService.Validate(model.UserName, model.Password);
            var profile = AuthenticationService.GetUserProfile(model.UserName);//remove in deployment
            if (profile.EmployeeNumber != null)
            {
                var claims = new List<Claim>();
                //var profile = AuthenticationService.GetUserProfile(model.UserName);
                var loginStat = connectService.checkStat(profile.EmployeeNumber);
                if(loginStat == "DISABLED" || loginStat == null)
                {
                  ViewBag.ErrorMessage = "You do not have permission to access this application. Please contact the system administrator for more details.";
                  return View(model);
                }
                var role = UsersRole.getLoginUsersRole(profile.EmployeeNumber);
                
                //Uncomment for role access admin
                var roles = role.Find(x => (x.RoleId == 1) || (x.RoleId == 4));
                if (roles == null)
                {
                    ViewBag.ErrorMessage = "You do not have permission to access this application. Please contact the system administrator for more details.";

                    return View(model);
                }

                //Actor claims to hold lastloginId
                LoginLogModel.addLoginLog(profile.EmployeeNumber);
                var lastLoginId = LoginLogModel.getLastLoginId(profile.EmployeeNumber);
                if (lastLoginId == null)
                {
                    ViewBag.ErrorMessage = "Connection cannot be established.";
                    return View(model);
                }
                claims.Add(new Claim(ClaimTypes.Name, profile.UserName));
                claims.Add(new Claim(ClaimTypes.GivenName, profile.FullName));
                claims.Add(new Claim(ClaimTypes.SerialNumber, profile.EmployeeNumber));
                claims.Add(new Claim(ClaimTypes.Email, profile.Email));
                claims.Add(new Claim(ClaimTypes.Actor, lastLoginId));
                claims.Add(new Claim(ClaimTypes.StateOrProvince, profile.Branch));
                claims.Add(new Claim(ClaimTypes.PostalCode, profile.BranchCode));              
                claims.Add(new Claim(ClaimTypes.Role, roles.RoleName));
                SignInAsync(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie));
                return RedirectToLocal(urlReturn);
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View(model); 
        }

        public ActionResult MyProfile()
        {
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void SignInAsync(ClaimsIdentity id)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(id);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "AdminDetails");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var principal = (ClaimsIdentity)User.Identity;
            Claim loginIdClaim = principal.FindFirst(ClaimTypes.Actor);
            int loginId = Convert.ToInt32(loginIdClaim.Value);
            LoginLogModel.updateLogLogout(loginId);
            AuthenticationManager.SignOut();
            return RedirectToAction("Process", "Admin");
        }
    }
}
