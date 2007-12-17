using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    class PitchWheelMap : IEventRule
    {
        private string name;
        private InputValueParameters inChannels;
        private NoteEventOutputParameters outChannel;
        private NoteEventOutputParameters outValue;

        public static PitchWheelMap LoadFromXmlNode(XmlNode mappingNode)
        {
            PitchWheelMap pitchWheelMap = new PitchWheelMap();
            pitchWheelMap.name = XmlUtils.GetAttribute(mappingNode,"Name","");
            pitchWheelMap.inChannels = new InputValueParameters(XmlUtils.GetAttribute(mappingNode,"InChannel","*"));
            pitchWheelMap.outChannel = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutChannel","*"), 1, 16);
            pitchWheelMap.outValue = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutValue","*"), 0, 0x4000);
            return pitchWheelMap;
        }

        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
        {
            bool match = false;
            if (inEvent.CommandCode == MidiCommandCode.PitchWheelChange)
            {
                PitchWheelChangeEvent pitchWheelEvent = (PitchWheelChangeEvent)inEvent;
                if (inChannels.IsValueIncluded(pitchWheelEvent.Channel))
                {
                    pitchWheelEvent.Pitch = outValue.ProcessValue(pitchWheelEvent.Pitch);
                    pitchWheelEvent.Channel = outChannel.ProcessValue(pitchWheelEvent.Channel);
                    match = true;
                }
            }
            return match;
        }

    }
}
