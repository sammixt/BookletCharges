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
    public class UsersRole
    {
        private static string dbSchema = ConfigurationManager.AppSettings["DbSchema"];
        public static List<RolesViewModel> getLoginUsersRole(string userid)
        {

            ConnectionService con = new ConnectionService();
            List<RolesViewModel> loginRole = new List<RolesViewModel>();
            OracleConnection connect = con.connection();
            OracleDataReader hd = null;
            connect.Open();
            OracleParameter[] parameters = new OracleParameter[2];
            parameters[0] = con.CreateCursorParameter("roles_list");
            parameters[1] = con.CreateInputParameter<string>("userid", OracleDbType.Varchar2, userid);

            OracleCommand command = connect.CreateCommand();
            command.CommandText = dbSchema + ".bklt_mgr_select_user_roles";
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
            {
                OracleParameterCollection cmdParams = command.Parameters;
                for (int i = 0; i < parameters.Length; i++) { cmdParams.Add(parameters[i]); }
            }
            try { 
                command.ExecuteNonQuery();

                OracleRefCursor r = (OracleRefCursor)parameters[0].Value;
                if (r != null)
                {
                    hd = r.GetDataReader();
                }

                decimal row_id = 0;
                while (hd.Read())
                {
                    row_id++;

                    loginRole.Add(new RolesViewModel
                    {   //the roleId or rolename will be compared before granting access.
                        RoleId = Convert.ToInt32(hd["roleid"]),
                        RoleName = Convert.ToString(hd["rolename"])
                    });

                }
                if (hd != null)
                    hd.Close();
                connect.Close();
            }
            catch (Exception ex)
            {
                loginRole = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace + loginRole);
            }
            
            return loginRole;

        }
    }
}