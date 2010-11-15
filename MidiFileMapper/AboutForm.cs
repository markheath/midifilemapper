using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MarkHeath.MidiUtils
{
    public partial class AboutForm : Form
    {
        public AboutForm(string homepage)
        {
            InitializeComponent();
            labelProductName.Text = Application.ProductName;
            labelVersion.Text = String.Format("Version: {0}", Application.ProductVersion);
            labelCopyright.Text = GetCopyrightMessage();
            linkLabel1.Text = homepage;
            this.Text = String.Format("About {0}", Application.ProductName);
        }

        private string GetCopyrightMessage()
        {
            var copyright = (AssemblyCopyrightAttribute) Attribute.GetCustomAttribute(
                    Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute));
            return copyright.Copyright;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
    }
}
