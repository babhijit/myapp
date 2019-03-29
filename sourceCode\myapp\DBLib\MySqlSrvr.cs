using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace DBLib
{
    public sealed class MySqlSrvr : GenericServer
    {
        public MySqlSrvr()
        {
            GenericServer.s_serverInstance = this;
        }

        #region Properties

        #region String Builder "MySql Version"

        private MySqlConnectionStringBuilder conStrBldr = new MySqlConnectionStringBuilder();
        public override String ServerName
        {
            get { return conStrBldr.Server; }
            set { conStrBldr.Server = value; }
        }

        public override String UserID
        {
            get { return conStrBldr.UserID; }
            set { conStrBldr.UserID = value; }
        }

        public override String Password
        {
            get { return conStrBldr.Password; }
            set { conStrBldr.Password = value; }
        }

        public override String Database
        {
            get { return conStrBldr.Database; }
            set { conStrBldr.Database = value; }
        }

        #endregion

        #endregion

        public override IDbConnection DBConnection
        {
            get
            {
                if ((m_dbConnection == null) || (!IsOpen))
                {
                    conStrBldr.AllowUserVariables = true;
                    m_dbConnection = new MySqlConnection(conStrBldr.ToString());
                }

                return m_dbConnection;
            }
        }

        #region IDb attributes/functions

        public override IDataAdapter DataAdapter
        {
            get
            {
                return new MySqlDataAdapter();
            }
        }

        public override IDataAdapter GetIDataAdapter(IDbCommand cmd)
        {
            MySqlCommand command = cmd as MySqlCommand;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);

            return dataAdapter;
        }

        #endregion

        #region DataSet functions

        public override DataSet GetDataSetForSelectQuery(String sqlSelect, String logicalTableName)
        {
            MySqlConnection dbConnection = ServerInstance.DBConnection as MySqlConnection;
            MySqlCommand selectCmd = new MySqlCommand(sqlSelect, dbConnection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectCmd);
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(logicalTableName);
            dataAdapter.Fill(dataSet.Tables[logicalTableName]);

            return dataSet;

        }

        #endregion

        #region Utility Functions

        public override Int32 GetMaxSerialNumber(String tableName, String pkColumn = "ID")
        {
            String sqlSelect = String.Format("SELECT MAX({0}) FROM {1}", pkColumn, tableName);
            MySqlConnection dbConnection = ServerInstance.DBConnection as MySqlConnection;
            MySqlCommand selectCmd = new MySqlCommand(sqlSelect, dbConnection);
            Int32 maxSerialNumber = Convert.ToInt32(selectCmd.ExecuteScalar());

            return maxSerialNumber;
        }

        #endregion


        #region Bind Parameters By Type Functions

        public override void AddStringParameter(IDbCommand cmd, String paramName, String value)
        {
            MySqlCommand command = cmd as MySqlCommand;
            if (!String.IsNullOrEmpty(value))
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddInt32Parameter(IDbCommand cmd, String paramName, Int32? value)
        {
            MySqlCommand command = cmd as MySqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddInt16Parameter(IDbCommand cmd, String paramName, Int16? value)
        {
            MySqlCommand command = cmd as MySqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddDoubleParameter(IDbCommand cmd, String paramName, Double? value)
        {
            MySqlCommand command = cmd as MySqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddBooleanParameter(IDbCommand cmd, String paramName, Boolean? value)
        {
            MySqlCommand command = cmd as MySqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddDateTimeParameter(IDbCommand cmd, String paramName, DateTime? value)
        {
            MySqlCommand command = cmd as MySqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }


        #region Output paramaters

        public override void SetInt32OutParameter(IDbCommand cmd, String paramName)
        {
            MySqlCommand command = cmd as MySqlCommand;
            MySqlParameter outParam = command.Parameters.Add(paramName, MySqlDbType.Int32);
            outParam.Direction = ParameterDirection.Output;
        }

        public override Int32? GetInt32OutParameter(IDbCommand cmd, String paramName)
        {
            MySqlCommand command = cmd as MySqlCommand;
            return (Int32?)command.Parameters[paramName].Value;
        }

        #endregion

        #endregion

    }
}
