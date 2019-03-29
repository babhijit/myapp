using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public partial class DBSetupForm : Form
    {
        public DBSetupForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppConfig.Instance.DBType = cbDatabases.SelectedItem.ToString();
            AppConfig.Instance.DBServer = tbServer.Text;
            AppConfig.Instance.Database = tbDatabase.Text;
            AppConfig.Instance.DBUser = tbDBUser.Text;
            AppConfig.Instance.DBPassword = tbPassword.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void DBConectForm_Load(object sender, EventArgs e)
        {
            LoadDBTypes();

            this.cbDatabases.SelectedItem = AppConfig.Instance.DBType;

            tbServer.Text = AppConfig.Instance.DBServer;
            tbDatabase.Text = AppConfig.Instance.Database;
            if (!String.IsNullOrEmpty(AppConfig.Instance.DBUser))
                tbDBUser.Text = AppConfig.Instance.DBUser;
            if (!String.IsNullOrEmpty(AppConfig.Instance.DBPassword))
                tbPassword.Text = AppConfig.Instance.DBPassword;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void LoadDBTypes()
        {
            this.cbDatabases.Items.Add(GlobalConstants.DB_SQLSVR);
            this.cbDatabases.Items.Add(GlobalConstants.DB_MYSQL);
        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            String whichDB = this.cbDatabases.SelectedItem.ToString();
            switch (whichDB)
            {
                case GlobalConstants.DB_MYSQL:
                    this.lblDatabase.Text = "Database";
                    this.lblServer.Text = "Server";
                    break;
                case GlobalConstants.DB_SQLSVR:
                    this.lblDatabase.Text = "Initial Catalog";
                    this.lblServer.Text = "Data Source";
                    break;
                default:
                    throw new MyAppOperationNotAllowed("Invalid Selection");
            }
        }
    }
}
