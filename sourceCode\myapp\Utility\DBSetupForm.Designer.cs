namespace Utility
{
    partial class DBSetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblServer = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblDBUser = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.gpDBDetails = new System.Windows.Forms.GroupBox();
            this.tbDBUser = new System.Windows.Forms.TextBox();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.gpDBDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(15, 36);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(69, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server &Name";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(132, 189);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(15, 60);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Data&base";
            // 
            // lblDBUser
            // 
            this.lblDBUser.AutoSize = true;
            this.lblDBUser.Location = new System.Drawing.Point(15, 85);
            this.lblDBUser.Name = "lblDBUser";
            this.lblDBUser.Size = new System.Drawing.Size(47, 13);
            this.lblDBUser.TabIndex = 4;
            this.lblDBUser.Text = "&DB User";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(15, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "&Password";
            // 
            // gpDBDetails
            // 
            this.gpDBDetails.Controls.Add(this.cbDatabases);
            this.gpDBDetails.Controls.Add(this.lblServer);
            this.gpDBDetails.Controls.Add(this.lblDatabase);
            this.gpDBDetails.Controls.Add(this.lblDBUser);
            this.gpDBDetails.Controls.Add(this.lblPassword);
            this.gpDBDetails.Controls.Add(this.tbDBUser);
            this.gpDBDetails.Controls.Add(this.tbDatabase);
            this.gpDBDetails.Controls.Add(this.tbServer);
            this.gpDBDetails.Controls.Add(this.tbPassword);
            this.gpDBDetails.Location = new System.Drawing.Point(12, 25);
            this.gpDBDetails.Name = "gpDBDetails";
            this.gpDBDetails.Size = new System.Drawing.Size(277, 150);
            this.gpDBDetails.TabIndex = 3;
            this.gpDBDetails.TabStop = false;
            this.gpDBDetails.Text = "Database Details";
            // 
            // tbDBUser
            // 
            this.tbDBUser.Location = new System.Drawing.Point(120, 82);
            this.tbDBUser.Name = "tbDBUser";
            this.tbDBUser.Size = new System.Drawing.Size(145, 20);
            this.tbDBUser.TabIndex = 5;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(120, 57);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(145, 20);
            this.tbDatabase.TabIndex = 3;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(120, 33);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(145, 20);
            this.tbServer.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(120, 107);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(145, 20);
            this.tbPassword.TabIndex = 7;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbDatabases
            // 
            this.cbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(120, 0);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(145, 21);
            this.cbDatabases.TabIndex = 6;
            this.cbDatabases.SelectedIndexChanged += new System.EventHandler(this.cbDatabases_SelectedIndexChanged);
            // 
            // DBSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 224);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gpDBDetails);
            this.Controls.Add(this.btnCancel);
            this.Name = "DBSetupForm";
            this.Text = "DBConectForm";
            this.Load += new System.EventHandler(this.DBConectForm_Load);
            this.gpDBDetails.ResumeLayout(false);
            this.gpDBDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblDBUser;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.GroupBox gpDBDetails;
        private System.Windows.Forms.TextBox tbDBUser;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbDatabases;
    }
}