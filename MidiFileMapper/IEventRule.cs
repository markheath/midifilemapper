using System;
using System.Collections.Generic;
using System.Text;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    public interface IEventRule
    {
        bool Apply(MidiEvent midiEvent);
    }
}
