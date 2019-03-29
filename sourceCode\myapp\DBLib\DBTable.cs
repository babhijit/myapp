using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utility;

namespace DBLib
{
    public abstract class DBTable
    {
        #region Members

        // Provision to allow binding to different database
        private GenericServer m_defaultServer = null;
        protected GenericServer Server
        {
            get
            {
                if (m_defaultServer == null)
                {
                    m_defaultServer = GenericServer.ServerInstance;
                }

                return m_defaultServer;
            }

            set
            {
                m_defaultServer = value;
            }
        }

        protected IDataReader m_dataReader = null;

        protected Boolean m_dirty = false;
        public Boolean IsDirty { get { return m_dirty; } }

        protected Int32 m_PK = 0;
        public Int32 PK
        {
            get { return m_PK; }
            set
            {
                // check for change of value is removed
                // even though this is going to give us a performance hit
                // there is a fundamental flaw in one of our classes that allows
                // the designer to force 0 values for some properties.
                m_PK = value;
                if (m_PK != 0)
                {
                    FetchRecord();
                }
            }
        }

        protected String m_PKName;
        protected String m_TableName;
        protected String m_InsertUpdateSPName;

        #endregion

        public DBTable(String tableName, String pkName, String insUpdSPName, GenericServer defaultServer = null)
        {
            m_TableName = tableName;
            m_PKName = pkName;
            m_InsertUpdateSPName = insUpdSPName;
            m_defaultServer = defaultServer;
        }

        #region "Database Operations"

        protected void CloseDataReader()
        {
            if (m_dataReader != null)
            {
                if (!m_dataReader.IsClosed)
                    m_dataReader.Close();
            }
        }

        protected virtual void FetchRecord()
        {
            CloseDataReader();

            if (m_PK == 0)
            {
                m_dataReader = null;
                return;
            }

            m_dirty = false;

            String sqlSelect = String.Format("SELECT * FROM {0} WHERE {1} = {2}", m_TableName, m_PKName, PK);

            IDbCommand selectCommand = Server.DbCommand;
            selectCommand.Connection = Server.DBConnection;
            selectCommand.CommandType = CommandType.Text;
            selectCommand.CommandText = sqlSelect;
            m_dataReader = selectCommand.ExecuteReader();

            Boolean hasRecord = m_dataReader.Read();

            if (hasRecord)
            {
                GetData();
            }

            m_dataReader.Close();

            if (!hasRecord)
                throw new MyAppRecordNotFound(String.Format("Record Not Found for {0} in {1}", m_PK, m_TableName));

        }

        /// <summary>
        /// Delete: Deletes the record specified by PK
        /// Throws exception if m_PK is 0
        /// </summary>
        /// <returns></returns>
        public virtual Int32 Delete()
        {
            if (m_PK == 0)
                throw new MyAppRecordNotFound();

            CloseDataReader();

            String sqlDelete = String.Format("DELETE FROM {0} WHERE {1} = {2}", m_TableName, m_PKName, PK);

            IDbCommand command = Server.DbCommand;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlDelete;

            return command.ExecuteNonQuery();
        }

        protected abstract void GetData();

        public virtual void Save()
        {
            OnPreSave();

            if (!m_dirty)
            {
                return;
            }

            Boolean addNew = (m_PK == 0);

            IDbCommand storedProcCmd = Server.DbCommand;
            storedProcCmd.CommandType = CommandType.StoredProcedure;
            storedProcCmd.CommandText = m_InsertUpdateSPName;

            SetupSPParams(storedProcCmd);

            Server.SetInt32OutParameter(storedProcCmd, DBConstants.SP_INSERT_OUT_INSERTEDID);

            storedProcCmd.ExecuteNonQuery();


            if (addNew)
            {
                Int32? insertedID = Server.GetInt32OutParameter(storedProcCmd, DBConstants.SP_INSERT_OUT_INSERTEDID);
                if (insertedID.HasValue)
                    m_PK = insertedID.Value;
                else
                    throw new MyAppException(String.Format("Insertion failed for {0}", m_TableName));
            }

            OnPostSave();
        }

        protected abstract void SetupSPParams(IDbCommand storedProcCmd);

        protected virtual void OnPreSave() { }

        protected virtual void OnPostSave() { }

        #endregion

    }
}
