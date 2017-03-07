using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.LIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using AutomationOfWithdrawalBookletCharges.Models;
using System.Globalization;

namespace AutomationOfWithdrawalBookletCharges.Controllers
{
    [Authorize]
    public class AccountCheckController : Controller
    {
        //
        // GET: /AccountCheck/
        public ActionResult Index()
        {
            ViewBag.NoAccountError = TempData["NoAccountError"];
            return View();
        }

        [HttpPost]
        public ActionResult Display(CustomerViewModel model)
        {
            var customerDetails = AcountRetrievalServices.GetCustomerAcctDetails(model.AccountNumber);
            if (customerDetails != null)
            { return View(customerDetails); }
            TempData["NoAccountError"] = "Account Does not Exist";
            return RedirectToAction("Index", "AccountCheck");

        }

        // insert records into the database and send alert to hssa
        [HttpPost]
        public ActionResult Proceed(CustomerViewModel model)
        {
            SendEmail mailHssa = new SendEmail();
            var principal = (ClaimsIdentity)User.Identity;
            Claim empNumClaim = principal.FindFirst(ClaimTypes.SerialNumber);
            Claim loginIdClaim = principal.FindFirst(ClaimTypes.Actor);
            Claim branchCodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            var name = principal.FindFirst(ClaimTypes.GivenName).Value;
            var hostname = HttpContext.Request.Url.Host;
            string port = Convert.ToString(HttpContext.Request.Url.Port);
            string branchCode = branchCodeClaim.Value;
            int loginId = Convert.ToInt32(loginIdClaim.Value);
            string empNum = empNumClaim.Value;
            string hssaEmail = CustomerModel.AddRecord(model, empNum, loginId, branchCode);
            List<string> repEmail = new List<string>(); repEmail.Add(hssaEmail);
            var body = "<p>Hello,<p><p>A new Withdrawal Booklet Request awaits your approval, please log on to "+
            "the Booklet Management Application on http://"
                + hostname + ":" + port + "/HssaChecker/Index to approve act on the request.</p>";
            var title = "New Withdrawal Booklet Request";
             //List<string> ccEmail = new List<string>(); ccEmail.Add("");
             //List<string> bccEmail = new List<string>(); bccEmail.Add("");
            mailHssa.SendEmailWithoutAttachment(name,title , body, "", repEmail);
        
            //model.RequestId = requestid;
            return View("Message", model);
        }

        public ActionResult RequestList(string id)
        {
            ViewBag.DisplayMessage = "List of " + ToTitleCase(id) + " Request For " + DateTime.Now.ToLongDateString();
            CustomerModel RequestPool = new CustomerModel();
            ModelState.Clear();
            var start_date = DateTime.Now.ToShortDateString();
            var end_date = DateTime.Now.ToShortDateString();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            return View(RequestPool.searchRequest(start_date, end_date, branchCode, id));
        }

        public ActionResult DisplayState(string id)
        {
            CustomerModel details = new CustomerModel();
            var principal = (ClaimsIdentity)User.Identity;
            Claim branchcodeClaim = principal.FindFirst(ClaimTypes.PostalCode);
            string branchCode = branchcodeClaim.Value;
            return View("ListDetails", details.displayRequestDetails(id, branchCode).FirstOrDefault());
        }

        private string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
