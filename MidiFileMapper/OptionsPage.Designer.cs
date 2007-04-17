namespace MarkHeath.MidiUtils
{
    partial class OptionsPage
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
            this.groupBoxFileType = new System.Windows.Forms.GroupBox();
            this.radioButtonType1 = new System.Windows.Forms.RadioButton();
            this.radioButtonType0 = new System.Windows.Forms.RadioButton();
            this.radioButtonUnchanged = new System.Windows.Forms.RadioButton();
            this.groupBoxLocation = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxFileType.SuspendLayout();
            this.groupBoxLocation.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFileType
            // 
            this.groupBoxFileType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFileType.Controls.Add(this.radioButtonType1);
            this.groupBoxFileType.Controls.Add(this.radioButtonType0);
            this.groupBoxFileType.Controls.Add(this.radioButtonUnchanged);
            this.groupBoxFileType.Location = new System.Drawing.Point(7, 67);
            this.groupBoxFileType.Name = "groupBoxFileType";
            this.groupBoxFileType.Size = new System.Drawing.Size(560, 100);
            this.groupBoxFileType.TabIndex = 2;
            this.groupBoxFileType.TabStop = false;
            this.groupBoxFileType.Text = "MIDI File Type";
            // 
            // radioButtonType1
            // 
            this.radioButtonType1.AutoSize = true;
            this.radioButtonType1.Location = new System.Drawing.Point(7, 68);
            this.radioButtonType1.Name = "radioButtonType1";
            this.radioButtonType1.Size = new System.Drawing.Size(106, 17);
            this.radioButtonType1.TabIndex = 2;
            this.radioButtonType1.Text = "Convert to type 1";
            this.radioButtonType1.UseVisualStyleBackColor = true;
            // 
            // radioButtonType0
            // 
            this.radioButtonType0.AutoSize = true;
            this.radioButtonType0.Location = new System.Drawing.Point(7, 44);
            this.radioButtonType0.Name = "radioButtonType0";
            this.radioButtonType0.Size = new System.Drawing.Size(106, 17);
            this.radioButtonType0.TabIndex = 1;
            this.radioButtonType0.Text = "Convert to type 0";
            this.radioButtonType0.UseVisualStyleBackColor = true;
            // 
            // radioButtonUnchanged
            // 
            this.radioButtonUnchanged.AutoSize = true;
            this.radioButtonUnchanged.Checked = true;
            this.radioButtonUnchanged.Location = new System.Drawing.Point(7, 20);
            this.radioButtonUnchanged.Name = "radioButtonUnchanged";
            this.radioButtonUnchanged.Size = new System.Drawing.Size(112, 17);
            this.radioButtonUnchanged.TabIndex = 0;
            this.radioButtonUnchanged.TabStop = true;
            this.radioButtonUnchanged.Text = "Leave unchanged";
            this.radioButtonUnchanged.UseVisualStyleBackColor = true;
            // 
            // groupBoxLocation
            // 
            this.groupBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLocation.Controls.Add(this.buttonBrowse);
            this.groupBoxLocation.Controls.Add(this.textBoxOutputFolder);
            this.groupBoxLocation.Controls.Add(this.label2);
            this.groupBoxLocation.Location = new System.Drawing.Point(7, 3);
            this.groupBoxLocation.Name = "groupBoxLocation";
            this.groupBoxLocation.Size = new System.Drawing.Size(560, 58);
            this.groupBoxLocation.TabIndex = 1;
            this.groupBoxLocation.TabStop = false;
            this.groupBoxLocation.Text = "Output Folder Location";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(468, 22);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 6;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxCustomFolder
            // 
            this.textBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(89, 24);
            this.textBoxOutputFolder.Name = "textBoxCustomFolder";
            this.textBoxOutputFolder.ReadOnly = true;
            this.textBoxOutputFolder.Size = new System.Drawing.Size(363, 20);
            this.textBoxOutputFolder.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output Folder:";
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxLocation);
            this.Controls.Add(this.groupBoxFileType);
            this.Name = "OptionsPage";
            this.Size = new System.Drawing.Size(570, 179);
            this.groupBoxFileType.ResumeLayout(false);
            this.groupBoxFileType.PerformLayout();
            this.groupBoxLocation.ResumeLayout(false);
            this.groupBoxLocation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFileType;
        private System.Windows.Forms.RadioButton radioButtonType1;
        private System.Windows.Forms.RadioButton radioButtonType0;
        private System.Windows.Forms.RadioButton radioButtonUnchanged;
        private System.Windows.Forms.GroupBox groupBoxLocation;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Label label2;
    }
}
