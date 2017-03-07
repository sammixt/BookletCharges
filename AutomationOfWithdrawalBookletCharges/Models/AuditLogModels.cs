using AutomationOfWithdrawalBookletCharges.LIB;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AutomationOfWithdrawalBookletCharges.Models
{
    public class AuditLogModels
    {
        private static string dbSchema = ConfigurationManager.AppSettings["DbSchema"];
        public static List<CustomerViewModel> displayRequestLog(string start_date=null, string end_date=null, string reqid = null)
        {
           
            ConnectionService con = new ConnectionService();
            List<CustomerViewModel> userInfo = new List<CustomerViewModel>();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            OracleParameter[] parameters;
            try
            {
                connect.Open();
                if (reqid == null) { 
                parameters = new OracleParameter[3];
                parameters[0] = con.CreateCursorParameter("request_list");
                parameters[1] = con.CreateInputParameter<string>("start_date", OracleDbType.Varchar2, start_date);
                parameters[2] = con.CreateInputParameter<string>("end_date", OracleDbType.Varchar2, end_date);               
                command.CommandText = dbSchema + ".bklt_mgr_search_reqlog";
                }
                else
                {
                    parameters = new OracleParameter[2];
                    int id = Convert.ToInt32(reqid);
                    parameters[0] = con.CreateCursorParameter("request_list");
                    parameters[1] = con.CreateInputParameter<int>("start_date", OracleDbType.Int32, id);
                    command.CommandText = dbSchema + ".bklt_mgr_search_reqlogid";
                }
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    OracleParameterCollection cmdParams = command.Parameters;
                    for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
                }

                command.ExecuteNonQuery();

                OracleRefCursor r = (OracleRefCursor)parameters[0].Value;
                OracleDataReader hd = null;
                if (r != null)
                {
                    hd = r.GetDataReader();
                }

                //decimal row_id = 0;
                while (hd.Read())
                {
                    //row_id++;
                    userInfo.Add(new CustomerViewModel
                    {
                        RequestId = hd["REQUESTID"].ToString(),
                        AccountName = hd["ACCOUNTNAME"].ToString(),
                        AccountNumber = hd["ACCOUNTNUMBER"].ToString(),
                        SsaId = hd["SSAID"].ToString(),
                        HssaId = hd["hssaid"].ToString(),
                        SerialNoStart = hd["SERIALNUMBERSTART"].ToString(),
                        SerialNoEnd = hd["SERIALNUMBEREND"].ToString(),
                        RequestCreationDate = Convert.ToDateTime(hd["REQUESTCREATIONDATE"].ToString()),
                        RequestCreationTime = hd["REQUESTCREATIONTIME"].ToString(),
                        // RequestAuthorizationDate = Convert.ToDateTime(X),
                        RequestAuthorizationDate = (hd["REQUESTAUTORIZATIONDATE"] != null) ? hd["REQUESTAUTORIZATIONDATE"].ToString() : null,
                        RequestAuthorizationTime = hd["REQUESTAUTORIZATIONTIME"].ToString(),
                        Status = hd["STATUS"].ToString(),
                        Comment = hd["HSSACOMMENT"].ToString(),
                        
                    });

                }
                if (hd != null)
                    hd.Close();
                connect.Close();
                return userInfo;
            }
            catch (Exception ex)
            {
                userInfo = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + userInfo);
                return userInfo;
            }

        }

        public static List<PostingViewModel> displayPostingLogCr(string start_date = null, string end_date = null, string reqid = null)
        {

            ConnectionService con = new ConnectionService();
            List<PostingViewModel> postingInfo = new List<PostingViewModel>();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            OracleParameter[] parameters;
            try
            {
                connect.Open();
                if (reqid == null)
                {
                    parameters = new OracleParameter[3];
                    parameters[0] = con.CreateCursorParameter("request_list");
                    parameters[1] = con.CreateInputParameter<string>("start_date", OracleDbType.Varchar2, start_date);
                    parameters[2] = con.CreateInputParameter<string>("end_date", OracleDbType.Varchar2, end_date);
                    command.CommandText = dbSchema + ".bklt_mgr_search_credlog";
                }
                else
                {
                    parameters = new OracleParameter[2];
                    int id = Convert.ToInt32(reqid);
                    parameters[0] = con.CreateCursorParameter("request_list");
                    parameters[1] = con.CreateInputParameter<int>("start_date", OracleDbType.Int32, id);
                    command.CommandText = dbSchema + ".bklt_mgr_search_credlogid";
                }
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    OracleParameterCollection cmdParams = command.Parameters;
                    for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
                }

                command.ExecuteNonQuery();

                OracleRefCursor r = (OracleRefCursor)parameters[0].Value;
                OracleDataReader hd = null;
                if (r != null)
                {
                    hd = r.GetDataReader();
                }

                //decimal row_id = 0;
                while (hd.Read())
                {
                    //row_id++;
                    postingInfo.Add(new PostingViewModel
                    {
                        transactionId = hd["REQUESTID"].ToString(),
                        paymentReference = hd["PAYMENTREF"].ToString(),
                        accountNumber = hd["ACCOUNTNUMBER"].ToString(),
                        amount = Convert.ToDecimal(hd["CREDIT"].ToString()),
                        branchCode = hd["INITIATINGBRANCH"].ToString(),
                        glCasaIndicator = hd["GLCASAINDICATOR"].ToString(),
                        narration = hd["NARRATION"].ToString(),
                        transactionDate = hd["TRANSACTIONSTARTDATE"].ToString(),
                        currency = hd["CURRENCY"].ToString(),
                        // RequestAuthorizationDate = Convert.ToDateTime(X),
                        status = hd["STATUS"].ToString(),
                        responseCode = hd["RESPONSECODE"].ToString(),
                        responseMsg = hd["RESPONSEMSG"].ToString()                       
                    });

                }
                if (hd != null)
                    hd.Close();
                connect.Close();
                return postingInfo;
            }
            catch (Exception ex)
            {
                postingInfo = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + postingInfo);
                return postingInfo;
            }

        }

        public static List<PostingViewModel> displayPostingLogDr(string start_date = null, string end_date = null, string reqid = null)
        {

            ConnectionService con = new ConnectionService();
            List<PostingViewModel> postingInfo = new List<PostingViewModel>();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            OracleParameter[] parameters;
            try
            {
                connect.Open();
                if (reqid == null)
                {
                    parameters = new OracleParameter[3];
                    parameters[0] = con.CreateCursorParameter("request_list");
                    parameters[1] = con.CreateInputParameter<string>("start_date", OracleDbType.Varchar2, start_date);
                    parameters[2] = con.CreateInputParameter<string>("end_date", OracleDbType.Varchar2, end_date);
                    command.CommandText = dbSchema + ".bklt_mgr_search_debitlog";
                }
                else
                {
                    parameters = new OracleParameter[2];
                    int id = Convert.ToInt32(reqid);
                    parameters[0] = con.CreateCursorParameter("request_list");
                    parameters[1] = con.CreateInputParameter<int>("start_date", OracleDbType.Int32, id);
                    command.CommandText = dbSchema + ".bklt_mgr_search_debitlogid";
                }
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    OracleParameterCollection cmdParams = command.Parameters;
                    for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
                }

                command.ExecuteNonQuery();

                OracleRefCursor r = (OracleRefCursor)parameters[0].Value;
                OracleDataReader hd = null;
                if (r != null)
                {
                    hd = r.GetDataReader();
                }

                //decimal row_id = 0;
                while (hd.Read())
                {
                    //row_id++;
                    postingInfo.Add(new PostingViewModel
                    {
                        transactionId = hd["REQUESTID"].ToString(),
                        paymentReference = hd["PAYMENTREF"].ToString(),
                        accountNumber = hd["ACCOUNTNUMBER"].ToString(),
                        amount = Convert.ToDecimal(hd["DEBIT"].ToString()),
                        branchCode = hd["INITIATINGBRANCH"].ToString(),
                        glCasaIndicator = hd["GLCASAINDICATOR"].ToString(),
                        narration = hd["NARRATION"].ToString(),
                        transactionDate = hd["TRANSACTIONSTARTDATE"].ToString(),
                        currency = hd["CURRENCY"].ToString(),
                        // RequestAuthorizationDate = Convert.ToDateTime(X),
                        status = hd["STATUS"].ToString(),
                        responseCode = hd["RESPONSECODE"].ToString(),
                        responseMsg = hd["RESPONSEMSG"].ToString()
                    });

                }
                if (hd != null)
                    hd.Close();
                connect.Close();
                return postingInfo;
            }
            catch (Exception ex)
            {
                postingInfo = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + postingInfo);
                return postingInfo;
            }

        }
    }
}