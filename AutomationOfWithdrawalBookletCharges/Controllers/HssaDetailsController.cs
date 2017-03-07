using AutomationOfWithdrawalBookletCharges.LIB;
using AutomationOfWithdrawalBookletCharges.Models;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AutomationOfWithdrawalBookletCharges.Controllers
{
    [Authorize]
    public class HssaDetailsController : Controller
    {

        private static Random random = new Random();
        //
        // GET: /HssaDetails/
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.DisplayMessage = "List of Pending Request For " + DateTime.Now.ToLongDateString();
            CustomerModel RequestPool = new CustomerModel();   
            ModelState.Clear();
            var start_date = DateTime.Now.ToShortDateString();
            var end_date = DateTime.Now.ToShortDateString();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            return View(RequestPool.searchRequest(start_date,end_date,branchCode,"PENDING"));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(CustomerViewModel model)
        {
            CustomerModel RequestPool = new CustomerModel();   
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            string strDate = String.Format("{0:MM/dd/yyyy}", model.SearchStartDate);
            string endDate = String.Format("{0:MM/dd/yyyy}", model.SearchEndDate);
            string status = model.Status;
            
            if(strDate != null && endDate != null)
            {
                bool comp = String.Equals(strDate, endDate);
                if(comp == true)
                {
                    ViewBag.DisplayMessage = "List of " + ToTitleCase(status) + " For " + longDate(strDate);
                }
                else {
                    ViewBag.DisplayMessage = "List of " + ToTitleCase(status) + " Request From " + longDate(strDate) + " to " + longDate(endDate);
                }
            }
            return View("Index", RequestPool.searchRequest(strDate, endDate, branchCode, status));
            
            
        }

        [Authorize]
        public ActionResult Display(string id)
        {
            CustomerModel details = new CustomerModel();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            return View(details.displayRequestDetails(id, branchCode).FirstOrDefault());
        }
        public ActionResult DisplayState(string id)
        {
            CustomerModel details = new CustomerModel();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            return View("DisplayState",details.displayRequestDetails(id, branchCode).FirstOrDefault());
        }

        [Authorize,HttpPost]
        public ActionResult Approve(CustomerViewModel model)
        {
            PostingServices postClient = new PostingServices();
            SendEmail mailHssa = new SendEmail();
            var principal = (ClaimsIdentity)User.Identity;
            Claim empNumClaim = principal.FindFirst(ClaimTypes.SerialNumber);
            Claim loginIdClaim = principal.FindFirst(ClaimTypes.Actor);
            Claim branchCodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            var name = principal.FindFirst(ClaimTypes.GivenName).Value;
            var body = "";
            ViewBag.Message = "";
            string branchCode = branchCodeClaim.Value;
            int loginId = Convert.ToInt32(loginIdClaim.Value);        
            string batchId = RandomString(23);
            string payRef = RandomString(23);
            string empNum = empNumClaim.Value;//debit flexcube
            var emailParams = CustomerModel.sentToSsaParams(model);
            var emailparameters = emailParams.FirstOrDefault();
            List<string> repEmail = new List<string>(); repEmail.Add(emailparameters.Email);
            var title = "RE: Withdrawal Booklet Request for " + emailparameters.AccountNumber;
            string postingStat = postClient.postingTransaction(branchCode, model.AccountNumber, batchId, payRef, model.RequestId);
            if (postingStat == "SUCCESSFUL")
            {
                var ssaEmail = CustomerModel.UpdateRecord(model, empNum, loginId, branchCode, "APPROVED");
                body = "<p>Hello " + emailparameters.SsaName + ",<p><p>The booklet request for the customer " + emailparameters.AccountName +
                " with account number " + emailparameters.AccountNumber + " have be authorized for issuance, kindly issue the booklet to the customer.";
                ViewBag.Message = "ISSUANCE SUCCESSFULLY COMPLETED";
            }               
            else
            {
                body = "<p>Hello " + emailparameters.SsaName + ",<p><p>The booklet request for the customer " + emailparameters.AccountName +
                " with account number " + emailparameters.AccountNumber + " cannot be processed at the moment.";
                ViewBag.Message = "ISSUANCE UNSUCCESSFUL (ACCOUNT NOT DEBITED)";
            }
            mailHssa.SendEmailWithoutAttachment(name, title, body, "", repEmail);
            return View("Message");
        }

        [Authorize, HttpPost]
        public ActionResult Decline(CustomerViewModel model)
        {
            SendEmail mailHssa = new SendEmail();
            var principal = (ClaimsIdentity)User.Identity;
            var name = principal.FindFirst(ClaimTypes.GivenName).Value;
            Claim empNumClaim = principal.FindFirst(ClaimTypes.SerialNumber);
            Claim loginIdClaim = principal.FindFirst(ClaimTypes.Actor);
            Claim branchCodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchCodeClaim.Value;
            int loginId = Convert.ToInt32(loginIdClaim.Value);
            string empNum = empNumClaim.Value;
            var ssaEmail = CustomerModel.UpdateRecord(model, empNum, loginId, branchCode, "DECLINED");
            var emailParams = CustomerModel.sentToSsaParams(model);
            var emailparameters = emailParams.FirstOrDefault();
            List<string> repEmail = new List<string>(); repEmail.Add(emailparameters.Email);
            var body = "<p>Hello "+ emailparameters.SsaName +",<p><p>The booklet request for the customer "+emailparameters.AccountName+  
            " with account number " +emailparameters.AccountNumber +
            " have been declined. <br/> Reason: "
                + emailparameters.Comment ;
            var title = "RE: Withdrawal Booklet Request for " + emailparameters.AccountNumber;

            mailHssa.SendEmailWithoutAttachment(name, title, body, "", repEmail);
            ViewBag.Message = "REQUEST HAVE BEEN DECLINED";
            return View("Message");
        }

        //counting request with pending status.
        public string countReq()
        {
            string branchCode = "";
            string reqNum = "";
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchCodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            try { 
            branchCode = branchCodeClaim.Value;
            reqNum = CustomerModel.countRequest(branchCode, "PENDING");
            }
            catch
            {
                reqNum = "0";
            }
            return reqNum;
        }

        private string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string longDate(string dates)
        {
            DateTime dt = DateTime.ParseExact(dates, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            string newdt = dt.ToLongDateString();
            return newdt;
        }

        public static string Truncate(string source, int length)
        {
            return source.Substring(0, Math.Min(length, source.Length));
        }

        //Reporting
        public ActionResult Report()
        {
            ViewBag.DisplayMessage = "Total Issued Booklets for " + DateTime.Now.ToLongDateString();
            ViewBag.Title = "Total Issued Booklets for " + DateTime.Now.ToLongDateString();
            CustomerModel RequestPool = new CustomerModel();
            ModelState.Clear();
            var start_date = DateTime.Now.ToShortDateString();
            var end_date = DateTime.Now.ToShortDateString();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            return View(RequestPool.searchReport(start_date, end_date, branchCode, "APPROVED"));
        }

        [Authorize]
        [HttpPost]
        public ActionResult ReportSearch(CustomerViewModel model)
        {
            CustomerModel RequestPool = new CustomerModel();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            string strDate = String.Format("{0:MM/dd/yyyy}", model.SearchStartDate);
            string endDate = String.Format("{0:MM/dd/yyyy}", model.SearchEndDate);
            string status = model.Status;

            if (strDate != null && endDate != null)
            {
                bool comp = String.Equals(strDate, endDate);
                if (comp == true)
                {
                    ViewBag.DisplayMessage = "Total Issued Booklets For " + longDate(strDate);
                    ViewBag.Title = "Total Issued Booklets For " + longDate(strDate);
                }
                else
                {
                    ViewBag.DisplayMessage = "Total Issued Booklets From  " + longDate(strDate) + " To " + longDate(endDate);
                    ViewBag.Title = "Total Issued Booklets From  " + longDate(strDate) + " To " + longDate(endDate);
                }
            }
            return View("Report", RequestPool.searchReport(strDate, endDate, branchCode, "APPROVED"));
        }
	}
}