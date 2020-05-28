namespace WorkingClock
{
    partial class WorkingClockForm
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
            this.label_unlockedTime = new System.Windows.Forms.Label();
            this.label_unlockedDescription = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label_timeOnDate = new System.Windows.Forms.Label();
            this.label_timeLogged = new System.Windows.Forms.Label();
            this.linkLabel_saveFolder = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label_unlockedTime
            // 
            this.label_unlockedTime.AutoSize = true;
            this.label_unlockedTime.Location = new System.Drawing.Point(165, 5);
            this.label_unlockedTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_unlockedTime.Name = "label_unlockedTime";
            this.label_unlockedTime.Size = new System.Drawing.Size(49, 13);
            this.label_unlockedTime.TabIndex = 0;
            this.label_unlockedTime.Text = "00:00:00";
            // 
            // label_unlockedDescription
            // 
            this.label_unlockedDescription.AutoSize = true;
            this.label_unlockedDescription.Location = new System.Drawing.Point(6, 5);
            this.label_unlockedDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_unlockedDescription.Name = "label_unlockedDescription";
            this.label_unlockedDescription.Size = new System.Drawing.Size(142, 13);
            this.label_unlockedDescription.TabIndex = 2;
            this.label_unlockedDescription.Text = "Screen unlocked time today:";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(13, 55);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.monthCalendar1.MaxSelectionCount = 31;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 3;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // label_timeOnDate
            // 
            this.label_timeOnDate.AutoSize = true;
            this.label_timeOnDate.Location = new System.Drawing.Point(6, 30);
            this.label_timeOnDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_timeOnDate.Name = "label_timeOnDate";
            this.label_timeOnDate.Size = new System.Drawing.Size(155, 13);
            this.label_timeOnDate.TabIndex = 4;
            this.label_timeOnDate.Text = "Time logged for selected dates:";
            // 
            // label_timeLogged
            // 
            this.label_timeLogged.AutoSize = true;
            this.label_timeLogged.Location = new System.Drawing.Point(165, 30);
            this.label_timeLogged.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_timeLogged.Name = "label_timeLogged";
            this.label_timeLogged.Size = new System.Drawing.Size(49, 13);
            this.label_timeLogged.TabIndex = 0;
            this.label_timeLogged.Text = "00:00:00";
            // 
            // linkLabel_saveFolder
            // 
            this.linkLabel_saveFolder.AutoSize = true;
            this.linkLabel_saveFolder.Location = new System.Drawing.Point(12, 222);
            this.linkLabel_saveFolder.Name = "linkLabel_saveFolder";
            this.linkLabel_saveFolder.Size = new System.Drawing.Size(61, 13);
            this.linkLabel_saveFolder.TabIndex = 5;
            this.linkLabel_saveFolder.TabStop = true;
            this.linkLabel_saveFolder.Text = "Save folder";
            this.linkLabel_saveFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_saveFolder_LinkClicked);
            // 
            // WorkingClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 243);
            this.Controls.Add(this.linkLabel_saveFolder);
            this.Controls.Add(this.label_timeOnDate);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label_unlockedDescription);
            this.Controls.Add(this.label_timeLogged);
            this.Controls.Add(this.label_unlockedTime);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "WorkingClockForm";
            this.Text = "WorkingClock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_unlockedTime;
        private System.Windows.Forms.Label label_unlockedDescription;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label_timeOnDate;
        private System.Windows.Forms.Label label_timeLogged;
        private System.Windows.Forms.LinkLabel linkLabel_saveFolder;
    }
}

