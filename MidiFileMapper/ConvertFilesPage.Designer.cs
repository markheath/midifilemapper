namespace MarkHeath.MidiUtils
{
    partial class ConvertFilesPage
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
            this.buttonBegin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.progressLog1 = new NAudio.Utils.ProgressLog();
            this.SuspendLayout();
            // 
            // buttonBegin
            // 
            this.buttonBegin.Location = new System.Drawing.Point(6, 62);
            this.buttonBegin.Name = "buttonBegin";
            this.buttonBegin.Size = new System.Drawing.Size(75, 23);
            this.buttonBegin.TabIndex = 12;
            this.buttonBegin.Text = "Begin";
            this.buttonBegin.UseVisualStyleBackColor = true;
            this.buttonBegin.Click += new System.EventHandler(this.buttonBegin_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(603, 44);
            this.label4.TabIndex = 11;
            this.label4.Text = "If you are ready to convert your MIDI files, click Begin. \r\nYour existing files w" +
                "ill not be modified, but new ones will be created with the notes remapped using " +
                "the mapping rules selected previously.";
            // 
            // progressLog1
            // 
            this.progressLog1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressLog1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.progressLog1.Location = new System.Drawing.Point(6, 92);
            this.progressLog1.Name = "progressLog1";
            this.progressLog1.Padding = new System.Windows.Forms.Padding(1);
            this.progressLog1.Size = new System.Drawing.Size(597, 122);
            this.progressLog1.TabIndex = 13;
            // 
            // ConvertFilesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressLog1);
            this.Controls.Add(this.buttonBegin);
            this.Controls.Add(this.label4);
            this.Name = "ConvertFilesPage";
            this.Size = new System.Drawing.Size(610, 220);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBegin;
        private System.Windows.Forms.Label label4;
        private NAudio.Utils.ProgressLog progressLog1;
    }
}
