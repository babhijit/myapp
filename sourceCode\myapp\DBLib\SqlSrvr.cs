using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DBLib
{
    public sealed class SqlSrvr : GenericServer
    {
        public SqlSrvr()
        {
            GenericServer.s_serverInstance = this;
        }

        #region Properties

        #region String Builder "SqlServer Version"

        private SqlConnectionStringBuilder conStrBldr = new SqlConnectionStringBuilder();
        public override String ServerName
        {
            get { return conStrBldr.DataSource; }
            set { conStrBldr.DataSource = value; }
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
            get { return conStrBldr.InitialCatalog; }
            set { conStrBldr.InitialCatalog = value; }
        }

        #endregion

        #endregion

        public override IDbConnection DBConnection
        {
            get
            {
                if ((m_dbConnection == null) || (!IsOpen))
                {
                    if(String.IsNullOrEmpty(conStrBldr.UserID))
                    {
                        conStrBldr.IntegratedSecurity = true;
                    }
                    m_dbConnection = new SqlConnection(conStrBldr.ToString());
                }

                return m_dbConnection;
            }
        }

        #region IDb attributes/functions

        public override IDataAdapter DataAdapter
        {
            get
            {
                return new SqlDataAdapter();
            }
        }

        public override IDataAdapter GetIDataAdapter(IDbCommand cmd)
        {
            SqlCommand command = cmd as SqlCommand;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            return dataAdapter;
        }

        #endregion

        #region DataSet functions

        public override DataSet GetDataSetForSelectQuery(String sqlSelect, String logicalTableName)
        {
            SqlConnection dbConnection = ServerInstance.DBConnection as SqlConnection;
            SqlCommand selectCmd = new SqlCommand(sqlSelect, dbConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd);
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
            SqlConnection dbConnection = ServerInstance.DBConnection as SqlConnection;
            SqlCommand selectCmd = new SqlCommand(sqlSelect, dbConnection);
            Int32 maxSerialNumber = Convert.ToInt32(selectCmd.ExecuteScalar());

            return maxSerialNumber;
        }

        #endregion

        #region Bind Parameters By Type Functions

        public override void AddStringParameter(IDbCommand cmd, String paramName, String value)
        {
            SqlCommand command = cmd as SqlCommand;
            if (!String.IsNullOrEmpty(value))
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddInt32Parameter(IDbCommand cmd, String paramName, Int32? value)
        {
            SqlCommand command = cmd as SqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddInt16Parameter(IDbCommand cmd, String paramName, Int16? value)
        {
            SqlCommand command = cmd as SqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddDoubleParameter(IDbCommand cmd, String paramName, Double? value)
        {
            SqlCommand command = cmd as SqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddBooleanParameter(IDbCommand cmd, String paramName, Boolean? value)
        {
            SqlCommand command = cmd as SqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }

        public override void AddDateTimeParameter(IDbCommand cmd, String paramName, DateTime? value)
        {
            SqlCommand command = cmd as SqlCommand;
            if (value.HasValue)
                command.Parameters.AddWithValue(paramName, value);
            else
                command.Parameters.AddWithValue(paramName, DBNull.Value);
        }


        #region Output paramaters

        public override void SetInt32OutParameter(IDbCommand cmd, String paramName)
        {
            SqlCommand command = cmd as SqlCommand;
            SqlParameter outParam = command.Parameters.Add(paramName, SqlDbType.Int);
            outParam.Direction = ParameterDirection.Output;
        }

        public override Int32? GetInt32OutParameter(IDbCommand cmd, String paramName)
        {
            SqlCommand command = cmd as SqlCommand;
            return (Int32?)command.Parameters[paramName].Value;
        }

        #endregion

        #endregion
    }
}
