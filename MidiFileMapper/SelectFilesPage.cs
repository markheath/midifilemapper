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
    partial class SelectFilesPage : UserControl, IWizardPage
    {
        IWizardPage previousPage;
        IWizardPage nextPage;

        public SelectFilesPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        #region IWizardPage Members

        public bool CanMoveNext
        {
            get { return true; }
        }

        public bool ValidatePage()
        {
            if (listBoxFilesToConvert.Items.Count == 0)
            {
                MessageBox.Show(
                    "Please select some files to convert first", 
                    Application.ProductName, 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
            get { return Resources.SelectFilesPageHeading; }
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

        public string InstructionsText
        {
            get { return Resources.SelectFilesPageInstructions; }
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Standard MIDI Files (*.mid)|*.mid";
            openFileDialog.Multiselect = true;            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddFiles(openFileDialog.FileNames);
            }
        }

        private void AddFiles(string[] fileNames)
        {
            bool error = false;
            foreach (string fileName in fileNames)
            {
                if (System.IO.Path.GetExtension(fileName).ToLower() == ".mid")
                {
                    if (!listBoxFilesToConvert.Items.Contains(fileName))
                    {
                        listBoxFilesToConvert.Items.Add(fileName);
                    }
                }
                else
                {
                    error = true;
                }
            }
            if (error)
            {
                MessageBox.Show(
                    "You can only process MIDI files", 
                    Application.ProductName, 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void listBoxFilesToConvert_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            AddFiles(files);
        }

        private void listBoxFilesToConvert_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            
            if((listBoxFilesToConvert.Items.Count > 0) &&
                (listBoxFilesToConvert.SelectedItems.Count == 0))
            {
                MessageBox.Show(
                    "Select the files you want to remove first.",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            while (listBoxFilesToConvert.SelectedItems.Count > 0)
            {
                listBoxFilesToConvert.Items.Remove(listBoxFilesToConvert.SelectedItems[0]);
            }
        }

        public List<string> SelectedFiles
        {
            get
            {
                List<string> selectedFiles = new List<string>();
                foreach (string fileName in listBoxFilesToConvert.Items)
                {
                    selectedFiles.Add(fileName);
                }
                return selectedFiles;
            }
        }
    }
}
