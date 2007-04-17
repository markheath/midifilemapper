namespace MarkHeath.MidiUtils
{
    partial class SelectMappingPage
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
            this.labelInstructions = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.listBoxMapping = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelInstructions
            // 
            this.labelInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInstructions.Location = new System.Drawing.Point(3, 11);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(476, 32);
            this.labelInstructions.TabIndex = 16;
            this.labelInstructions.Text = "You can choose from the included xml mapping files or browse for your own custom " +
                "mapping file.\r\nCakewalk SONAR and Steinberg Cubase drum map files are also suppo" +
                "rted.";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(404, 46);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 15;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // listBoxMapping
            // 
            this.listBoxMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMapping.FormattingEnabled = true;
            this.listBoxMapping.Location = new System.Drawing.Point(3, 45);
            this.listBoxMapping.Name = "listBoxMapping";
            this.listBoxMapping.Size = new System.Drawing.Size(394, 69);
            this.listBoxMapping.TabIndex = 17;
            // 
            // SelectMappingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxMapping);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.buttonBrowse);
            this.Name = "SelectMappingPage";
            this.Size = new System.Drawing.Size(494, 120);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ListBox listBoxMapping;
    }
}
