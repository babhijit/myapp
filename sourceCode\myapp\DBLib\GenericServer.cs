using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DBLib
{
    // The base class of all DB server classes (e.g. SQL Server, MySql, etc.)
    public abstract class GenericServer
    {
        protected static GenericServer s_serverInstance = null;
        public static GenericServer ServerInstance
        {
            get { return s_serverInstance; }
        }

        #region String Builder "Generic"
        // DB Connection String Builder 
        public abstract String ServerName { get; set; }
        public abstract String UserID { get; set; }
        public abstract String Password { get; set; }
        public abstract String Database { get; set; }

        #endregion

        #region Database Connection
        // DB Connection
        protected IDbConnection m_dbConnection = null;
        public abstract IDbConnection DBConnection { get; }

        public Boolean IsOpen
        {
            get
            {
                if (m_dbConnection != null)
                {
                    return (m_dbConnection.State == ConnectionState.Open);
                }

                return false;
            }
        }

        #endregion

        #region IDb attributes/functions

        public IDbCommand DbCommand
        {
            get { return ServerInstance.DBConnection.CreateCommand(); }
        }

        public abstract IDataAdapter DataAdapter { get; }

        public abstract IDataAdapter GetIDataAdapter(IDbCommand cmd);

        #endregion

        #region IDataReader static Functions

        public static IDataReader GetDataFromTable(String dataTableName, String sqlWhere)
        {
            String sqlSelect = String.Format("SELECT * FROM {0} ", dataTableName);
            if (!String.IsNullOrEmpty(sqlWhere))
                sqlSelect = String.Format("{0} WHERE {1}", sqlSelect, sqlWhere);

            return GetDataFromQuery(sqlSelect);
        }

        public static IDataReader GetDataFromQuery(String sql)
        {
            IDbCommand selectCommand = ServerInstance.DbCommand;
            selectCommand.Connection = ServerInstance.DBConnection;
            selectCommand.CommandType = CommandType.Text;
            selectCommand.CommandText = sql;
            return selectCommand.ExecuteReader();
        }

        public static String GetStringFromDataReader(IDataReader dataReader, String columnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(columnName)))
                return String.Empty;

            return (String)dataReader[columnName];
        }

        public static Int16 GetInt16FromDataReader(IDataReader dataReader, String columnName)
        {
            Int32 columnIndex = dataReader.GetOrdinal(columnName);
            if (dataReader.IsDBNull(columnIndex))
                return 0;

            return dataReader.GetInt16(columnIndex);
        }

        public static Int32 GetInt32FromDataReader(IDataReader dataReader, String columnName)
        {
            Int32 columnIndex = dataReader.GetOrdinal(columnName);
            if (dataReader.IsDBNull(columnIndex))
                return 0;

            return dataReader.GetInt32(columnIndex);
        }

        public static Boolean GetBooleanFromDataReader(IDataReader dataReader, String columnName)
        {
            Int32 columnIndex = dataReader.GetOrdinal(columnName);
            if (dataReader.IsDBNull(columnIndex))
                return false;

            return dataReader.GetBoolean(columnIndex);
        }

        public static DateTime GetDateTimeFromDataReader(IDataReader dataReader, String columnName)
        {
            Int32 columnIndex = dataReader.GetOrdinal(columnName);
            if (dataReader.IsDBNull(columnIndex))
                return DateTime.Now;

            return dataReader.GetDateTime(columnIndex);

        }

        public static Double GetDoubleFromDataReader(IDataReader dataReader, String columnName)
        {
            Int32 columnIndex = dataReader.GetOrdinal(columnName);
            if (dataReader.IsDBNull(columnIndex))
                return 0.0;

            return dataReader.GetDouble(columnIndex);
        }

        #endregion

        #region DataSet functions

        public abstract DataSet GetDataSetForSelectQuery(String sqlSelect, String logicalTableName);

        #endregion

        #region Utility Functions

        public static Int32 ExecuteSqlNonQuery(String sqlStatement)
        {
            IDbCommand command = ServerInstance.DbCommand;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlStatement;
            return command.ExecuteNonQuery();
        }

        public abstract Int32 GetMaxSerialNumber(String tableName, String pkColumn = "ID");

        /// <summary>
        /// GetStringResultList : Ensure that the columnName is present in the sqlSelect SELECT clause for it to work
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="sqlSelect"></param>
        /// <returns></returns>
        public static List<String> GetStringResultList(String columnName, String sqlSelect)
        {
            IDbCommand command = ServerInstance.DbCommand;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlSelect;
            IDataReader dataReader = command.ExecuteReader();

            List<String> results = new List<String>();
            String colValue = String.Empty;

            while (dataReader.Read())
            {
                colValue = GetStringFromDataReader(dataReader, columnName);
                if (!String.IsNullOrEmpty(colValue))
                {
                    results.Add(colValue);
                }
            }

            dataReader.Close();

            return results;
        }


        #endregion

        #region Bind Parameters By Type Functions

        public abstract void AddStringParameter(IDbCommand cmd, String paramName, String value);

        public abstract void AddInt32Parameter(IDbCommand cmd, String paramName, Int32? value);

        public abstract void AddInt16Parameter(IDbCommand cmd, String paramName, Int16? value);

        public abstract void AddDoubleParameter(IDbCommand cmd, String paramName, Double? value);

        public abstract void AddBooleanParameter(IDbCommand cmd, String paramName, Boolean? value);

        public abstract void AddDateTimeParameter(IDbCommand cmd, String paramName, DateTime? value);

        #region Output paramaters

        public abstract void SetInt32OutParameter(IDbCommand cmd, String paramName);
        public abstract Int32? GetInt32OutParameter(IDbCommand cmd, String paramName);

        #endregion

        #endregion

        #region Transaction

        public virtual IDbTransaction DBTransaction 
        {
            get
            {
                return ServerInstance.DBConnection.BeginTransaction();
            }
        }

        #endregion
        
    }
}
