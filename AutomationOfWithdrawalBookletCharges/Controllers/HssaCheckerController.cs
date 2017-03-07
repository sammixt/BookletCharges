using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.LIB;
using AutomationOfWithdrawalBookletCharges.Models;

namespace AutomationOfWithdrawalBookletCharges.Controllers
{
    public class HssaCheckerController : Controller
    {
        //
        // GET: /HssaChecker/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model, string urlReturn)
        {
            //var pass = AuthenticationService.Validate(model.UserName, model.Password);
            var profile = AuthenticationService.GetUserProfile(model.UserName);
            if (profile.EmployeeNumber != null)
            {
                var claims = new List<Claim>();
                //var profile = AuthenticationService.GetUserProfile(model.UserName);
                var role = UsersRole.getLoginUsersRole(profile.EmployeeNumber);
                var roles = role.Find(x => (x.RoleId == 3));
                
                if (roles == null)
                {
                    ViewBag.ErrorMessage = "You do not have permission to access this application. Please contact the system administrator for more details.";

                    return View(model);
                }

                //Actor claims to hold lastloginId
                LoginLogModel.addLoginLog(profile.EmployeeNumber);
                var lastLoginId = LoginLogModel.getLastLoginId(profile.EmployeeNumber);

                claims.Add(new Claim(ClaimTypes.Name, profile.UserName));
                claims.Add(new Claim(ClaimTypes.GivenName, profile.FullName));
                claims.Add(new Claim(ClaimTypes.SerialNumber, profile.EmployeeNumber));
                claims.Add(new Claim(ClaimTypes.Email, profile.Email));
                claims.Add(new Claim(ClaimTypes.Actor, lastLoginId));
                claims.Add(new Claim(ClaimTypes.StateOrProvince, profile.Branch));
                claims.Add(new Claim(ClaimTypes.PostalCode, profile.BranchCode));
                claims.Add(new Claim(ClaimTypes.Role, roles.RoleName));

                //foreach (var r in roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, r));
                //}

                SignInAsync(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie));

                return RedirectToLocal(urlReturn);
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View(model);
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
                return RedirectToAction("Index", "HssaDetails");
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
            return RedirectToAction("Index", "HssaChecker");
        }
	}
}