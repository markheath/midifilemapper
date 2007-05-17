using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using MarkHeath.MidiUtils.Properties;

namespace MarkHeath.MidiUtils
{
    partial class SelectMappingPage : UserControl, IWizardPage
    {
        class ComboItem
        {
            string name;
            string fileName;

            public ComboItem(string name, string fileName)
            {
                this.name = name;
                this.fileName = fileName;
            }

            public string Name 
            { 
                get { return name; } 
            }

            public string FileName
            {
                get { return fileName; }
            }

            public override string ToString()
            {
                return name;
            }
        }

        IWizardPage previousPage;
        IWizardPage nextPage;
        MidiMappingRules mappingRules;

        public SelectMappingPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            PopulateMappingCombo();
        }

        private void PopulateMappingCombo()
        {            
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            foreach (string xmlFile in Directory.GetFiles(appPath, "*.xml"))
            {
                AddXmlMappingFile(xmlFile);
            }
            // add from Cakewalk directory
            AddSonarDrumMaps();
            // add from working directory
            AddSonarDrumMaps(appPath);
            // TODO: Cubase drum maps from Cubase directory
            AddCubaseDrumMaps(appPath);

            // make a selection
            foreach (ComboItem comboItem in listBoxMapping.Items)
            {
                if (comboItem.FileName.Equals(Settings.Default.LastSelectedMapping, StringComparison.OrdinalIgnoreCase))
                {
                    listBoxMapping.SelectedItem = comboItem;
                    break;
                }
            }
            if (listBoxMapping.SelectedIndex == -1 && listBoxMapping.Items.Count > 0)
            {
                listBoxMapping.SelectedIndex = 0;
            }
        }

        private void AddSonarDrumMaps()
        {
            //HKEY_CURRENT_USER\Software\Cakewalk Music Software\SONAR Studio\5.0\UserPaths\DrumMapFolder
            using (RegistryKey cakewalkKey = Registry.CurrentUser.OpenSubKey("Software\\Cakewalk Music Software"))
            {
                if (cakewalkKey != null)
                {
                    foreach (string keyName in cakewalkKey.GetSubKeyNames())
                    {
                        if (keyName.ToLower().StartsWith("sonar"))
                        {
                            using (RegistryKey sonarKey = cakewalkKey.OpenSubKey(keyName))
                            {
                                foreach (string versionKeyName in sonarKey.GetSubKeyNames())
                                {
                                    using (RegistryKey versionKey = sonarKey.OpenSubKey(versionKeyName + "\\UserPaths"))
                                    {
                                        if (versionKey != null)
                                        {
                                            string drumMapPath = (string)versionKey.GetValue("DrumMapFolder");
                                            if(Directory.Exists(drumMapPath))
                                            {
                                                AddSonarDrumMaps(drumMapPath);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddSonarDrumMaps(string drumMapPath)
        {
            foreach (string drumMap in Directory.GetFiles(drumMapPath, "*.map"))
            {
                string name = "[SONAR Map] " + Path.GetFileNameWithoutExtension(drumMap);
                listBoxMapping.Items.Add(new ComboItem(name, drumMapPath));
            }
        }

        private void AddCubaseDrumMaps(string drumMapPath)
        {
            foreach (string drumMap in Directory.GetFiles(drumMapPath, "*.drm"))
            {
                string name = "[Cubase Map] " + Path.GetFileNameWithoutExtension(drumMap);
                listBoxMapping.Items.Add(new ComboItem(name, drumMapPath));
            }
        }


        private bool AddXmlMappingFile(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            if (xmlDoc.DocumentElement.Name == MidiMappingRules.RootElementName)
            {
                string name = Path.GetFileNameWithoutExtension(fileName);

                XmlNode nameNode = xmlDoc.DocumentElement.SelectSingleNode(
                    "//" + MidiMappingRules.RootElementName + "/" +
                    MidiMappingRules.GeneralSettingsElementName + "/Name");
                if (nameNode != null)
                {
                    name = nameNode.InnerText;
                }
                listBoxMapping.Items.Add(new ComboItem(name, fileName));
                return true;
            }
            return false;
        }

        #region IWizardPage Members

        public bool CanMoveNext
        {
            get 
            {
                return true;
            }

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
            get { return Resources.SelectMappingPageHeading; }
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
            ComboItem selectedItem = (ComboItem)listBoxMapping.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select a mapping file to use first",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (selectedItem.FileName.ToLower().EndsWith(".xml"))
                    this.mappingRules = MidiMappingRules.LoadFromXml(selectedItem.FileName);
                else if (selectedItem.FileName.ToLower().EndsWith(".map"))
                    this.mappingRules = MidiMappingRules.LoadFromCakewalkDrumMap(selectedItem.FileName);
                else if (selectedItem.FileName.ToLower().EndsWith(".drm"))
                    this.mappingRules = MidiMappingRules.LoadFromCubaseDrumMap(selectedItem.FileName);
                else
                    throw new NotSupportedException(String.Format("{0} is not a supported map format", selectedItem.FileName));
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    String.Format("Error Loading Map\r\n{0}", e.Message), 
                    Application.ProductName,
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return false;
            }
            Settings.Default.LastSelectedMapping = selectedItem.FileName;
            return true;
        }

        public string InstructionsText
        {
            get { return Resources.SelectMappingPageInstructions; }
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

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MIDI File Mapper (*.xml)|*.xml|" +
                "Cakewalk Drum Maps (*.map)|*.map|" +
                "Cubase Drum Maps (*.drm)|*.drm|" +
                "All Supported Types|*.xml;*.drm;*.map";
            openFileDialog.FilterIndex = 4;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile =  openFileDialog.FileName;
                foreach (ComboItem item in listBoxMapping.Items)
                {
                    if (item.FileName == selectedFile)
                    {
                        listBoxMapping.SelectedItem = item;
                        return;
                    }
                }
                bool added = false;
                if(selectedFile.ToLower().EndsWith(".xml"))
                {
                    added = AddXmlMappingFile(selectedFile);
                }
                else
                {
                    // TODO: validate the .map or .drm file
                    ComboItem item = new ComboItem(Path.GetFileNameWithoutExtension(selectedFile), selectedFile);
                    listBoxMapping.Items.Add(item);
                    listBoxMapping.SelectedItem = item;
                    added = true;
                }
                if (!added)
                {
                    MessageBox.Show("The selected file was not recognised as a valid map",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public MidiMappingRules MappingRules
        {
            get
            {
                return mappingRules;
            }

        }
    }
}
