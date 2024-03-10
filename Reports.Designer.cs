namespace Mid_Term_Project
{
    partial class Reports
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbReports = new System.Windows.Forms.ComboBox();
            this.cmbAssessment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.cmbCLO = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(105, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Report :";
            // 
            // cmbReports
            // 
            this.cmbReports.BackColor = System.Drawing.Color.Gainsboro;
            this.cmbReports.DisplayMember = "5";
            this.cmbReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReports.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cmbReports.FormattingEnabled = true;
            this.cmbReports.Items.AddRange(new object[] {
            "Attendence Report",
            "Assessment Wise",
            "CLO Wise Result",
            "Class Result"});
            this.cmbReports.Location = new System.Drawing.Point(299, 55);
            this.cmbReports.Margin = new System.Windows.Forms.Padding(0);
            this.cmbReports.Name = "cmbReports";
            this.cmbReports.Size = new System.Drawing.Size(270, 29);
            this.cmbReports.TabIndex = 34;
            this.cmbReports.SelectedIndexChanged += new System.EventHandler(this.cmbCLOs_SelectedIndexChanged);
            // 
            // cmbAssessment
            // 
            this.cmbAssessment.BackColor = System.Drawing.Color.Gainsboro;
            this.cmbAssessment.DisplayMember = "5";
            this.cmbAssessment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssessment.Enabled = false;
            this.cmbAssessment.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cmbAssessment.FormattingEnabled = true;
            this.cmbAssessment.Items.AddRange(new object[] {
            "Attendence Report",
            "Assessment Wise",
            "CLO Wise Result"});
            this.cmbAssessment.Location = new System.Drawing.Point(299, 102);
            this.cmbAssessment.Margin = new System.Windows.Forms.Padding(0);
            this.cmbAssessment.Name = "cmbAssessment";
            this.cmbAssessment.Size = new System.Drawing.Size(270, 29);
            this.cmbAssessment.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(105, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 21);
            this.label2.TabIndex = 36;
            this.label2.Text = "Select Assessment :";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateReport.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.btnGenerateReport.Location = new System.Drawing.Point(450, 193);
            this.btnGenerateReport.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(119, 37);
            this.btnGenerateReport.TabIndex = 37;
            this.btnGenerateReport.Text = "Generate";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // cmbCLO
            // 
            this.cmbCLO.BackColor = System.Drawing.Color.Gainsboro;
            this.cmbCLO.DisplayMember = "5";
            this.cmbCLO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCLO.Enabled = false;
            this.cmbCLO.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cmbCLO.FormattingEnabled = true;
            this.cmbCLO.Location = new System.Drawing.Point(299, 146);
            this.cmbCLO.Margin = new System.Windows.Forms.Padding(0);
            this.cmbCLO.Name = "cmbCLO";
            this.cmbCLO.Size = new System.Drawing.Size(270, 29);
            this.cmbCLO.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(105, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 21);
            this.label3.TabIndex = 39;
            this.label3.Text = "Select CLO :";
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 285);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCLO);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbAssessment);
            this.Controls.Add(this.cmbReports);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbReports;
        private System.Windows.Forms.ComboBox cmbAssessment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.ComboBox cmbCLO;
        private System.Windows.Forms.Label label3;
    }
}