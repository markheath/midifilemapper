namespace MarkHeath.MidiUtils
{
    partial class WelcomePage
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
            this.radioButtonInputFolder = new System.Windows.Forms.RadioButton();
            this.radioButtonInputFiles = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonInputFolder
            // 
            this.radioButtonInputFolder.AutoSize = true;
            this.radioButtonInputFolder.Checked = true;
            this.radioButtonInputFolder.Location = new System.Drawing.Point(18, 14);
            this.radioButtonInputFolder.Name = "radioButtonInputFolder";
            this.radioButtonInputFolder.Size = new System.Drawing.Size(283, 17);
            this.radioButtonInputFolder.TabIndex = 0;
            this.radioButtonInputFolder.TabStop = true;
            this.radioButtonInputFolder.Text = "Select a folder and process all the MIDI files it contains";
            this.radioButtonInputFolder.UseVisualStyleBackColor = true;
            // 
            // radioButtonInputFiles
            // 
            this.radioButtonInputFiles.AutoSize = true;
            this.radioButtonInputFiles.Location = new System.Drawing.Point(18, 37);
            this.radioButtonInputFiles.Name = "radioButtonInputFiles";
            this.radioButtonInputFiles.Size = new System.Drawing.Size(201, 17);
            this.radioButtonInputFiles.TabIndex = 0;
            this.radioButtonInputFiles.Text = "Select individual MIDI files to process";
            this.radioButtonInputFiles.UseVisualStyleBackColor = true;
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonInputFiles);
            this.Controls.Add(this.radioButtonInputFolder);
            this.Name = "WelcomePage";
            this.Size = new System.Drawing.Size(343, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonInputFolder;
        private System.Windows.Forms.RadioButton radioButtonInputFiles;
    }
}
