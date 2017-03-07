using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using AutomationOfWithdrawalBookletCharges.LIB;
using Oracle.ManagedDataAccess.Types;
using System.Web.Mvc;

namespace AutomationOfWithdrawalBookletCharges.Models
{
    public class LoginLogModel
    {
        private static string dbSchema = ConfigurationManager.AppSettings["DbSchema"];
        public static void addLoginLog(string staffid)
        {
            string logindate =  DateTime.Now.ToShortDateString();
            string logintime = DateTime.Now.ToLongTimeString();
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            connect.Open();
            OracleParameter[] parameters = new OracleParameter[3];
            parameters[0] = con.CreateInputParameter<string>("staff_id", OracleDbType.Varchar2, staffid);
            parameters[1] = con.CreateInputParameter<string>("login_date", OracleDbType.Varchar2, logindate);
            parameters[2] = con.CreateInputParameter<string>("login_time", OracleDbType.Varchar2, logintime);
           

            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema + ".bklt_mgr_log_login_users";
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
            {
                OracleParameterCollection cmdParams = command.Parameters;
                for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
            }
            try
            {
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace );
            }
        }

        public static string getLastLoginId(string staffid)
        {
            string loginId = "";
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            command = connect.CreateCommand();
            try
            {
                command.CommandText = dbSchema + ".bklt_mgr_last_loginid";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("last_id", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["last_id"].Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add("staff_id", OracleDbType.Varchar2);
                command.Parameters["staff_id"].Value = staffid;

                connect.Open();
                command.ExecuteNonQuery();
                loginId = command.Parameters["last_id"].Value.ToString();
                connect.Close();
                connect.Dispose();              
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
                loginId = null;
            }
            return loginId;
            
        }

        public static void updateLogLogout(int login_id)
        {
            var logoutdate = DateTime.Now.ToShortDateString();
            var logouttime = DateTime.Now.ToLongTimeString();
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            connect.Open();
            OracleParameter[] parameters = new OracleParameter[3];
            parameters[0] = con.CreateInputParameter<int>("login_id", OracleDbType.Int32, login_id);
            parameters[1] = con.CreateInputParameter<string>("logout_date", OracleDbType.Varchar2, logoutdate);
            parameters[2] = con.CreateInputParameter<string>("logout_time", OracleDbType.Varchar2, logouttime);

            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema + ".bklt_mgr_log_logout_users";
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
            {
                OracleParameterCollection cmdParams = command.Parameters;
                for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
            }

            try
            {
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
            }
            
        }

        public static List<LoginLogViewModel> retrieveLoginLog(string start_date, string end_date)
        {
            DateTime? MyNullableDate = null;
            ConnectionService con = new ConnectionService();
            List<LoginLogViewModel> loginLog = new List<LoginLogViewModel>();
            OracleConnection connect = con.connection();
            connect.Open();
            OracleParameter[] parameters = new OracleParameter[3];
            parameters[0] = con.CreateCursorParameter("loginlog_list");
            parameters[1] = con.CreateInputParameter<string>("start_date", OracleDbType.Varchar2, start_date);
            parameters[2] = con.CreateInputParameter<string>("end_date", OracleDbType.Varchar2, end_date);

            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema + ".bklt_mgr_search_login";
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
                loginLog.Add(new LoginLogViewModel
                {
                    LoginId = hd["LOGINID"].ToString(),
                    StaffId = hd["STAFFID"].ToString(),
                    LoginTime = hd["LOGINTIME"].ToString(),
                    LoginOutTime = hd["LOGOUTTIME"].ToString(),
                    LoginDate = (hd["LOGINDATE"].ToString() != "") ? Convert.ToDateTime(hd["LOGINDATE"].ToString()) : MyNullableDate,
                    LoginOutDate = (hd["LOGOUTDATE"].ToString() != "") ? Convert.ToDateTime(hd["LOGOUTDATE"].ToString()) : MyNullableDate,
                });

            }
            if (hd != null)
                hd.Close();
            connect.Close();
            return loginLog;

        }
    }
}