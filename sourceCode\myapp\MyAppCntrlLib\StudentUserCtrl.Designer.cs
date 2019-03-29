namespace MyAppCntrlLib
{
    partial class StudentUserCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.lblStd = new System.Windows.Forms.Label();
            this.tbStandard = new System.Windows.Forms.TextBox();
            this.gbStudent = new System.Windows.Forms.GroupBox();
            this.gbStudent.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(78, 22);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(216, 20);
            this.tbName.TabIndex = 1;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(20, 50);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(26, 13);
            this.lblAge.TabIndex = 0;
            this.lblAge.Text = "Age";
            // 
            // tbAge
            // 
            this.tbAge.Location = new System.Drawing.Point(78, 47);
            this.tbAge.Name = "tbAge";
            this.tbAge.Size = new System.Drawing.Size(216, 20);
            this.tbAge.TabIndex = 2;
            // 
            // lblStd
            // 
            this.lblStd.AutoSize = true;
            this.lblStd.Location = new System.Drawing.Point(20, 76);
            this.lblStd.Name = "lblStd";
            this.lblStd.Size = new System.Drawing.Size(50, 13);
            this.lblStd.TabIndex = 0;
            this.lblStd.Text = "Standard";
            // 
            // tbStandard
            // 
            this.tbStandard.Location = new System.Drawing.Point(78, 73);
            this.tbStandard.Name = "tbStandard";
            this.tbStandard.Size = new System.Drawing.Size(216, 20);
            this.tbStandard.TabIndex = 3;
            // 
            // gbStudent
            // 
            this.gbStudent.Controls.Add(this.tbStandard);
            this.gbStudent.Controls.Add(this.tbAge);
            this.gbStudent.Controls.Add(this.tbName);
            this.gbStudent.Controls.Add(this.lblStd);
            this.gbStudent.Controls.Add(this.lblAge);
            this.gbStudent.Controls.Add(this.label1);
            this.gbStudent.Location = new System.Drawing.Point(3, 3);
            this.gbStudent.Name = "gbStudent";
            this.gbStudent.Size = new System.Drawing.Size(319, 105);
            this.gbStudent.TabIndex = 2;
            this.gbStudent.TabStop = false;
            this.gbStudent.Text = "Student Details";
            // 
            // StudentUserCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbStudent);
            this.Name = "StudentUserCtrl";
            this.Size = new System.Drawing.Size(326, 115);
            this.gbStudent.ResumeLayout(false);
            this.gbStudent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox tbAge;
        private System.Windows.Forms.Label lblStd;
        private System.Windows.Forms.TextBox tbStandard;
        private System.Windows.Forms.GroupBox gbStudent;
    }
}
