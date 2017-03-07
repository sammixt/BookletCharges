using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomationOfWithdrawalBookletCharges.ViewModel
{
    public class RolesViewModel
    {
       
            public List<SelectListItem> Roles { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDesc { get; set; }
            [Display(Name ="AD Username")]
            
            [Required]
            public string username { get; set; }
        
    }
}