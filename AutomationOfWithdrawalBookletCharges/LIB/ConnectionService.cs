using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using AutomationOfWithdrawalBookletCharges.ViewModel;
using Oracle.ManagedDataAccess.Types;

namespace AutomationOfWithdrawalBookletCharges.LIB
{
    public class ConnectionService
    {
        private static string dbSchema = ConfigurationManager.AppSettings["DbSchema"];
        private string conn = ConfigurationManager.ConnectionStrings["FCUBSConn"].ConnectionString;
        private OracleConnection connect;
        private OracleCommand command;

        public OracleConnection connection()
        {
            connect = new OracleConnection(conn);
            return connect;
        }

        public bool storeP(string username, string userid)
        {
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            string bval;
            connect = connection();
            command = connect.CreateCommand();
            try { 
            command.CommandText = dbSchema + ".bklt_mgr_validateadminlogin1";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
            command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;

            command.Parameters.Add("v_staffid", OracleDbType.Varchar2);
            command.Parameters["v_staffid"].Value = userid;

            command.Parameters.Add("v_username", OracleDbType.Varchar2);
            command.Parameters["v_username"].Value = username;
            connect.Open();
            command.ExecuteNonQuery();
            bval = command.Parameters["returnVal"].Value.ToString();
            connect.Close();
            connect.Dispose();
            }
            catch(Exception ex)
            {
                bval = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
            }
            if(bval == userid)
            {
                return true;
            }
            else if (bval == null)
            {
                return false;
            }
            else {
                return false;
            }
            //connection.Close();
            //connection.Dispose();

        }

        //check user status before login whether disabled or enabled
        public  string checkStat(string userid)
        {
            int RETURN_VALUE_BUFFER_SIZE = 32767;
            string bval;
            connect = connection();
            command = connect.CreateCommand();
            try
            {
                command.CommandText = dbSchema + ".bklt_mgr_getstatlogin";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("returnVal", OracleDbType.Varchar2, RETURN_VALUE_BUFFER_SIZE);
                command.Parameters["returnVal"].Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add("userid", OracleDbType.Varchar2);
                command.Parameters["userid"].Value = userid;

                connect.Open();
                command.ExecuteNonQuery();
                bval = command.Parameters["returnVal"].Value.ToString();
            }
            catch(Exception ex)
            {
                bval = null;
                ErrorLogs.log(ex.Message + "+ -------------------------------- + " + ex.StackTrace);
            }
            connect.Close();
            connect.Dispose();
            return bval;

        }

       

        public  OracleParameter CreateCursorParameter(string name)
        {
            OracleParameter prm = new OracleParameter(name, OracleDbType.RefCursor);
            prm.Direction = ParameterDirection.ReturnValue;
            return prm;
        }
        public  OracleParameter CreateInputParameter<N>(string name, OracleDbType dbType, N value)
        {
            OracleParameter parameter = new OracleParameter();
            parameter.ParameterName = name;
            parameter.OracleDbType = dbType;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = value;
            return parameter;
        }

        public  OracleParameter ReturnValueParameter<N>(string name, OracleDbType dbType, N value)
        {
            OracleParameter parameter = new OracleParameter();
            parameter.ParameterName = name;
            parameter.OracleDbType = dbType;
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.Value = value;
            return parameter;
        }
   
    }
}
