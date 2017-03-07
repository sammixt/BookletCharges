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
    public class CustomerModel
    {
        private static string dbSchema = ConfigurationManager.AppSettings["DbSchema"];

        public static DateTime? lastIssueDate(string account)
        {
            
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            DateTime? bval;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            command = connect.CreateCommand();
            try
            {
                command.CommandText = dbSchema + ".bklt_mgr_last_issue";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("date_approved", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["date_approved"].Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add("account_num", OracleDbType.Varchar2);
                command.Parameters["account_num"].Value = account;

                connect.Open();
                command.ExecuteNonQuery();
                if (command.Parameters["date_approved"].Value.ToString() != null)
                {
                    bval = Convert.ToDateTime(command.Parameters["date_approved"].Value.ToString());
                }
                else { bval = null; }
                connect.Close();
                connect.Dispose();
                return bval;
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace );
                return null;
            }


        }

        //Insert initial request from ssa


        public static string AddRecord(CustomerViewModel model, string empNumber, int login_id,string branch_code)
        {
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            connect.Open();
            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema + ".bklt_mgr_insert_request";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
            command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add("acct_name", OracleDbType.Varchar2);
            command.Parameters["acct_name"].Value = model.AccountName;
            command.Parameters.Add("acct_number", OracleDbType.Varchar2);
            command.Parameters["acct_number"].Value = model.AccountNumber;
            command.Parameters.Add("acct_balance", OracleDbType.Decimal);
            command.Parameters["acct_balance"].Value = model.AccountBalance;
            command.Parameters.Add("ssa_id", OracleDbType.Varchar2);
            command.Parameters["ssa_id"].Value = empNumber;
            command.Parameters.Add("ssa_loginid", OracleDbType.Int32);
            command.Parameters["ssa_loginid"].Value = login_id;
            command.Parameters.Add("sn_no_strt", OracleDbType.Varchar2);
            command.Parameters["sn_no_strt"].Value = model.SerialNoStart;
            command.Parameters.Add("sn_no_end", OracleDbType.Varchar2);
            command.Parameters["sn_no_end"].Value = model.SerialNoEnd;
            command.Parameters.Add("req_date", OracleDbType.Varchar2);
            command.Parameters["req_date"].Value = DateTime.Now.ToShortDateString();
            command.Parameters.Add("req_time", OracleDbType.Varchar2);
            command.Parameters["req_time"].Value = model.RequestCreationTime;
            command.Parameters.Add("req_status", OracleDbType.Varchar2);
            command.Parameters["req_status"].Value = "PENDING";
            command.Parameters.Add("branch_code", OracleDbType.Varchar2);
            command.Parameters["branch_code"].Value = branch_code;
            command.Parameters.Add("phone_no", OracleDbType.Varchar2);
            command.Parameters["phone_no"].Value = model.PhoneNo;
            try
            {
                command.ExecuteNonQuery();
                string bval = command.Parameters["returnVal"].Value.ToString();
                connect.Close();
                return bval;
            }
            catch (Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
                return null;
            }
        }

        //used to search request by hssa and ssa
        public List<CustomerViewModel> searchRequest(string start_date, string end_date, string branch_code, string reqst_status)
        {
             
            ConnectionService con = new ConnectionService();
            List<CustomerViewModel> userInfo = new List<CustomerViewModel>();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[5];
                parameters[0] = con.CreateCursorParameter("request_list");
                parameters[1] = con.CreateInputParameter<string>("start_date", OracleDbType.Varchar2, start_date);
                parameters[2] = con.CreateInputParameter<string>("end_date", OracleDbType.Varchar2, end_date);
                parameters[3] = con.CreateInputParameter<string>("branch_code", OracleDbType.Varchar2, branch_code);
                parameters[4] = con.CreateInputParameter<string>("reqst_status", OracleDbType.Varchar2, reqst_status);

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_search_reqst";
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

                decimal row_id = 0;
                while (hd.Read())
                {
                    row_id++;
                    userInfo.Add(new CustomerViewModel
                    {
                        RequestId = hd["REQUESTID"].ToString(),
                        AccountName = hd["ACCOUNTNAME"].ToString(),
                        AccountNumber = hd["ACCOUNTNUMBER"].ToString(),
                        SsaName = hd["STAFFNAME"].ToString(),
                        Status = hd["STATUS"].ToString(),
                        RequestCreationDate = Convert.ToDateTime(hd["REQUESTCREATIONDATE"].ToString()),
                        RequestCreationTime = hd["REQUESTCREATIONTIME"].ToString()
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

        //Hssa retrieving customers information
        public List<CustomerViewModel> displayRequestDetails(string reqid, string branch_code)
        {
            int id = Convert.ToInt32(reqid);
            ConnectionService con = new ConnectionService();
            List<CustomerViewModel> userInfo = new List<CustomerViewModel>();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[3];
                parameters[0] = con.CreateCursorParameter("request_list");
                parameters[1] = con.CreateInputParameter<int>("req_id", OracleDbType.Int32, id);
                parameters[2] = con.CreateInputParameter<string>("branch_code", OracleDbType.Varchar2, branch_code);


                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_reqst_details";
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

                decimal row_id = 0;
                while (hd.Read())
                {
                    row_id++;
                    userInfo.Add(new CustomerViewModel
                    {
                        RequestId = hd["REQUESTID"].ToString(),
                        AccountName = hd["ACCOUNTNAME"].ToString(),
                        AccountNumber = hd["ACCOUNTNUMBER"].ToString(),
                        AccountBalance = Convert.ToDecimal(hd["ACCOUNTBALANCE"].ToString()),
                        SsaId = hd["SSAID"].ToString(),
                        SerialNoStart = hd["SERIALNUMBERSTART"].ToString(),
                        SerialNoEnd = hd["SERIALNUMBEREND"].ToString(),
                        RequestCreationDate = Convert.ToDateTime(hd["REQUESTCREATIONDATE"].ToString()),
                        RequestCreationTime = hd["REQUESTCREATIONTIME"].ToString(),
                        // RequestAuthorizationDate = Convert.ToDateTime(X),
                        RequestAuthorizationDate = (hd["REQUESTAUTORIZATIONDATE"] != null) ? hd["REQUESTAUTORIZATIONDATE"].ToString() : null,
                        RequestAuthorizationTime = hd["REQUESTAUTORIZATIONTIME"].ToString(),
                        Status = hd["STATUS"].ToString(),
                        PhoneNo = hd["PHONENO"].ToString(),
                        Comment = hd["HSSACOMMENT"].ToString(),
                        DateOfLastIssue = lastIssueDate(hd["ACCOUNTNUMBER"].ToString())
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
        //hssa updates request record.
        public static string UpdateRecord(CustomerViewModel model, string hssastaff_id, int hssalogin_id, string branch_code, string status)
    
        {
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_update_request";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add("reqst_id", OracleDbType.Int32);
                command.Parameters["reqst_id"].Value = Convert.ToInt32(model.RequestId);
                command.Parameters.Add("ssa_id", OracleDbType.Varchar2);
                command.Parameters["ssa_id"].Value = model.SsaId;
                command.Parameters.Add("req_auth_date", OracleDbType.Varchar2);
                command.Parameters["req_auth_date"].Value = model.RequestAuthorizationDate;
                command.Parameters.Add("req_auth_time", OracleDbType.Varchar2);
                command.Parameters["req_auth_time"].Value = model.RequestAuthorizationTime;
                command.Parameters.Add("req_status", OracleDbType.Varchar2);
                command.Parameters["req_status"].Value = status;
                command.Parameters.Add("hssalogin_id", OracleDbType.Int32);
                command.Parameters["hssalogin_id"].Value = hssalogin_id;
                command.Parameters.Add("hssastaff_id", OracleDbType.Varchar2);
                command.Parameters["hssastaff_id"].Value = hssastaff_id;
                command.Parameters.Add("branch_code", OracleDbType.Varchar2);
                command.Parameters["branch_code"].Value = branch_code;
                command.Parameters.Add("hssa_comment", OracleDbType.Varchar2);
                command.Parameters["hssa_comment"].Value = model.Comment;
                command.ExecuteNonQuery();
                string bval = command.Parameters["returnVal"].Value.ToString();
                connect.Close();
                return bval;
            }
            catch (Exception ex)
            {
                string bval = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + bval);
                return bval;
            }
        }

        //retrieves count of pending request for display on checkers page
        public static string countRequest(string branch_code, string status)
        {
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_count_reqstat";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add("branch_code", OracleDbType.Varchar2);
                command.Parameters["branch_code"].Value = branch_code;
                command.Parameters.Add("stats", OracleDbType.Varchar2);
                command.Parameters["stats"].Value = status;
                command.ExecuteNonQuery();
                string bval = command.Parameters["returnVal"].Value.ToString();
                connect.Close();
                return bval;
            }
            catch (Exception ex)
            {
                string bval = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + bval);
                return bval;
            }
        }

        //used to generate report
        public List<CustomerViewModel> searchReport(string start_date, string end_date, string branch_code, string reqst_status)
        {
            ConnectionService con = new ConnectionService();
            List<CustomerViewModel> userInfo = new List<CustomerViewModel>();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[5];
                parameters[0] = con.CreateCursorParameter("report_list");
                parameters[1] = con.CreateInputParameter<string>("branch_code", OracleDbType.Varchar2, branch_code);
                parameters[2] = con.CreateInputParameter<string>("stats", OracleDbType.Varchar2, reqst_status);
                parameters[3] = con.CreateInputParameter<string>("start_date", OracleDbType.Varchar2, start_date);
                parameters[4] = con.CreateInputParameter<string>("end_date", OracleDbType.Varchar2, end_date);

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_report";
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

                decimal row_id = 0;
                while (hd.Read())
                {
                    row_id++;
                    userInfo.Add(new CustomerViewModel
                    {
                        RequestCreationDate = Convert.ToDateTime(hd["REQUESTCREATIONDATE"].ToString()),
                        RequestId = hd["requestcreationdate"].ToString(),
                        AccountName = hd["accountname"].ToString(),
                        AccountNumber = hd["accountnumber"].ToString(),
                        DateOfLastIssue = Convert.ToDateTime(hd["issuedate"].ToString()),
                        SerialNoStart = hd["serialnumberstart"].ToString(),
                        SerialNoEnd = hd["serialnumberend"].ToString(),
                        SsaId = hd["ssaid"].ToString(),
                        HssaId = hd["hssaid"].ToString(),
                        RequestCreationTime = hd["REQUESTCREATIONTIME"].ToString(),
                        RequestAuthorizationTime = hd["REQUESTAUTORIZATIONTIME"].ToString()
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

        //used to retrieve ssa name/email customer details for sending mail to the ssa from hssa
        public static List<CustomerViewModel> sentToSsaParams(CustomerViewModel model)
        {

            ConnectionService con = new ConnectionService();
            List<CustomerViewModel> userInfo = new List<CustomerViewModel>();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[2];
                parameters[0] = con.CreateCursorParameter("request_list");
                parameters[1] = con.CreateInputParameter<int>("req_id", OracleDbType.Int32, Convert.ToInt32(model.RequestId));
                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_ssa_cust_det";
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

                decimal row_id = 0;
                while (hd.Read())
                {
                    row_id++;
                    userInfo.Add(new CustomerViewModel
                    {
                        AccountName = hd["accountname"].ToString(),
                        AccountNumber = hd["accountnumber"].ToString(),
                        SsaName = hd["staffname"].ToString(),
                        Email = hd["email"].ToString(),
                        Comment = hd["hssacomment"].ToString()
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


    }
}