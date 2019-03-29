using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib;

namespace MyAppDBLib
{
    public class DBStudent : DBTable
    {
        public DBStudent() : base(MyAppDBConstants.TABLE_STUDENT, MyAppDBConstants.STUDENT_PK, MyAppDBConstants.SP_STUDENT_INSERTUPDATE) { }

        #region Columns

        private Int32 m_Age = 0;
        public Int32 Age
        {
            get { return m_Age; }
            set
            {
                if (m_Age != value)
                {
                    m_Age = value;
                    m_dirty = true;
                }
            }
        }

        private String m_Name = String.Empty;
        public String Name
        {
            get { return m_Name; }
            set
            {
                if (!m_Name.Equals(value))
                {
                    m_Name = value;
                    m_dirty = true;
                }
            }
        }


        private String m_Standard = String.Empty;
        public String Standard
        {
            get { return m_Standard; }
            set
            {
                if (!m_Standard.Equals(value))
                {
                    m_Standard = value;
                    m_dirty = true;
                }
            }
        }
         
        #endregion

        protected override void GetData()
        {
            m_Name = GenericServer.GetStringFromDataReader(m_dataReader, MyAppDBConstants.STUDENT_NAME);
            m_Age = GenericServer.GetInt32FromDataReader(m_dataReader, MyAppDBConstants.STUDENT_AGE);
            m_Standard = GenericServer.GetStringFromDataReader(m_dataReader, MyAppDBConstants.STUDENT_STD);
        }

        protected override void SetupSPParams(System.Data.IDbCommand storedProcCmd)
        {
            GenericServer.ServerInstance.AddInt32Parameter(storedProcCmd, MyAppDBConstants.SP_STUDENT_IN_STUDENT_ID, m_PK);
            GenericServer.ServerInstance.AddStringParameter(storedProcCmd, MyAppDBConstants.STUDENT_NAME, m_Name);
            GenericServer.ServerInstance.AddInt32Parameter(storedProcCmd, MyAppDBConstants.STUDENT_AGE, m_Age);
            GenericServer.ServerInstance.AddStringParameter(storedProcCmd, MyAppDBConstants.STUDENT_STD, m_Standard);
        }
    }
}
