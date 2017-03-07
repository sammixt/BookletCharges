using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomationOfWithdrawalBookletCharges.ViewModel
{
    public class CustomerViewModel
    {
        public string RequestId { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Account Balance")]
        public decimal AccountBalance { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Display(Name = "Last Issue Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfLastIssue { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RequestCreationDate { get; set; }

        public string RequestCreationTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string RequestAuthorizationDate { get; set; }

        public string RequestAuthorizationTime { get; set; }
        
        [Required]
        [Display(Name = "Serial Number From")]
        public string SerialNoStart { get; set; }

        [Required]
        [Display(Name = "Serial Number To")]
        public string SerialNoEnd { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
        //when retrieving for the hssa view
        public string SsaName { get; set; }

        public string Email { get; set; }
        
        public string SsaId { get; set; }

        public string HssaId { get; set; }

        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SearchStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SearchEndDate { get; set; }
    }
}