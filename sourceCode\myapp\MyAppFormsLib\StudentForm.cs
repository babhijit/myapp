using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBLib;
using MyAppCntrlLib;
using MyAppDBLib;
using MyPlugin;
using Utility;
using SupportLib;

namespace MyAppFormsLib
{
    [MyPlugin(MyAppDBConstants.TABLE_STUDENT, "Student Form")]
    public partial class StudentForm : Form, IMyPlugin
    {
        #region Events

        public delegate void RecordEventHandler(Object sender, RecordLevelEventArgs e);

        // If Parent Form is present, parent might handle it
        public event RecordEventHandler RecordUpdated;
        public event RecordEventHandler RecordDeleted;
        public event RecordEventHandler RecordInserted;
        public event RecordEventHandler OperationAborted;

        #endregion

        #region IMyPlugin Implementation

        private String m_description = String.Empty;
        public String Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
            }
        }

        private String m_pluginName = String.Empty;
        public String PluginName
        {
            get
            {
                return m_pluginName;
            }
            set
            {
                m_pluginName = value;
            }
        }
        
        public Form PluginForm
        {
            get { return this; }
        }

        private Int32 m_Priority = 0;
        public Int32 EventPriority
        {
            get
            {
                return m_Priority;
            }
            set
            {
                m_Priority = value;
            }
        }

        public void HandleEvent(String eventName, EventArgs e)
        {
            if (eventName.Equals(Utility.GlobalConstants.APP_SHUTDOWN))
            {
                SupportLib.MyEventArg<String> shutdownEventArg = e as SupportLib.MyEventArg<String>;
                if (shutdownEventArg != null)
                {
                    this.studentUserCtrl.GetValuesToControl();
                    if (this.studentUserCtrl.IsDirty)
                    {
                        if (MessageBox.Show("Do you want to save data?", "Save Data", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            this.studentUserCtrl.Save();
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion


        public StudentForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.studentUserCtrl.Save();
        }


        private void StudentForm_Load(object sender, EventArgs e)
        {
            // Setup the event delegates
            this.studentUserCtrl.RecordInserted += new StudentUserCtrl.RecordEventHandler(StudentInsertedEventHandler);
            this.studentUserCtrl.RecordDeleted += new StudentUserCtrl.RecordEventHandler(StudentDeletedEventHandler);
            this.studentUserCtrl.RecordUpdated += new StudentUserCtrl.RecordEventHandler(StudentUpdatedEventHandler);
            this.studentUserCtrl.OperationAborted += new StudentUserCtrl.RecordEventHandler(StudentInsertionCancelledEventHandler);

            // bind combo to database
            #region FIX FOR PROPER LOADING OF THE CONTROL IN Visual Studio
            // This is a FIX is for VISUAL STUDIO Editor ;)
            if (GenericServer.ServerInstance == null)
                return;
            #endregion

            RefreshStudentSelector();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.studentUserCtrl.Delete();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.studentUserCtrl.CancelAddNew();
        }

        #region "Event Handlers"

        // handlers
        private void StudentInsertedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            RefreshStudentSelector();

            if (RecordInserted == null)
                MessageBox.Show("Student Record Inserted in Database", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                RecordInserted(this, e);
        }

        private void StudentUpdatedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            RefreshStudentSelector();

            if (RecordUpdated == null)
                MessageBox.Show("Student Record Updated in Database", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                RecordUpdated(this, e);
        }

        private void StudentDeletedEventHandler(Object sender, RecordLevelEventArgs e)
        {
            RefreshStudentSelector();

            if (RecordDeleted == null)
            {
                MessageBox.Show("Student Record Deleted in Database", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetupForm();
            }
            else
            {
                RecordDeleted(this, e);
            }
        }

        private void StudentInsertionCancelledEventHandler(Object sender, RecordLevelEventArgs e)
        {
            if (OperationAborted == null)
            {
                MessageBox.Show("DB Operation Aborted!", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.cbStudentID.SelectedValue = 0;

                SetupForm();
            }
            else
            {
                OperationAborted(this, e);
            }
        }

        #endregion

        private void SetupForm()
        {
            Boolean addNew = (this.studentUserCtrl.ID == 0);

            if (addNew)
                this.btnDelete.Hide();
            else
                this.btnDelete.Show(); // all three buttons are enabled
        }

        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.studentUserCtrl.ID = (Int32)this.cbStudentID.SelectedValue;
            SetupForm();
        }

        private void RefreshStudentSelector()
        {
            if (GenericServer.ServerInstance.IsOpen)
            {
                String sqlSelect = String.Format("SELECT {0}, {1} FROM {2}", MyAppDBConstants.STUDENT_PK,
                                                                             MyAppDBConstants.STUDENT_NAME,
                                                                             MyAppDBConstants.TABLE_STUDENT);

                DataSet usersDataSet = GenericServer.ServerInstance.GetDataSetForSelectQuery(sqlSelect, MyAppDBConstants.TABLE_STUDENT);
                this.cbStudentID.BindingContext = new System.Windows.Forms.BindingContext();
                this.cbStudentID.DisplayMember = MyAppDBConstants.STUDENT_NAME;
                this.cbStudentID.ValueMember = MyAppDBConstants.STUDENT_PK;
                this.cbStudentID.DataSource = usersDataSet.Tables[MyAppDBConstants.TABLE_STUDENT];
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.studentUserCtrl.ID = 0;
            SetupForm();
        }
    }
}
