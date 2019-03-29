using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using MyPlugin;

namespace GUILib
{
    public partial class DBBaseForm : Form, IMyPlugin
    {
        #region IMyPlugin Implementation

        private String m_Name = String.Empty;
        public String PluginName
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        private String m_Description = String.Empty;
        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        public virtual Form PluginForm
        {
            get { return this; }
        }

        public Int32 EventPriority
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void HandleEvent(String eventName, EventArgs e)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Events

        public delegate void RecordEventHandler(Object sender, RecordLevelEventArgs e);

        // If Parent Form is present, parent might handle it
        public event RecordEventHandler RecordUpdated;
        public event RecordEventHandler RecordDeleted;
        public event RecordEventHandler RecordInserted;
        public event RecordEventHandler OperationAborted;
        public event RecordEventHandler RecordSelected;

        #endregion

        public DBBaseForm()
        {
            InitializeComponent();
        }


        #region Attributes

        #region Control
        private MyBaseUserControl m_userControl = null;
        public MyBaseUserControl BaseUserControl
        {
            set
            {
                this.m_userControl = value;
                AddControlToPanel();
            }
        }
        #endregion
        
        #region ID
        private Int32 m_ID = 0;
        public Int32 ID
        {
            get { return m_ID; }
            set
            {
                m_ID = value;
                if (m_userControl != null)
                    m_userControl.ID = m_ID;
                SetupForm();
            }
        }
        #endregion

        #region "Show MessageBox"
        private Boolean m_showMessageBox = true;
        public Boolean ShowMessageBox
        {
            set { m_showMessageBox = value; }
        }
        #endregion

        #endregion

        #region Methods

        private void SetupForm()
        {
            Boolean addNew = (this.m_userControl.ID == 0);

            if (addNew)
            {
                this.btnDelete.Hide();
            }
            else
            {
                this.btnDelete.Show();
            }
        }

        private void AddControlToPanel()
        {
            this.panelRecordUserControlContainer.Controls.Add(this.m_userControl);
        }

        /// <summary>
        /// Base sets up default handling of events.
        /// If derived class needs to setup its own event handler, it must override this.
        /// </summary>
        protected virtual void SetupFormEventHandlers()
        {
            this.m_userControl.RecordInserted += new MyBaseUserControl.RecordEventHandler(RecordInsertedEventHandler);
            this.m_userControl.RecordUpdated += new MyBaseUserControl.RecordEventHandler(RecordUpdatedEventHandler);
            this.m_userControl.RecordDeleted += new MyBaseUserControl.RecordEventHandler(RecordDeletedEventHandler);
            this.m_userControl.OperationAborted += new MyBaseUserControl.RecordEventHandler(RecordInsertionCancelledEventHandler);
            this.m_userControl.RecordSelected += new MyBaseUserControl.RecordEventHandler(RecordSelectedEventHandler);
        }

        #endregion

        #region "Record Event Handlers"

        // handlers
        private void RecordInsertedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            if (RecordInserted == null)
            {
                if (m_showMessageBox)
                {
                    MessageBox.Show("Record Inserted in Database", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.ID = this.m_userControl.ID;
            }
            else
            {
                RecordInserted(this, e);
            }
        }

        private void RecordUpdatedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            if (RecordUpdated == null)
            {
                if (m_showMessageBox)
                {
                    MessageBox.Show("Record Updated in Database!", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                this.ID = this.m_userControl.ID;
            }
            else
            {
                RecordUpdated(this, e);
            }

        }

        private void RecordDeletedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            if (RecordDeleted == null)
            {
                if(m_showMessageBox)
                {
                    MessageBox.Show("Record Deleted in Database! Switching back to Add New", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                this.m_userControl.ID = 0;
                this.ID = this.m_userControl.ID;
            }
            else
            {
                RecordDeleted(this, e);
            }
        }

        private void RecordInsertionCancelledEventHandler(Object sender, RecordLevelEventArgs e)
        {
            if (OperationAborted == null)
            {
                if (m_showMessageBox)
                {
                    MessageBox.Show("Record Insert Aborted! Switching back to Add New", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.m_userControl.ID = 0;
                this.ID = this.m_userControl.ID;
            }
            else
            {
                OperationAborted(this, e);
            }
        }

        /// <summary>
        /// If any component/form subscribes to RecordSelected event then let the subscriber handle
        ///     else it should handle itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordSelectedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            if (RecordSelected == null)
            {
                SetupForm();
            }
            else
            {
                RecordSelected(this, e);
            }
        }

        #endregion


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean saveRecord = true;
                if (m_showMessageBox)
                {
                    if (DialogResult.No == MessageBox.Show("Do you want to save?", this.Text, MessageBoxButtons.YesNo))
                    {
                        saveRecord = false;
                    }
                }

                if (saveRecord)
                {
                    Boolean addNew = (m_ID == 0);

                    this.m_userControl.Save();

                    m_ID = m_userControl.ID;

                    if (this.Parent == null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        RecordLevelEventArgs evtRecLevel = new RecordLevelEventArgs(m_ID);

                        if (addNew)
                        {
                            if (RecordInserted != null)
                            {
                                RecordInserted(this, evtRecLevel);
                            }
                        }
                        else
                        {
                            if (RecordUpdated != null)
                            {
                                RecordUpdated(this, evtRecLevel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            m_userControl.Delete();

            if (RecordDeleted != null)
            {
                RecordDeleted(this, new RecordLevelEventArgs(m_ID));
            }
        }

        private void DBBaseForm_Load(object sender, EventArgs e)
        {
            this.PluginName = "DBBaseForm";

            SetupFormEventHandlers();

            SetupForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_userControl.CancelAddNew();

                if (OperationAborted != null)
                {
                    OperationAborted(this, new RecordLevelEventArgs(m_ID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // top level forms are to close only
            if (this.Parent == null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
