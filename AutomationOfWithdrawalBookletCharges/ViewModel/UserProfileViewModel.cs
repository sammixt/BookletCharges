using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomationOfWithdrawalBookletCharges.ViewModel
{
    public class UserProfileViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Employee #")]
        public string EmployeeNumber { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public string BranchCode { get; set; }

        public string Branch { get; set; }

        public string Department { get; set; }

        public string Title { get; set; }

        public string Roles { get; set; }

        [Display(Name = "Users Status")]
        public string Status { get; set; }
        
        //for edit purpose
        public string RolesID { get; set; }
    }

    public class BranchViewModel
    {
        public string BranchId { get; set; }
        public string BranchLocationName { get; set; }
    }
}