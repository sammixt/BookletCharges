using AutomationOfWithdrawalBookletCharges.FcubsGetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Configuration;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.Models;

namespace AutomationOfWithdrawalBookletCharges.LIB
{
    public class AcountRetrievalServices
    {
        
        private static EnquiryServiceClient RetrievalServices()
        {
            var   proxy = new FcubsGetService.EnquiryServiceClient();
            proxy.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["SrvUsername"];
            proxy.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["SrvPassword"];
            return proxy;
        }


        public static CustomerViewModel GetCustomerAcctDetails(string acctno)
        {
            var proxy = RetrievalServices();
             var acctEnqHeader = new UBNHeaderType();
            var customerDetails = proxy.getCustomerDetailsWithAcctNumber(ref acctEnqHeader, acctno);
            if (customerDetails != null) { 
            var customerAccDetails = proxy.getCustomerAcctsDetail(ref acctEnqHeader, acctno);
            var newdate = (CustomerModel.lastIssueDate(acctno) != null) ? CustomerModel.lastIssueDate(acctno) : null;
            return new CustomerViewModel
            {
                AccountName = customerDetails.customerName,
                PhoneNo = customerDetails.custPhone,
                AccountBalance = Convert.ToDecimal(customerAccDetails.availableBalance),
                AccountNumber = acctno,
                DateOfLastIssue = newdate
            };
            }
            else { return null; }
        }

        public static List<BranchViewModel> GetBranches()
        {
            List<BranchViewModel> Branches = new List<BranchViewModel>();
            var proxy = RetrievalServices();
            var acctEnqHeader = new UBNHeaderType();
            var BranchList = proxy.getBranchList(ref acctEnqHeader);
            if (BranchList != null)
            {
                foreach (var item in BranchList)
                {
                    Branches.Add(new BranchViewModel
                    {
                        BranchId = item.branchCode,
                        BranchLocationName = item.branchName
                    });
                }
                return Branches;
            }
            else { return null; }
        }

    }
}