using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MarkHeath.MidiUtils
{
    public partial class UnhandledExceptionForm : Form
    {
        public static void Show(Exception e)
        {
            UnhandledExceptionForm form = new UnhandledExceptionForm(e);
            form.ShowDialog();
        }

        public UnhandledExceptionForm(Exception e)
        {
            InitializeComponent();
            textBoxMessage.Text = e.ToString();
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxMessage.Text);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
