using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MarkHeath.MidiUtils.Properties;

namespace MarkHeath.MidiUtils
{
    public partial class SelectFolderPage : UserControl, IWizardPage
    {
        IWizardPage previousPage;
        IWizardPage nextPage;
        
        public SelectFolderPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            textBoxInputFolder.Text = Settings.Default.InputFolder;
            checkBoxCopyNonMidi.Checked = Settings.Default.CopyNonMidi;
        }

        #region IWizardPage Members

        public bool CanMoveNext
        {
            get { return true; }
        }

        public bool CanMovePrevious
        {
            get { return true; }
        }

        public string NextButtonText
        {
            get { return Resources.NextButtonText; }
        }

        public string PreviousButtonText
        {
            get { return Resources.PreviousButtonText; }
        }

        public string HeadingText
        {
            get { return Resources.SelectFolderPageHeading; }
        }

        public string InstructionsText
        {
            get { return Resources.SelectFolderPageInstructions; }
        }

        public bool CanExitApp
        {
            get { return true; }
        }

        public Control UserInterface
        {
            get { return this; }
        }

        public IWizardPage PreviousPage
        {
            get { return previousPage; }
            set { previousPage = value; }
        }

        public IWizardPage NextPage
        {
            get { return nextPage; }
            set { nextPage = value; }
        }

        public bool ValidatePage()
        {
            if (textBoxInputFolder.Text.Length == 0)
            {
                MessageBox.Show(
                    "Please specify an input folder",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (!Directory.Exists(textBoxInputFolder.Text))
            {
                MessageBox.Show(
                    "Input folder could not be found",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            Settings.Default.InputFolder = textBoxInputFolder.Text;
            Settings.Default.CopyNonMidi = checkBoxCopyNonMidi.Checked;
            return true;
        }

        public event EventHandler Updated;

        /// <summary>
        /// This function not actually used, but makes a compiler warning go away
        /// </summary>
        private void OnUpdated()
        {
            if (Updated != null)
                Updated(this, EventArgs.Empty);
        }

        #endregion

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select Input Folder";
            folderBrowser.SelectedPath = textBoxInputFolder.Text;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                textBoxInputFolder.Text = folderBrowser.SelectedPath;
            }
        }

    }
}
