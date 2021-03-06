
using System.ComponentModel;
using System.Windows.Forms;

namespace Timecards.Client
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelRole = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.LabelUserName = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeWorkDate = new System.Windows.Forms.DateTimePicker();
            this.buttonNew = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.labelWorkHours = new System.Windows.Forms.Label();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainerWorkTime = new System.Windows.Forms.SplitContainer();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxWorkTime = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.splitContainerWorkTime.Panel1.SuspendLayout();
            this.splitContainerWorkTime.SuspendLayout();
            this.groupBoxWorkTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelRole);
            this.groupBox1.Controls.Add(this.labelEmail);
            this.groupBox1.Controls.Add(this.LabelUserName);
            this.groupBox1.Controls.Add(this.labelLastName);
            this.groupBox1.Controls.Add(this.labelFirstName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2441, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Profile";
            // 
            // labelRole
            // 
            this.labelRole.Location = new System.Drawing.Point(1678, 52);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(213, 25);
            this.labelRole.TabIndex = 9;
            // 
            // labelEmail
            // 
            this.labelEmail.Location = new System.Drawing.Point(1237, 52);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(367, 25);
            this.labelEmail.TabIndex = 8;
            // 
            // LabelUserName
            // 
            this.LabelUserName.Location = new System.Drawing.Point(894, 52);
            this.LabelUserName.Name = "LabelUserName";
            this.LabelUserName.Size = new System.Drawing.Size(213, 25);
            this.LabelUserName.TabIndex = 7;
            // 
            // labelLastName
            // 
            this.labelLastName.Location = new System.Drawing.Point(488, 52);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(213, 25);
            this.labelLastName.TabIndex = 6;
            // 
            // labelFirstName
            // 
            this.labelFirstName.Location = new System.Drawing.Point(142, 52);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(213, 25);
            this.labelFirstName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1610, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Role:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1160, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(763, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name:";
            // 
            // dateTimeWorkDate
            // 
            this.dateTimeWorkDate.Location = new System.Drawing.Point(7, 76);
            this.dateTimeWorkDate.Name = "dateTimeWorkDate";
            this.dateTimeWorkDate.Size = new System.Drawing.Size(489, 31);
            this.dateTimeWorkDate.TabIndex = 1;
            this.dateTimeWorkDate.ValueChanged += new System.EventHandler(this.dateTimeWorkDate_ValueChanged);
            // 
            // buttonNew
            // 
            this.buttonNew.Enabled = false;
            this.buttonNew.Location = new System.Drawing.Point(8, 299);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(496, 62);
            this.buttonNew.TabIndex = 12;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Select Workday:";
            // 
            // labelWorkHours
            // 
            this.labelWorkHours.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelWorkHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelWorkHours.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelWorkHours.Location = new System.Drawing.Point(8, 630);
            this.labelWorkHours.Name = "labelWorkHours";
            this.labelWorkHours.Size = new System.Drawing.Size(488, 74);
            this.labelWorkHours.TabIndex = 10;
            this.labelWorkHours.Text = "40";
            this.labelWorkHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(8, 174);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(496, 33);
            this.comboBoxProject.TabIndex = 2;
            this.comboBoxProject.SelectedValueChanged += new System.EventHandler(this.comboBoxProject_SelectedValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 66);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(489, 31);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 157);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(496, 33);
            this.comboBox1.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 25);
            this.label9.TabIndex = 10;
            this.label9.Text = "Select Project:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 25);
            this.label8.TabIndex = 11;
            this.label8.Text = "Select Workday:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(496, 62);
            this.button1.TabIndex = 12;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // splitContainerWorkTime
            // 
            this.splitContainerWorkTime.Location = new System.Drawing.Point(6, 30);
            this.splitContainerWorkTime.Name = "splitContainerWorkTime";
            // 
            // splitContainerWorkTime.Panel1
            // 
            this.splitContainerWorkTime.Panel1.Controls.Add(this.buttonSubmit);
            this.splitContainerWorkTime.Panel1.Controls.Add(this.buttonSave);
            this.splitContainerWorkTime.Panel1.Controls.Add(this.comboBoxProject);
            this.splitContainerWorkTime.Panel1.Controls.Add(this.buttonNew);
            this.splitContainerWorkTime.Panel1.Controls.Add(this.labelWorkHours);
            this.splitContainerWorkTime.Panel1.Controls.Add(this.dateTimeWorkDate);
            this.splitContainerWorkTime.Panel1.Controls.Add(this.label7);
            this.splitContainerWorkTime.Size = new System.Drawing.Size(2413, 970);
            this.splitContainerWorkTime.SplitterDistance = 519;
            this.splitContainerWorkTime.TabIndex = 13;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(0, 733);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonSubmit.Size = new System.Drawing.Size(496, 62);
            this.buttonSubmit.TabIndex = 14;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(7, 549);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(493, 62);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // groupBoxWorkTime
            // 
            this.groupBoxWorkTime.Controls.Add(this.splitContainerWorkTime);
            this.groupBoxWorkTime.Location = new System.Drawing.Point(12, 140);
            this.groupBoxWorkTime.Name = "groupBoxWorkTime";
            this.groupBoxWorkTime.Size = new System.Drawing.Size(2436, 1024);
            this.groupBoxWorkTime.TabIndex = 14;
            this.groupBoxWorkTime.TabStop = false;
            this.groupBoxWorkTime.Text = "Work Time";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2472, 1171);
            this.Controls.Add(this.groupBoxWorkTime);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Work Time";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainerWorkTime.Panel1.ResumeLayout(false);
            this.splitContainerWorkTime.Panel1.PerformLayout();
            this.splitContainerWorkTime.ResumeLayout(false);
            this.groupBoxWorkTime.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label labelWorkHours;

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSubmit;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private DateTimePicker dateTimeWorkDate;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private Label labelRole;
        private Label labelEmail;
        private Label LabelUserName;
        private Label labelLastName;
        private Label labelFirstName;
        private Label label7;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.SplitContainer splitContainerWorkTime;
        private System.Windows.Forms.GroupBox groupBoxWorkTime;
    }
}