namespace MarkHeath.MidiUtils
{
    partial class SelectFolderPage
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
            this.textBoxInputFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseInput = new System.Windows.Forms.Button();
            this.checkBoxCopyNonMidi = new System.Windows.Forms.CheckBox();
            this.labelInputFolder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxInputFolder
            // 
            this.textBoxInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInputFolder.Location = new System.Drawing.Point(4, 22);
            this.textBoxInputFolder.Name = "textBoxInputFolder";
            this.textBoxInputFolder.ReadOnly = true;
            this.textBoxInputFolder.Size = new System.Drawing.Size(301, 20);
            this.textBoxInputFolder.TabIndex = 0;
            // 
            // buttonBrowseInput
            // 
            this.buttonBrowseInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseInput.Location = new System.Drawing.Point(311, 20);
            this.buttonBrowseInput.Name = "buttonBrowseInput";
            this.buttonBrowseInput.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseInput.TabIndex = 1;
            this.buttonBrowseInput.Text = "Browse...";
            this.buttonBrowseInput.UseVisualStyleBackColor = true;
            this.buttonBrowseInput.Click += new System.EventHandler(this.buttonBrowseInput_Click);
            // 
            // checkBoxCopyNonMidi
            // 
            this.checkBoxCopyNonMidi.AutoSize = true;
            this.checkBoxCopyNonMidi.Checked = true;
            this.checkBoxCopyNonMidi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopyNonMidi.Location = new System.Drawing.Point(4, 48);
            this.checkBoxCopyNonMidi.Name = "checkBoxCopyNonMidi";
            this.checkBoxCopyNonMidi.Size = new System.Drawing.Size(252, 17);
            this.checkBoxCopyNonMidi.TabIndex = 3;
            this.checkBoxCopyNonMidi.Text = "Copy Non-MIDI Files from Input to Output Folder";
            this.checkBoxCopyNonMidi.UseVisualStyleBackColor = true;
            // 
            // labelInputFolder
            // 
            this.labelInputFolder.AutoSize = true;
            this.labelInputFolder.Location = new System.Drawing.Point(3, 6);
            this.labelInputFolder.Name = "labelInputFolder";
            this.labelInputFolder.Size = new System.Drawing.Size(99, 13);
            this.labelInputFolder.TabIndex = 4;
            this.labelInputFolder.Text = "Select Input Folder:";
            // 
            // SelectFolderPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelInputFolder);
            this.Controls.Add(this.checkBoxCopyNonMidi);
            this.Controls.Add(this.buttonBrowseInput);
            this.Controls.Add(this.textBoxInputFolder);
            this.Name = "SelectFolderPage";
            this.Size = new System.Drawing.Size(396, 77);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInputFolder;
        private System.Windows.Forms.Button buttonBrowseInput;
        private System.Windows.Forms.CheckBox checkBoxCopyNonMidi;
        private System.Windows.Forms.Label labelInputFolder;
    }
}
