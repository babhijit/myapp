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
    public partial class MailSetupForm : Form
    {
        public MailSetupForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                AppConfig.Instance.SmtpHost = this.tbHost.Text;
                AppConfig.Instance.SmtpPort = Convert.ToInt32(this.tbPort.Text);
                AppConfig.Instance.SmtpUser = this.tbUser.Text;
                AppConfig.Instance.SmtpPwd = this.tbPwd.Text;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (System.Exception exception)
            {
                String exceptionMessage = "Failed to connect to Mail Server.\n" + exception.Message;
                Utility.Logger.Instance.WriteLog(exceptionMessage);
            }

        }

        private void MailSetupForm_Load(object sender, EventArgs e)
        {
            this.tbHost.Text = AppConfig.Instance.SmtpHost;
            this.tbPort.Text = AppConfig.Instance.SmtpPort.ToString();
            this.tbUser.Text = AppConfig.Instance.SmtpUser;
            this.tbPwd.Text = AppConfig.Instance.SmtpPwd;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
