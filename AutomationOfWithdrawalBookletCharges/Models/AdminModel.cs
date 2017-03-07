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
    public class AdminModel
    {
        private static string dbSchema = ConfigurationManager.AppSettings["DbSchema"];

        public List<UserProfileViewModel> retrieveUsers(string branchCode)
        {

            ConnectionService con = new ConnectionService();
            List<UserProfileViewModel> userInfo = new List<UserProfileViewModel>();
            OracleConnection connect = con.connection();
            connect.Open();
            OracleParameter[] parameters = new OracleParameter[2];
            parameters[0] = con.CreateCursorParameter("returnVal");
            parameters[1] = con.CreateInputParameter<string>("branch_code", OracleDbType.Varchar2, branchCode);

            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema+".bklt_mgr_select_all_users";
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
                userInfo.Add(new UserProfileViewModel
                {
                    EmployeeNumber = hd["staffid"].ToString(),
                    FullName = hd["staffname"].ToString(),
                    Email = hd["email"].ToString(),
                    Branch = hd["branchname"].ToString(),
                    BranchCode = hd["branchcode"].ToString(),
                    Title = hd["role_desc"].ToString(),
                    Roles = hd["rolename"].ToString(),
                    Status = hd["status"].ToString()
                });

            }
            if (hd != null)
                hd.Close();
            connect.Close();
            return userInfo;

        }

        //retrieve users for super admin view.
        public List<UserProfileViewModel> retrieveUsersAdmin()
        {

            ConnectionService con = new ConnectionService();
            List<UserProfileViewModel> userInfo = new List<UserProfileViewModel>();
            OracleConnection connect = con.connection();
            connect.Open();
            OracleParameter[] parameters = new OracleParameter[1];
            parameters[0] = con.CreateCursorParameter("users_list");

            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema + ".bklt_mgr_select_all_users_a";
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
                userInfo.Add(new UserProfileViewModel
                {
                    EmployeeNumber = hd["staffid"].ToString(),
                    FullName = hd["staffname"].ToString(),
                    Email = hd["email"].ToString(),
                    Branch = hd["branchname"].ToString(),
                    BranchCode = hd["branchcode"].ToString(),
                    Title = hd["role_desc"].ToString(),
                    Roles = hd["rolename"].ToString(),
                    Status = hd["status"].ToString()
                });

            }
            if (hd != null)
                hd.Close();
            connect.Close();
            return userInfo;

        }

        public List<UserProfileViewModel> retrieveToEditUsers(string userid)
        {

            ConnectionService con = new ConnectionService();
            List<UserProfileViewModel> userInfo = new List<UserProfileViewModel>();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[2];
                parameters[0] = con.CreateCursorParameter("returnVal");
                parameters[1] = con.CreateInputParameter<string>("userid", OracleDbType.Varchar2, userid);

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_select_edit_users";
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
                    userInfo.Add(new UserProfileViewModel
                    {
                        EmployeeNumber = hd["staffid"].ToString(),
                        FullName = hd["staffname"].ToString(),
                        Email = hd["email"].ToString(),
                        Title = hd["role_desc"].ToString(),
                        Roles = hd["rolename"].ToString(),
                        Status = hd["status"].ToString(),
                        UserName = hd["username"].ToString()

                    });

                }
                if (hd != null)
                    hd.Close();
                connect.Close();
                return userInfo;
            }catch(Exception ex)
            {
                userInfo = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + userInfo);
                return userInfo;
            }
        }

        public static List<SelectListItem> populateRole()
        {

            ConnectionService con = new ConnectionService();
            List<SelectListItem> userRole = new List<SelectListItem>();
            OracleConnection connect = con.connection();
            connect.Open();
            try
            {
                OracleParameter[] parameters = new OracleParameter[1];
                parameters[0] = con.CreateCursorParameter("roles_list");

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_select_all_roles";
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

                    userRole.Add(new SelectListItem
                    {
                        Value = Convert.ToString(hd["roleid"]),
                        Text = "(" + hd["rolename"].ToString() + ")-" + hd["role_desc"].ToString()
                    });

                }
                if (hd != null)
                    hd.Close();
                connect.Close();
                return userRole;
            }
            catch(Exception ex)
            {
                userRole = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + userRole);
                return userRole;
            }
            

        }

        //Check if user exist on the database
        public static bool chkUserExist(string username)
        {
            var profile = AuthenticationService.GetUserProfile(username);
            string userid = profile.EmployeeNumber ;
            string branchid = profile.BranchCode;
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            string bval;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            command = connect.CreateCommand();
            try
            {
                command.CommandText = dbSchema + ".bklt_mgr_chk_user_exist";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add("userid", OracleDbType.Varchar2);
                command.Parameters["userid"].Value = userid;

                command.Parameters.Add("branchid", OracleDbType.Varchar2);
                command.Parameters["branchid"].Value = branchid;
                connect.Open();
                command.ExecuteNonQuery();
                bval = command.Parameters["returnVal"].Value.ToString();
                var outcome = String.Equals(bval, "1");

                bool retVal = (outcome == true ? outcome : false);
                connect.Close();
                connect.Dispose();
                return retVal;
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
                return false;
            }
            
            //connection.Close();
            //connection.Dispose();

        }

        //Insert users record into the database
        public static void AddUserRecord(string username, int roleid)
        {
            var profile = AuthenticationService.GetUserProfile(username);

            ConnectionService con = new ConnectionService();
            List<UserProfileViewModel> userInfo = new List<UserProfileViewModel>();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[8];
                parameters[0] = con.CreateInputParameter<string>("userid", OracleDbType.Varchar2, profile.EmployeeNumber);
                parameters[1] = con.CreateInputParameter<int>("user_role", OracleDbType.Int32, roleid);
                parameters[2] = con.CreateInputParameter<string>("staff_name", OracleDbType.Varchar2, profile.FullName);
                parameters[3] = con.CreateInputParameter<string>("user_name", OracleDbType.Varchar2, profile.UserName);
                parameters[4] = con.CreateInputParameter<string>("user_email", OracleDbType.Varchar2, profile.Email);
                parameters[5] = con.CreateInputParameter<string>("branch_code", OracleDbType.Varchar2, profile.BranchCode);
                parameters[6] = con.CreateInputParameter<string>("branch_name", OracleDbType.Varchar2, profile.Branch);
                parameters[7] = con.CreateInputParameter<string>("user_status", OracleDbType.Varchar2, "ENABLED");

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_insert_user";
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    OracleParameterCollection cmdParams = command.Parameters;
                    for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
                }
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
            }
        }

        //Update users status in the database
        public static void updateUserStat(string userid, string status_val)
        {
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[2];
                parameters[0] = con.CreateInputParameter<string>("userid", OracleDbType.Varchar2, userid);
                parameters[1] = con.CreateInputParameter<string>("status_val", OracleDbType.Varchar2, status_val);

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_update_user_stat";
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    OracleParameterCollection cmdParams = command.Parameters;
                    for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
                }
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
            }
        }

        //check if user title match the role or exist before creation
        public static bool compareTitle(string Title, string username)
        {
            var profile = AuthenticationService.GetUserProfile(username);
            bool compareUserTitle = String.Equals(Title,profile.Title);
            if(compareUserTitle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //check if Branch matches the users branch.
        public static bool compareBranch(string branch, string username)
        {
            var profile = AuthenticationService.GetUserProfile(username);
            bool compareUserBranch = String.Equals(branch, profile.BranchCode);
            if (compareUserBranch)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //select role from user role table to compare the selected role of the user been edited.
        public static bool compareEditedUserRole(int roleid, string username)
        {
            var profile = AuthenticationService.GetUserProfile(username);
            string roleTitle = profile.Title;
            string branchid = profile.BranchCode;
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            string bval;
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            OracleCommand command = connect.CreateCommand();
            command = connect.CreateCommand();
            try
            {
                command.CommandText = dbSchema + ".bklt_mgr_sel_rol_edit_users";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add("role_id", OracleDbType.Int32);
                command.Parameters["role_id"].Value = roleid;

                connect.Open();
                command.ExecuteNonQuery();
                bval = command.Parameters["returnVal"].Value.ToString();

                var outcome = String.Equals(bval, roleTitle);

                bool retVal = (outcome == true ? outcome : false);
                connect.Close();
                connect.Dispose();
                return retVal;
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
                return false;
            }

            //connection.Close();
            //connection.Dispose();
        }

        //select role from user role table to compare the selected role of the user been edited.
        public static void updateUserRole(int roleid, string username)
        {
            ConnectionService con = new ConnectionService();
            OracleConnection connect = con.connection();
            try
            {
                connect.Open();
                OracleParameter[] parameters = new OracleParameter[2];
                parameters[0] = con.CreateInputParameter<string>("userid", OracleDbType.Varchar2, username);
                parameters[1] = con.CreateInputParameter<int>("role_id", OracleDbType.Int32, roleid);

                OracleCommand command = connect.CreateCommand();
                command.CommandText = dbSchema + ".bklt_mgr_update_user_role";
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    OracleParameterCollection cmdParams = command.Parameters;
                    for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
                }
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch(Exception ex)
            {
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
            }
        }
    }
}