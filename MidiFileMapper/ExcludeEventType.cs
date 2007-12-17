using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    enum ExcludeEventType
    {
        All,
        Controller,
        PitchWheel,
        PatchChange,
        ChannelAfterTouch,
        KeyAfterTouch,
        Copyright,
        Text,
        Marker,
        Sysex,
        NonStandard,
        SequencerSpecific,
        SmpteOffset,
        CuePoint,
        Lyric,
        DeviceName,
        ProgramName,
        TrackInstrumentName,
        TrackName,
        TrackSequenceNumber,
        ContinueSequence,
        StopSequence,
        StartSequence,
        TimingClock,
        KeySignature,
    }
}
