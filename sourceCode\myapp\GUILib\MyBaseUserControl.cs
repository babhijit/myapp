using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using DBLib;


namespace GUILib
{
    public partial class MyBaseUserControl : UserControl
    {
        public MyBaseUserControl()
        {
            InitializeComponent();
        }

        #region Events

        public delegate void RecordEventHandler(Object sender, RecordLevelEventArgs e);

        // Parent must handle these events
        public event RecordEventHandler RecordUpdated;
        public event RecordEventHandler RecordDeleted;
        public event RecordEventHandler RecordInserted;
        public event RecordEventHandler OperationAborted;
        public event RecordEventHandler RecordSelected;

        #endregion

        #region Attributes

        protected Int32 m_ID;
        public Int32 ID
        {
            get { return m_ID; }
            set
            {
                m_ID = value;
                InitialiseForm();

                if (RecordSelected != null)
                {
                    RecordSelected(this, new RecordLevelEventArgs(m_ID));
                }
            }
        }

        private Boolean m_hasCtrlLoaded = false;
        protected Boolean HasCtrlLoaded
        {
            get { return m_hasCtrlLoaded; }
        }

        public virtual Boolean IsDirty
        {
            get { throw new MyAppNotImplemented("Derived class needs to implement this!"); } 
        }

        #endregion

        #region Operations

        private void InitialiseForm()
        {
            #region Visual Studio Designer Fix

            if (GenericServer.ServerInstance == null)
                return;

            #endregion

            Boolean addNew = (m_ID == 0);

            if (addNew)
                PrepareForAddNew();
            else
                InitialiseFormWithData();
        }

        // Virtual Methods that Derived Class need to implement
        protected virtual void PrepareForAddNew() { throw new MyAppNotImplemented("Derived class need to implement this"); }
        protected virtual void InitialiseFormWithData() { throw new MyAppNotImplemented("Derived class need to implement this"); }
        protected virtual void SaveRecord() { throw new MyAppNotImplemented("Derived class need to implement this"); }
        protected virtual void DeleteRecord() { throw new MyAppNotImplemented("Derived class need to implement this"); }
        public virtual void GetValuesToControl() { /* Derived class need to implement this*/ }

        // template methods
        public void Save()
        {
            Boolean addNew = (m_ID == 0);

            SaveRecord();

            if (addNew)
            {
                if (RecordInserted != null)
                    RecordInserted(this, new RecordLevelEventArgs(m_ID));
            }
            else
            {
                if (RecordUpdated != null)
                    RecordUpdated(this, new RecordLevelEventArgs(m_ID));
            }
        }

        public void CancelAddNew()
        {
            PrepareForAddNew();

            if (OperationAborted != null)
                OperationAborted(this, new RecordLevelEventArgs());
        }

        public void Delete()
        {
            DeleteRecord();

            if (RecordDeleted != null)
                RecordDeleted(this, new RecordLevelEventArgs(m_ID));

        }

        #endregion

        #region "Event Handler"

        private void MyBaseUserControl_Load(object sender, EventArgs e)
        {
            m_hasCtrlLoaded = true;
        }

        #endregion
    }
}
