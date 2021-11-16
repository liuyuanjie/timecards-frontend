using System.ComponentModel;

namespace Timecards.Client
{
    partial class FormNavigate
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.linkLabelAddTimecards = new System.Windows.Forms.LinkLabel();
            this.linkLabelOperateTimecards = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(45, 80);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(2512, 1181);
            this.panelMain.TabIndex = 0;
            // 
            // linkLabelAddTimecards
            // 
            this.linkLabelAddTimecards.Location = new System.Drawing.Point(29, 9);
            this.linkLabelAddTimecards.Name = "linkLabelAddTimecards";
            this.linkLabelAddTimecards.Size = new System.Drawing.Size(208, 62);
            this.linkLabelAddTimecards.TabIndex = 1;
            this.linkLabelAddTimecards.TabStop = true;
            this.linkLabelAddTimecards.Text = "Add Timecards";
            this.linkLabelAddTimecards.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelAddTimecards.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAddTimecards_LinkClicked);
            // 
            // linkLabelOperateTimecards
            // 
            this.linkLabelOperateTimecards.Location = new System.Drawing.Point(243, 9);
            this.linkLabelOperateTimecards.Name = "linkLabelOperateTimecards";
            this.linkLabelOperateTimecards.Size = new System.Drawing.Size(208, 62);
            this.linkLabelOperateTimecards.TabIndex = 2;
            this.linkLabelOperateTimecards.TabStop = true;
            this.linkLabelOperateTimecards.Text = "Approve/Decline";
            this.linkLabelOperateTimecards.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelOperateTimecards.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOperateTimecards_LinkClicked);
            // 
            // FormNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2579, 1273);
            this.Controls.Add(this.linkLabelOperateTimecards);
            this.Controls.Add(this.linkLabelAddTimecards);
            this.Controls.Add(this.panelMain);
            this.Name = "FormNavigate";
            this.Text = "Main";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.LinkLabel linkLabelAddTimecards;
        private System.Windows.Forms.LinkLabel linkLabelOperateTimecards;

        private System.Windows.Forms.Panel panelMain;

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;

        private System.Windows.Forms.Panel panel1;

        #endregion
    }
}