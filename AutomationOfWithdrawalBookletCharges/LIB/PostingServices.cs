using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutomationOfWithdrawalBookletCharges.PostingService;
using System.Configuration;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.Models;

namespace AutomationOfWithdrawalBookletCharges.LIB
{
    public class PostingServices
    {
        private static UBNMiddleWareWebServiceClient getPostingService()
        {
            PostingService.UBNMiddleWareWebServiceClient postingClient = new PostingService.UBNMiddleWareWebServiceClient();
            postingClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["SrvPostUsername"].ToString();
            postingClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["SrvPostPassword"].ToString();
            return postingClient;
        }

        private gefuTransactionsRequestData addRequestData()
        {
            PostingService.gefuTransactionsRequestData requestData = new PostingService.gefuTransactionsRequestData();
            requestData.requestModule = ConfigurationManager.AppSettings["ServiceModule"].ToString();
            requestData.moduleCredentials = ConfigurationManager.AppSettings["ServiceModuleCred"].ToString();
            requestData.transactionCurrency = "NGN";
            return requestData;
        }

        private  UBNHeaderType head()
        {
            PostingService.UBNHeaderType header = new PostingService.UBNHeaderType();
            return header;
        }

        private gefuBatchItemsData[] addRowData()
        {
            PostingService.gefuBatchItemsData[] rowData = new PostingService.gefuBatchItemsData[4];
            return rowData;
        }

        private gefuBatchItemsData addItemDr()
        {
             PostingService.gefuBatchItemsData item_dr = new PostingService.gefuBatchItemsData();
             return item_dr;
        }

        private gefuBatchItemsData addItemCr()
        {
             PostingService.gefuBatchItemsData item_cr = new PostingService.gefuBatchItemsData(); 
             return item_cr;
        }

        public string postingTransaction(string branch_code, string acct_num, string batchid,string payref, string transid)
        {
            var postingClient = getPostingService();
            var requestData = addRequestData();
            var header = head();
            var rowData = addRowData();
            var item_dr = addItemDr();
            string responseText = "";
            string response_code = "";
            string response_msg = ""; 
            requestData.batchId = batchid;
            requestData.initiatingBranch = branch_code;// "682";
            item_dr.acccountNumber = acct_num; // "0000767641";
            item_dr.amount = 315;
            item_dr.amountSpecified = true;
            item_dr.branchCode = "";
            item_dr.debitCreditIndicator = "D";
            item_dr.glCasaIndicator = "CASA";
            item_dr.narration = "Saving Withdrawal Booklet Charges";
            item_dr.paymentReference = payref;
            item_dr.transactionId = transid;
            //Insert Transaction into database
            DateTime start_date = DateTime.Now;
            PostingModel.AddLog(Convert.ToInt32(transid), payref, batchid, branch_code,
           acct_num, 315, "Saving Withdrawal Booklet Charges", start_date, "CASA",
           "CREDITS", ".bklt_mgr_insert_crdt_log");
            rowData[0] = item_dr;
            var creditDetails = PostingModel.creditAccount(transid, payref);

            for (int i = 0; i < creditDetails.Count; i++)
            {
                var item_cr = addItemCr();
                item_cr.acccountNumber = creditDetails[i].accountNumber; //"0040059353";
                item_cr.amount = creditDetails[i].amount; //10;
                item_cr.amountSpecified = creditDetails[i].amountSpecified;
                item_cr.branchCode = creditDetails[i].branchCode;
                item_cr.debitCreditIndicator = creditDetails[i].debitCreditIndicator;
                item_cr.glCasaIndicator = creditDetails[i].glCasaIndicator;//"CASA";
                item_cr.narration = creditDetails[i].narration;
                item_cr.paymentReference = creditDetails[i].paymentReference; //"t343435";
                item_cr.transactionId = creditDetails[i].transactionId;
                PostingModel.AddLog(Convert.ToInt32(transid), creditDetails[i].paymentReference, batchid, branch_code,
                   creditDetails[i].accountNumber, creditDetails[i].amount, creditDetails[i].narration, DateTime.Now, 
                   creditDetails[i].glCasaIndicator,
                   "DEBITS", ".bklt_mgr_insert_dbt_log");
                rowData[i + 1] = item_cr;
            }
           
            requestData.gefuBatchItemsDataList = rowData;
            gefuTransactionsResponseData responseData = postingClient.onlineGefuCbaPosting(ref header, requestData, "");
            if (responseData != null && (responseData.responseCode != null))
            {
                if(responseData.responseCode == "00")
                {
                    responseText = "SUCCESSFUL";
                }
                else
                {
                    responseText = "FAILED";
                }
                response_code = responseData.responseCode;
                response_msg = responseData.responseMessage;
            }
            else
            {
                responseText = "FAILED";
            }
            PostingModel.UpdateLog(Convert.ToInt32(transid), payref, DateTime.Now, responseText, response_code, response_msg,
            ".bklt_mgr_update_crdt_log");
            PostingModel.UpdateLog(Convert.ToInt32(transid), payref, DateTime.Now, responseText, response_code, response_msg,
            ".bklt_mgr_update_dbt_log");
            return responseText;
        }   
    }
}