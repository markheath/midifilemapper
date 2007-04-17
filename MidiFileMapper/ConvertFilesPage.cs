using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MarkHeath.MidiUtils.Properties;
using NAudio.Utils;

namespace MarkHeath.MidiUtils
{
    partial class ConvertFilesPage : UserControl, IWizardPage
    {
        IWizardPage previousPage;
        IWizardPage nextPage;
        bool canMoveNext;
        bool canMovePrevious;
        bool canExitApp;
        bool workQueued;
        SelectFilesPage selectFilesPage;
        SelectMappingPage selectMappingPage;

        public ConvertFilesPage(SelectFilesPage selectFilesPage, SelectMappingPage selectMappingPage)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            canMoveNext = false;
            canMovePrevious = true;
            canExitApp = true;
            this.selectFilesPage = selectFilesPage;
            this.selectMappingPage = selectMappingPage;
        }

        #region IWizardPage Members

        public bool CanMoveNext
        {
            get { return canMoveNext; }
        }

        public bool CanMovePrevious
        {
            get { return canMovePrevious; }
        }

        public string NextButtonText
        {
            get { return Resources.FinishButtonText; }
        }

        public string PreviousButtonText
        {
            get { return Resources.PreviousButtonText; }
        }

        public string HeadingText
        {
            get { return Resources.ConvertFilesPageHeading; }
        }

        public bool CanExitApp
        {
            get { return canExitApp; }
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
            return true;
        }

        public string InstructionsText
        {
            get { return Resources.ConvertFilesPageInstructions; }
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

        private void buttonBegin_Click(object sender, EventArgs e)
        {
            if (workQueued)
            {
                MessageBox.Show("Please wait until the current operation has finished", Application.ProductName);
            }
            else
            {
                ProcessFilesThreadParams threadParams = new ProcessFilesThreadParams(
                    selectMappingPage.MappingRules,
                    selectFilesPage.SelectedFiles);
                progressLog1.ClearLog();
                workQueued = ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFilesThread), threadParams);
                if (workQueued)
                {
                    canExitApp = false;
                    canMoveNext = false;
                    canMovePrevious = false;
                    OnUpdated();
                }
            }
        }

        class ProcessFilesThreadParams
        {
            private MidiMappingRules mappingRules;
            private List<string> selectedFiles;

            public ProcessFilesThreadParams(MidiMappingRules mappingRules, List<string> selectedFiles)
            {
                this.mappingRules = mappingRules;
                this.selectedFiles = selectedFiles;
            }

            public MidiMappingRules MappingRules
            {
                get { return mappingRules; }
            }

            public List<string> SelectedFiles
            {
                get { return selectedFiles; }
            }
        }

        private void ProcessFilesThread(object state)
        {
            ProcessFilesThreadParams threadParams = state as ProcessFilesThreadParams;
            try
            {
                MidiFileMapper mapper;                
                if (Settings.Default.UseInputFolder)
                {
                    mapper = new MidiFileMapper(threadParams.MappingRules);
                }
                else
                {
                    mapper = new MidiFileMapper(threadParams.MappingRules, threadParams.SelectedFiles);
                }
                mapper.Progress += new EventHandler<ProgressEventArgs>(mapper_Progress);
                mapper.Start();
            }
            catch (Exception e)
            {
                progressLog1.ReportProgress(new ProgressEventArgs(ProgressMessageType.Error,"{0}", e.Message));
            }
            finally
            {
                workQueued = false;
                canExitApp = true;
                canMovePrevious = true;
                canMoveNext = true;
                OnUpdated();
            }
        }

        void mapper_Progress(object sender, ProgressEventArgs e)
        {
            progressLog1.ReportProgress(e);
        }
    }
}
