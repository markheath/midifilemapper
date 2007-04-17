using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    public interface IWizardPage
    {
        bool CanMoveNext { get; }
        bool CanMovePrevious { get; }
        string NextButtonText { get; }
        string PreviousButtonText { get; }
        string HeadingText { get; }
        string InstructionsText { get; }
        bool CanExitApp { get; }
        System.Windows.Forms.Control UserInterface { get; }
        IWizardPage PreviousPage { get; set; }
        IWizardPage NextPage { get; set; }
        bool ValidatePage();
        event EventHandler Updated;
    }
}
