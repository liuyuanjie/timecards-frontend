using System.ComponentModel;

namespace Timecards.Client.FormTimecardsList
{
    partial class FormTimecardsList
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
            this.buttonApprove = new System.Windows.Forms.Button();
            this.buttonDecline = new System.Windows.Forms.Button();
            this.dataGridViewTimecards = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewTimecards)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApprove
            // 
            this.buttonApprove.Location = new System.Drawing.Point(59, 30);
            this.buttonApprove.Name = "buttonApprove";
            this.buttonApprove.Size = new System.Drawing.Size(164, 56);
            this.buttonApprove.TabIndex = 0;
            this.buttonApprove.Text = "Approve";
            this.buttonApprove.UseVisualStyleBackColor = true;
            this.buttonApprove.Click += new System.EventHandler(this.buttonApprove_Click);
            // 
            // buttonDecline
            // 
            this.buttonDecline.Location = new System.Drawing.Point(273, 30);
            this.buttonDecline.Name = "buttonDecline";
            this.buttonDecline.Size = new System.Drawing.Size(164, 56);
            this.buttonDecline.TabIndex = 1;
            this.buttonDecline.Text = "Decline";
            this.buttonDecline.UseVisualStyleBackColor = true;
            this.buttonDecline.Click += new System.EventHandler(this.buttonDecline_Click);
            // 
            // dataGridViewTimecards
            // 
            this.dataGridViewTimecards.AllowUserToAddRows = false;
            this.dataGridViewTimecards.AllowUserToDeleteRows = false;
            this.dataGridViewTimecards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTimecards.Location = new System.Drawing.Point(59, 180);
            this.dataGridViewTimecards.Name = "dataGridViewTimecards";
            this.dataGridViewTimecards.ReadOnly = true;
            this.dataGridViewTimecards.RowTemplate.Height = 33;
            this.dataGridViewTimecards.Size = new System.Drawing.Size(2282, 888);
            this.dataGridViewTimecards.TabIndex = 2;
            // 
            // FormTimecardsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2376, 1130);
            this.Controls.Add(this.dataGridViewTimecards);
            this.Controls.Add(this.buttonDecline);
            this.Controls.Add(this.buttonApprove);
            this.Name = "FormTimecardsList";
            this.Text = "FormTimecardsList";
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewTimecards)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dataGridViewTimecards;
        private System.Windows.Forms.Button buttonApprove;
        private System.Windows.Forms.Button buttonDecline;

        #endregion
    }
}