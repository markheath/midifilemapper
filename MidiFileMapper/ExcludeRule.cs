using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    class ExcludeRule : IEventRule
    {
        InputValueParameters inChannels;
        ExcludeEventType eventType;

        public static ExcludeRule LoadFromXmlNode(XmlNode excludeNode)
        {
            ExcludeRule excludeRule = new ExcludeRule();
            excludeRule.eventType = (ExcludeEventType)Enum.Parse(typeof(ExcludeEventType),(XmlUtils.GetAttribute(excludeNode,"EventType","All")));
            excludeRule.inChannels = new InputValueParameters(XmlUtils.GetAttribute(excludeNode,"Channel","*"));
            return excludeRule;
        }

        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
        {
            bool exclude = false;
            MetaEvent metaEvent = inEvent as MetaEvent;
            switch (eventType)
            {
                case ExcludeEventType.Controller:
                    exclude = inEvent.CommandCode == MidiCommandCode.ControlChange;
                    break;
                case ExcludeEventType.PitchWheel:
                    exclude = inEvent.CommandCode == MidiCommandCode.PitchWheelChange;
                    break;
                case ExcludeEventType.PatchChange:
                    exclude = inEvent.CommandCode == MidiCommandCode.PatchChange;
                    break;
                case ExcludeEventType.ChannelAfterTouch:
                    exclude = inEvent.CommandCode == MidiCommandCode.ChannelAfterTouch;
                    break;
                case ExcludeEventType.KeyAfterTouch:
                    exclude = inEvent.CommandCode == MidiCommandCode.KeyAfterTouch;
                    break;
                case ExcludeEventType.Sysex:
                    exclude = inEvent.CommandCode == MidiCommandCode.Sysex;
                    break;
                case ExcludeEventType.ContinueSequence:
                    exclude = inEvent.CommandCode == MidiCommandCode.ContinueSequence;
                    break;
                case ExcludeEventType.StopSequence:
                    exclude = inEvent.CommandCode == MidiCommandCode.StopSequence;
                    break;
                case ExcludeEventType.StartSequence:
                    exclude = inEvent.CommandCode == MidiCommandCode.StartSequence;
                    break;
                case ExcludeEventType.TimingClock:
                    exclude = inEvent.CommandCode == MidiCommandCode.TimingClock;
                    break;
                case ExcludeEventType.Copyright:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.Copyright);
                    break;
                case ExcludeEventType.Text:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.TextEvent);
                    break;
                case ExcludeEventType.Marker:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.Marker);
                    break;
                case ExcludeEventType.SequencerSpecific:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.SequencerSpecific);
                    break;
                case ExcludeEventType.SmpteOffset:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.SmpteOffset);
                    break;
                case ExcludeEventType.CuePoint:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.CuePoint);
                    break;
                case ExcludeEventType.Lyric:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.Lyric);
                    break;
                case ExcludeEventType.DeviceName:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.DeviceName);
                    break;
                case ExcludeEventType.ProgramName:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.ProgramName);
                    break;
                case ExcludeEventType.TrackInstrumentName:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.TrackInstrumentName);
                    break;
                case ExcludeEventType.SequenceTrackName:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.SequenceTrackName);
                    break;
                case ExcludeEventType.TrackSequenceNumber:
                    exclude = (metaEvent != null) && (metaEvent.MetaEventType == MetaEventType.TrackSequenceNumber);
                    break;
                case ExcludeEventType.NonStandard:
                    if(metaEvent != null)
                    {
                        exclude = !Enum.IsDefined(typeof(MetaEventType),metaEvent.MetaEventType);
                    }
                    else
                    {
                        exclude = !Enum.IsDefined(typeof(MidiCommandCode),inEvent.CommandCode);
                    }
                    break;
                case ExcludeEventType.All:
                    // End track and notes already excluded
                    if (metaEvent != null)
                    {
                        if ((metaEvent.MetaEventType != MetaEventType.SetTempo) &&
                            (metaEvent.MetaEventType != MetaEventType.KeySignature) &&
                            (metaEvent.MetaEventType != MetaEventType.TimeSignature))
                        {
                        }
                        else
                        {
                            exclude = true;
                        }

                    }
                    else
                    {
                        exclude = true;
                    }
                    // TODO: support exclude all
                    break;
            }
            if (exclude && metaEvent == null)
            {
                exclude = inChannels.IsValueIncluded(inEvent.Channel);                
            }
            return exclude;
        }
    }
}
