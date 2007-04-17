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
            pitchWheelMap.name = mappingNode.Attributes["Name"].Value ?? "";
            pitchWheelMap.inChannels = new InputValueParameters(mappingNode.Attributes["InChannel"].Value ?? "*");
            pitchWheelMap.outChannel = new NoteEventOutputParameters(mappingNode.Attributes["OutChannel"].Value ?? "*", 1, 16);
            pitchWheelMap.outValue = new NoteEventOutputParameters(mappingNode.Attributes["OutValue"].Value ?? "*", 0, 0x4000);
            return pitchWheelMap;
        }

        public bool Apply(MidiEvent inEvent)
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
