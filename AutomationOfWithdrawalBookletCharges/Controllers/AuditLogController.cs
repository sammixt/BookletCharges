using AutomationOfWithdrawalBookletCharges.Models;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomationOfWithdrawalBookletCharges.Controllers
{
    [Authorize]
    public class AuditLogController : Controller
    {
        //
        // GET: /AuditLog/
        public ActionResult LoginLog(LoginLogViewModel model)
        {
            string strDate = String.Format("{0:MM/dd/yyyy}", model.SearchStartDate);
            string endDate = String.Format("{0:MM/dd/yyyy}", model.SearchEndDate);
            if (strDate == "" && endDate == "")
            {
                strDate = DateTime.Now.ToShortDateString();
                endDate = DateTime.Now.ToShortDateString();
            }
            var loginLogDetails = LoginLogModel.retrieveLoginLog(strDate, endDate);
            return View(loginLogDetails);
        }

        public ActionResult RequestLog(CustomerViewModel model, string login_id = null)
        {
            if (login_id == null)
            {
                string strDate = String.Format("{0:MM/dd/yyyy}", model.SearchStartDate);
                string endDate = String.Format("{0:MM/dd/yyyy}", model.SearchEndDate);
                if (strDate == "" && endDate == "")
                {
                    strDate = DateTime.Now.ToShortDateString();
                    endDate = DateTime.Now.ToShortDateString();
                }
                var RequestLogDetails = AuditLogModels.displayRequestLog(strDate, endDate, login_id);
                return View(RequestLogDetails);
            }
            else
            {
                var RequestLogDetails = AuditLogModels.displayRequestLog(null, null, login_id);
                return View(RequestLogDetails);
            }
            
        }

        public ActionResult PostingLog(PostingViewModel model, string login_id = null)
        {
            if (login_id == null)
            {
                string strDate = String.Format("{0:MM/dd/yyyy}", model.SearchStartDate);
                string endDate = String.Format("{0:MM/dd/yyyy}", model.SearchEndDate);
                if (strDate == "" && endDate == "")
                {
                    strDate = DateTime.Now.ToShortDateString();
                    endDate = DateTime.Now.ToShortDateString();
                }
                var CreditLogDetails = AuditLogModels.displayPostingLogCr(strDate, endDate, login_id);
                ViewBag.DebitLogDetails = AuditLogModels.displayPostingLogDr(strDate, endDate, login_id);
                return View(CreditLogDetails);
            }
            else
            {
                var CreditLogDetails = AuditLogModels.displayPostingLogCr(null, null, login_id);
                ViewBag.DebitLogDetails = AuditLogModels.displayPostingLogDr(null, null, login_id);
                return View(CreditLogDetails);
            }

        }
	}
}