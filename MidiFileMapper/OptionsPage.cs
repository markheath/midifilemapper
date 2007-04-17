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
    public partial class OptionsPage : UserControl, IWizardPage
    {
        IWizardPage previousPage;
        IWizardPage nextPage;
        
        public OptionsPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            LoadSettings();
        }

        private void LoadSettings()
        {
            Settings settings = Settings.Default;
            if (settings.OutputFolder.Length == 0)
            {
                textBoxOutputFolder.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            else
            {
                textBoxOutputFolder.Text = settings.OutputFolder;
            }

            switch (settings.OutputFileType)
            {
                case -1:
                    radioButtonUnchanged.Checked = true;
                    break;
                case 0:
                    radioButtonType0.Checked = true;
                    break;
                case 1:
                    radioButtonType1.Checked = true;
                    break;
            }
        }

        private void UpdateSettings()
        {
            Settings settings = Settings.Default;

            if(radioButtonUnchanged.Checked)
                settings.OutputFileType = -1;
            else if (radioButtonType0.Checked)
                settings.OutputFileType = 0;
            else if (radioButtonType1.Checked)
                settings.OutputFileType = 1;
            
            settings.OutputFolder = textBoxOutputFolder.Text;
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
            get { return Resources.OptionsPageHeading; }
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
            if (!CheckOutputFolderExists())
                return false;
            if (!CheckOutputFolderIsEmpty())
                return false;
            if (!CheckOutputFolderIsValid())
                return false;

            UpdateSettings();
            
            return true;
        }

        private bool CheckOutputFolderIsValid()
        {
            if (Settings.Default.UseInputFolder)
            {
                if (textBoxOutputFolder.Text.StartsWith(Settings.Default.InputFolder))
                {
                    MessageBox.Show("Your output folder cannot be contained in your input folder",
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private bool CheckOutputFolderExists()
        {
            if (!Directory.Exists(textBoxOutputFolder.Text))
            {
                DialogResult result = MessageBox.Show("Your selected output folder does not exist.\r\nWould you like to create it now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Directory.CreateDirectory(textBoxOutputFolder.Text);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckOutputFolderIsEmpty()
        {
            if ((Directory.GetFiles(textBoxOutputFolder.Text).Length > 0) ||
                (Directory.GetDirectories(textBoxOutputFolder.Text).Length > 0))
            {
                MessageBox.Show("Your output folder is not empty.\r\n" +
                    "You must select an empty folder to store the converted MIDI files.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
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

        public string InstructionsText
        {
            get { return Resources.OptionsPageInstructions; }
        }

        #endregion

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.SelectedPath = textBoxOutputFolder.Text;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputFolder.Text = folderBrowser.SelectedPath;
                if (CheckOutputFolderExists())
                {
                    CheckOutputFolderIsEmpty();
                }
            }
        }
    }
}
