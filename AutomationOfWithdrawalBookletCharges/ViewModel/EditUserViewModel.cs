using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomationOfWithdrawalBookletCharges.ViewModel
{
    public class EditUserViewModel
    {
        public List<SelectListItem> Roles { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; } 
        public string StaffId { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string userRole { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }

}