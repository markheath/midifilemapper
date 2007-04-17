using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MarkHeath.MidiUtils.Properties;

namespace MarkHeath.MidiUtils
{
    partial class WelcomePage : UserControl, IWizardPage
    {
        IWizardPage previousPage;
        IWizardPage nextPage;
        SelectFilesPage selectFilesPage;
        SelectFolderPage selectFolderPage;

        public WelcomePage(SelectFilesPage selectFilesPage, SelectFolderPage selectFolderPage)
        {
            InitializeComponent();
            this.selectFilesPage = selectFilesPage;
            this.selectFolderPage = selectFolderPage;
            if (Settings.Default.UseInputFolder)
            {
                radioButtonInputFolder.Checked = true;
                nextPage = selectFolderPage;
            }
            else
            {
                radioButtonInputFiles.Checked = true;
                nextPage = selectFilesPage;
            }
            
        }

        #region IWizardPage Members

        public bool CanMoveNext
        {
            get { return true; }
        }

        public bool CanMovePrevious
        {
            get { return false; }
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
            get { return Resources.WelcomePageHeading; }
        }

        public string InstructionsText
        {
            get { return Resources.WelcomePageInstructions; }
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
            Settings.Default.UseInputFolder = radioButtonInputFolder.Checked;
            if (Settings.Default.UseInputFolder)
            {
                nextPage = selectFolderPage;
            }
            else
            {
                nextPage = selectFilesPage;
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

        #endregion
    }
}
