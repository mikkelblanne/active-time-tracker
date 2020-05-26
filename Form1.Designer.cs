namespace StopWatch
{
    partial class Form1
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
            this.label_manualTime = new System.Windows.Forms.Label();
            this.button_manualTime = new System.Windows.Forms.Button();
            this.label_unlockedTime = new System.Windows.Forms.Label();
            this.label_unlockedDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_manualTime
            // 
            this.label_manualTime.AutoSize = true;
            this.label_manualTime.Location = new System.Drawing.Point(13, 13);
            this.label_manualTime.Name = "label_manualTime";
            this.label_manualTime.Size = new System.Drawing.Size(64, 17);
            this.label_manualTime.TabIndex = 0;
            this.label_manualTime.Text = "00:00:00";
            // 
            // button_manualTime
            // 
            this.button_manualTime.Location = new System.Drawing.Point(154, 10);
            this.button_manualTime.Name = "button_manualTime";
            this.button_manualTime.Size = new System.Drawing.Size(75, 23);
            this.button_manualTime.TabIndex = 1;
            this.button_manualTime.Text = "Start";
            this.button_manualTime.UseVisualStyleBackColor = true;
            this.button_manualTime.Click += new System.EventHandler(this.button_toggle_Click);
            // 
            // label_unlockedTime
            // 
            this.label_unlockedTime.AutoSize = true;
            this.label_unlockedTime.Location = new System.Drawing.Point(12, 43);
            this.label_unlockedTime.Name = "label_unlockedTime";
            this.label_unlockedTime.Size = new System.Drawing.Size(64, 17);
            this.label_unlockedTime.TabIndex = 0;
            this.label_unlockedTime.Text = "00:00:00";
            // 
            // label_unlockedDescription
            // 
            this.label_unlockedDescription.AutoSize = true;
            this.label_unlockedDescription.Location = new System.Drawing.Point(82, 43);
            this.label_unlockedDescription.Name = "label_unlockedDescription";
            this.label_unlockedDescription.Size = new System.Drawing.Size(144, 17);
            this.label_unlockedDescription.TabIndex = 2;
            this.label_unlockedDescription.Text = "Screen unlocked time";
            // 
            // Form1
            // 
            this.AcceptButton = this.button_manualTime;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 89);
            this.Controls.Add(this.label_unlockedDescription);
            this.Controls.Add(this.button_manualTime);
            this.Controls.Add(this.label_unlockedTime);
            this.Controls.Add(this.label_manualTime);
            this.Name = "Form1";
            this.Text = "StopWatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_manualTime;
        private System.Windows.Forms.Button button_manualTime;
        private System.Windows.Forms.Label label_unlockedTime;
        private System.Windows.Forms.Label label_unlockedDescription;
    }
}

