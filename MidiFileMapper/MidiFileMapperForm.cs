using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using MarkHeath.MidiUtils.Properties;
using NAudio.Utils;

namespace MarkHeath.MidiUtils
{
    public partial class MidiFileMapperForm : Form
    {
        private IWizardPage currentPage;
        private WelcomePage welcomePage;
        private SelectFolderPage selectFolderPage;
        private SelectFilesPage selectFilesPage;
        private SelectMappingPage selectMappingPage;
        private OptionsPage optionsPage;
        private ConvertFilesPage convertFilesPage;

        public MidiFileMapperForm()
        {
            InitializeComponent();
            // use the default size as the minimum
            this.MinimumSize = this.Size;
            if (Settings.Default.FirstTime)
            {
                UpgradeSettings();
            }

            CreateWizard();
        }

        private void UpgradeSettings()
        {
            Settings settings = Settings.Default;

            string productVersion = (string)settings.GetPreviousVersion("ProductVersion");
            if ((productVersion != null) && (productVersion.Length > 0))
            {
                settings.OutputFileType = (int)settings.GetPreviousVersion("OutputFileType");
                settings.OutputFolder = (string)settings.GetPreviousVersion("OutputFolder");
                settings.InputFolder = (string)settings.GetPreviousVersion("InputFolder");
                settings.CopyNonMidi = (bool)settings.GetPreviousVersion("CopyNonMidi");
                try
                {
                    settings.LastSelectedMapping = (string)settings.GetPreviousVersion("LastSelectedMapping");
                }
                catch (SettingsPropertyNotFoundException)
                {
                    // not a big deal, leave it at its default setting
                }
            }                            
        }
        


        private void CreateWizard()
        {
            selectFilesPage = new SelectFilesPage();
            selectFolderPage = new SelectFolderPage();
            welcomePage = new WelcomePage(selectFilesPage,selectFolderPage);
            selectMappingPage = new SelectMappingPage();
            optionsPage = new OptionsPage();
            convertFilesPage = new ConvertFilesPage(selectFilesPage, selectMappingPage);
            
            welcomePage.Updated += new EventHandler(WizardPageUpdated);
            selectFolderPage.Updated += new EventHandler(WizardPageUpdated);
            selectFilesPage.Updated += new EventHandler(WizardPageUpdated);
            selectMappingPage.Updated += new EventHandler(WizardPageUpdated);
            optionsPage.Updated += new EventHandler(WizardPageUpdated);
            convertFilesPage.Updated += new EventHandler(WizardPageUpdated);

            selectFolderPage.PreviousPage = welcomePage;
            selectFolderPage.NextPage = selectMappingPage;
            selectFilesPage.PreviousPage = welcomePage;
            selectFilesPage.NextPage = selectMappingPage;
            selectMappingPage.PreviousPage = selectFilesPage;
            selectMappingPage.NextPage = optionsPage;
            optionsPage.PreviousPage = selectMappingPage;
            optionsPage.NextPage = convertFilesPage;
            convertFilesPage.PreviousPage = optionsPage;            

            ShowWizardPage(welcomePage);
        }

        void WizardPageUpdated(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(WizardPageUpdated), new object[] { sender, e });
            }
            else
            {
                buttonNext.Enabled = currentPage.CanMoveNext;
                buttonPrevious.Enabled = currentPage.CanMovePrevious;
            }
        }

        private void ShowWizardPage(IWizardPage wizardPage)
        {
            panelCurrentPage.Controls.Clear();
            panelCurrentPage.Controls.Add(wizardPage.UserInterface);
            labelPageHeading.Text = wizardPage.HeadingText;
            labelPageInstructions.Text = wizardPage.InstructionsText;
            buttonNext.Text = wizardPage.NextButtonText;
            buttonPrevious.Text = wizardPage.PreviousButtonText;
            currentPage = wizardPage;
            WizardPageUpdated(this, EventArgs.Empty);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentPage.ValidatePage())
            {
                if (currentPage.NextPage != null)
                {
                    ShowWizardPage(currentPage.NextPage);
                }
                else
                {
                    // must be the last page
                    this.Close();
                }
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage.PreviousPage != null)
            {
                ShowWizardPage(currentPage.PreviousPage);
            }
        }

        private void MidiFileMapperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentPage.CanExitApp)
            {
                // avoid a second settings upgrade
                Settings.Default.FirstTime = false;
                Settings.Default.ProductVersion = Application.ProductVersion;
                Settings.Default.Save();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(SystemColors.ControlDarkDark))
            {
                e.Graphics.DrawLine(pen, 10, 0, panelFooter.Width - 10, 0);
            }
            using (Pen pen = new Pen(SystemColors.ControlLightLight))
            {
                e.Graphics.DrawLine(pen, 10, 1, panelFooter.Width - 10, 1);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("https://github.com/markheath/midifilemapper");
            aboutForm.ShowDialog();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpFilePath = "https://github.com/markheath/midifilemapper/wiki";
            try
            {
                System.Diagnostics.Process.Start(helpFilePath);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Could not display the help file", Application.ProductName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}