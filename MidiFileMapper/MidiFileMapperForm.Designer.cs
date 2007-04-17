namespace MarkHeath.MidiUtils
{
    partial class MidiFileMapperForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCurrentPage = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelPageInstructions = new System.Windows.Forms.Label();
            this.labelPageHeading = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(582, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.contentsToolStripMenuItem.Text = "&Contents...";
            this.contentsToolStripMenuItem.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panelCurrentPage
            // 
            this.panelCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCurrentPage.Location = new System.Drawing.Point(0, 61);
            this.panelCurrentPage.Name = "panelCurrentPage";
            this.panelCurrentPage.Padding = new System.Windows.Forms.Padding(5);
            this.panelCurrentPage.Size = new System.Drawing.Size(582, 234);
            this.panelCurrentPage.TabIndex = 14;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.Window;
            this.panelHeader.Controls.Add(this.labelPageInstructions);
            this.panelHeader.Controls.Add(this.labelPageHeading);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(582, 37);
            this.panelHeader.TabIndex = 17;
            // 
            // labelPageInstructions
            // 
            this.labelPageInstructions.AutoSize = true;
            this.labelPageInstructions.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPageInstructions.Location = new System.Drawing.Point(6, 19);
            this.labelPageInstructions.Name = "labelPageInstructions";
            this.labelPageInstructions.Size = new System.Drawing.Size(173, 14);
            this.labelPageInstructions.TabIndex = 14;
            this.labelPageInstructions.Text = "Page instructions go here.";
            // 
            // labelPageHeading
            // 
            this.labelPageHeading.AutoSize = true;
            this.labelPageHeading.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPageHeading.Location = new System.Drawing.Point(6, 4);
            this.labelPageHeading.Name = "labelPageHeading";
            this.labelPageHeading.Size = new System.Drawing.Size(204, 14);
            this.labelPageHeading.TabIndex = 14;
            this.labelPageHeading.Text = "Step 1 of 4: Select Input Files";
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.buttonPrevious);
            this.panelFooter.Controls.Add(this.buttonNext);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 295);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(582, 31);
            this.panelFooter.TabIndex = 18;
            this.panelFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFooter_Paint);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevious.Location = new System.Drawing.Point(412, 5);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevious.TabIndex = 18;
            this.buttonPrevious.Text = "< Back";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(493, 5);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 17;
            this.buttonNext.Text = "Next >";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // MidiFileMapperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 326);
            this.Controls.Add(this.panelCurrentPage);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MidiFileMapperForm";
            this.Text = "MIDI File Mapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MidiFileMapperForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.Panel panelCurrentPage;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelPageHeading;
        private System.Windows.Forms.Label labelPageInstructions;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
    }
}

