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
using MyAppDBLib;
using GUILib;

namespace MyAppCntrlLib
{
    public partial class StudentUserCtrl : MyBaseUserControl
    {
        public StudentUserCtrl()
        {
            InitializeComponent();
        }

        private DBStudent m_student = new DBStudent();

        protected override void PrepareForAddNew()
        {
            m_student.PK = 0;

            this.tbName.Text = m_student.Name = String.Empty;
            m_student.Age = 0;
            this.tbAge.Text = String.Empty;
            this.tbStandard.Text = m_student.Standard = String.Empty;
        }

        protected override void InitialiseFormWithData()
        {
            #region "VISUAL STUDIO Designer Fix"
            if (GenericServer.ServerInstance == null)
                return;
            #endregion

            m_student.PK = m_ID;
            this.tbName.Text = m_student.Name;
            this.tbStandard.Text = m_student.Standard;
            this.tbAge.Text = m_student.Age.ToString();
        }

        public override void GetValuesToControl()
        {
            base.GetValuesToControl();

            m_student.Name = this.tbName.Text;
            m_student.Standard = this.tbStandard.Text;
            m_student.Age = Convert.ToInt32(this.tbAge.Text);

        }

        protected override void SaveRecord()
        {
            GetValuesToControl();

            m_student.Save();
        }

        protected override void DeleteRecord()
        {
            m_student.Delete();
        }

        public override Boolean IsDirty
        {
            get
            {
                return m_student.IsDirty;
            }
        }
    }
}
