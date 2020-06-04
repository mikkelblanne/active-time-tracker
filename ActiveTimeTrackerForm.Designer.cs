namespace ActiveTimeTracker
{
    partial class ActiveTimeTrackerForm
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
            this.label_manuallyAdd = new System.Windows.Forms.Label();
            this.textBox_manuallyAdd = new System.Windows.Forms.TextBox();
            this.button_manuallyAdd = new System.Windows.Forms.Button();
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
            this.monthCalendar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monthCalendar1.Location = new System.Drawing.Point(13, 115);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.monthCalendar1.MaxSelectionCount = 31;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 3;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // label_timeOnDate
            // 
            this.label_timeOnDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_timeOnDate.AutoSize = true;
            this.label_timeOnDate.Location = new System.Drawing.Point(6, 90);
            this.label_timeOnDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_timeOnDate.Name = "label_timeOnDate";
            this.label_timeOnDate.Size = new System.Drawing.Size(155, 13);
            this.label_timeOnDate.TabIndex = 4;
            this.label_timeOnDate.Text = "Time logged for selected dates:";
            // 
            // label_timeLogged
            // 
            this.label_timeLogged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_timeLogged.AutoSize = true;
            this.label_timeLogged.Location = new System.Drawing.Point(165, 90);
            this.label_timeLogged.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_timeLogged.Name = "label_timeLogged";
            this.label_timeLogged.Size = new System.Drawing.Size(49, 13);
            this.label_timeLogged.TabIndex = 0;
            this.label_timeLogged.Text = "00:00:00";
            // 
            // linkLabel_saveFolder
            // 
            this.linkLabel_saveFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel_saveFolder.AutoSize = true;
            this.linkLabel_saveFolder.Location = new System.Drawing.Point(12, 282);
            this.linkLabel_saveFolder.Name = "linkLabel_saveFolder";
            this.linkLabel_saveFolder.Size = new System.Drawing.Size(61, 13);
            this.linkLabel_saveFolder.TabIndex = 5;
            this.linkLabel_saveFolder.TabStop = true;
            this.linkLabel_saveFolder.Text = "Save folder";
            this.linkLabel_saveFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_saveFolder_LinkClicked);
            // 
            // label_manuallyAdd
            // 
            this.label_manuallyAdd.AutoSize = true;
            this.label_manuallyAdd.Location = new System.Drawing.Point(6, 28);
            this.label_manuallyAdd.Name = "label_manuallyAdd";
            this.label_manuallyAdd.Size = new System.Drawing.Size(112, 13);
            this.label_manuallyAdd.TabIndex = 6;
            this.label_manuallyAdd.Text = "Manually add minutes:";
            // 
            // textBox_manuallyAdd
            // 
            this.textBox_manuallyAdd.Location = new System.Drawing.Point(129, 25);
            this.textBox_manuallyAdd.Name = "textBox_manuallyAdd";
            this.textBox_manuallyAdd.Size = new System.Drawing.Size(42, 20);
            this.textBox_manuallyAdd.TabIndex = 7;
            // 
            // button_manuallyAdd
            // 
            this.button_manuallyAdd.Location = new System.Drawing.Point(177, 23);
            this.button_manuallyAdd.Name = "button_manuallyAdd";
            this.button_manuallyAdd.Size = new System.Drawing.Size(37, 23);
            this.button_manuallyAdd.TabIndex = 8;
            this.button_manuallyAdd.Text = "Add";
            this.button_manuallyAdd.UseVisualStyleBackColor = true;
            this.button_manuallyAdd.Click += new System.EventHandler(this.button_manuallyAdd_Click);
            // 
            // ActiveTimeTracker
            // 
            this.AcceptButton = this.button_manuallyAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 303);
            this.Controls.Add(this.button_manuallyAdd);
            this.Controls.Add(this.textBox_manuallyAdd);
            this.Controls.Add(this.label_manuallyAdd);
            this.Controls.Add(this.linkLabel_saveFolder);
            this.Controls.Add(this.label_timeOnDate);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label_unlockedDescription);
            this.Controls.Add(this.label_timeLogged);
            this.Controls.Add(this.label_unlockedTime);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ActiveTimeTrackerForm";
            this.Text = "ActiveTimeTracker";
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
        private System.Windows.Forms.Label label_manuallyAdd;
        private System.Windows.Forms.TextBox textBox_manuallyAdd;
        private System.Windows.Forms.Button button_manuallyAdd;
    }
}

