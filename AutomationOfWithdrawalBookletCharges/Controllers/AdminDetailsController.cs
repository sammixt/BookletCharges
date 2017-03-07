using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutomationOfWithdrawalBookletCharges.Models;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.LIB;

namespace AutomationOfWithdrawalBookletCharges.Controllers
{
    [Authorize]
    public class AdminDetailsController : Controller
    {
        
        public ActionResult Index(string branchcode)
        {
            ViewBag.AddUserSuccessMessage = TempData["AddUserSuccessMessage"];
           AdminModel AllUser = new AdminModel();    
            ModelState.Clear();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            Claim adminRoleClaim = principal.FindFirst(ClaimTypes.Role);
            ViewBag.Branches = AcountRetrievalServices.GetBranches();
            string adminRole = adminRoleClaim.Value;
            string branchCode = branchcodeClaim.Value;
            if(branchcode != null)
            {
                if (branchcode.Equals("1"))
                    return View(AllUser.retrieveUsersAdmin());
                else
                    return View(AllUser.retrieveUsers(branchcode));
            }else
                return View(AllUser.retrieveUsersAdmin());
            //for super admin retrieveUsersAdmin()                
        }

        public ActionResult Enable(string id)
        {
            AdminModel.updateUserStat(id, "ENABLED");
            TempData["AddUserSuccessMessage"] = "User account with Staff Number: "+ id + " have been enabled.";
            return RedirectToAction("Index", "AdminDetails");
        }

        public ActionResult Disable(string id)
        {
            AdminModel.updateUserStat(id, "DISABLED");
            TempData["AddUserSuccessMessage"] = "User account with Staff Number: " + id + " have been disabled.";
            return RedirectToAction("Index", "AdminDetails");
        }

        
        public ActionResult Create()
        {
            ViewBag.AddUserErrorMessage = TempData["AddUserErrorMessage"];
            RolesViewModel roles = new RolesViewModel();
            roles.Roles = AdminModel.populateRole();
            return View(roles);
        }

        public ActionResult Edit(string id)
        {
            ViewBag.EditUserErrorMessage = TempData["EditUserErrorMessage"];
            AdminModel AllUser = new AdminModel();
            ViewBag.RolesID = AdminModel.populateRole(); 
            return View(AllUser.retrieveToEditUsers(id));  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfileViewModel users)
        {
            var empNum = users.EmployeeNumber;
            var userName = users.UserName;
            var selectedId = Convert.ToInt32(users.RolesID);
            bool compTitleOp = AdminModel.compareEditedUserRole(selectedId, userName);//comparing users title
            if (compTitleOp == false)
            {
                TempData["EditUserErrorMessage"] = "The Title does not match with record on AD";
                return RedirectToAction("Edit","AdminDetails",new {id = empNum});
            }else
            {
                AdminModel.updateUserRole(selectedId, empNum);
                TempData["AddUserSuccessMessage"] = "Users role with Staff Number: " + empNum + " have been Edited.";
                return RedirectToAction("Index", "AdminDetails");
            }
            
        }

        //Refer to Admin Model for the Logic....
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createuser(RolesViewModel roles)
        {
             var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            Claim adminRoleClaim = principal.FindFirst(ClaimTypes.Role);
            string adminRole = adminRoleClaim.Value;
                
            ///////////////////////////////////////////////////////
            roles.Roles = AdminModel.populateRole();
            //values retured from selected item
            var selectedItem = roles.Roles.Find(p => p.Value == roles.RoleId.ToString());
             selectedItem.Selected = true;
                string textC =  selectedItem.Text;
                int roleNumber = roles.RoleId;
                string userName = roles.username;

                string compTitle = textC.Split('-')[1];
                var profile = AuthenticationService.GetUserProfile(userName);
                if (profile.EmployeeNumber != null) { 
                //omit for super admin
                if (adminRole == "Admin") {
                    bool compTitleOp = AdminModel.compareTitle(compTitle, userName);//comparing users title
                    //bool compBranch = AdminModel.compareBranch(branchCode, userName);// compare users branch
                    //if (!compBranch)
                    //{
                    //    TempData["AddUserErrorMessage"] = "User Assign is neither SSA nor HSSA in their branch";
                    //    return RedirectToAction("Create", "AdminDetails");
                    //}
                
                    if (!compTitleOp)
                    {
                        TempData["AddUserErrorMessage"] = "User Assign is neither SSA nor HSSA in their branch";
                        return RedirectToAction("Create", "AdminDetails");
                    }
                    else 
                    {
                        //check if user already exist on the database.
                        bool userExist = AdminModel.chkUserExist(userName);
                        if (userExist == true) { 
                            TempData["AddUserErrorMessage"] = "The user already Exist on the System...";
                            return RedirectToAction("Create", "AdminDetails");
                        }
                        AdminModel.AddUserRecord(userName, roleNumber);
                    }
                    TempData["AddUserSuccessMessage"] = "New user successfully added...";
                    return RedirectToAction("Index", "AdminDetails");
                }else
                {
                    bool userExist = AdminModel.chkUserExist(userName);
                    if (userExist == true)
                    {
                        TempData["AddUserErrorMessage"] = "The user already Exist on the System...";
                        return RedirectToAction("Create", "AdminDetails");
                    }
                    AdminModel.AddUserRecord(userName, roleNumber);
                    TempData["AddUserSuccessMessage"] = "New user successfully added...";
                    return RedirectToAction("Index", "AdminDetails");
                }
                }
                else
                {
                    TempData["AddUserErrorMessage"] = "Username does not exist on AD";
                    return RedirectToAction("Create", "AdminDetails");
                }
           
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create()
        //{
        //    //RolesViewModel roles = new RolesViewModel();
        //    //roles.Roles = AdminModel.populateRole();
        //    return View();
        //}
	}
}