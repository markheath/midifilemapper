using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    class AfterTouchMap : IEventRule
    {
        private string name;
        private AfterTouchType type;
        private InputValueParameters inChannels;
        private NoteEventOutputParameters outChannel;
        private NoteEventOutputParameters outValue;

        public static AfterTouchMap LoadFromXmlNode(XmlNode mappingNode)
        {
            AfterTouchMap afterTouchMap = new AfterTouchMap();
            afterTouchMap.name = XmlUtils.GetAttribute(mappingNode,"Name","");
            afterTouchMap.type = (AfterTouchType)Enum.Parse(typeof(AfterTouchType), XmlUtils.GetAttribute(mappingNode,"Type","Channel"));
            afterTouchMap.inChannels = new InputValueParameters(XmlUtils.GetAttribute(mappingNode,"InChannel","*"));
            afterTouchMap.outChannel = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutChannel","*"), 1, 16);
            afterTouchMap.outValue = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutValue","*"), 0, 127);
            return afterTouchMap;
        }

        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
        {
            bool match = false;
            if ((inEvent.CommandCode == MidiCommandCode.ChannelAfterTouch) 
                && (type == AfterTouchType.Channel))
            {
                if(inChannels.IsValueIncluded(inEvent.Channel))
                {
                    ChannelAfterTouchEvent afterTouchEvent = (ChannelAfterTouchEvent)inEvent;
                    afterTouchEvent.Channel = outChannel.ProcessValue(inEvent.Channel);
                    afterTouchEvent.AfterTouchPressure = outValue.ProcessValue(afterTouchEvent.AfterTouchPressure);
                    match = true;
                }
            }
            else if ((inEvent.CommandCode == MidiCommandCode.KeyAfterTouch) && (type == AfterTouchType.Key))
            {
                if (inChannels.IsValueIncluded(inEvent.Channel))
                {
                    NoteEvent afterTouchEvent = (NoteEvent)inEvent;
                    afterTouchEvent.Channel = outChannel.ProcessValue(inEvent.Channel);
                    afterTouchEvent.Velocity = outValue.ProcessValue(afterTouchEvent.Velocity);
                    match = true;
                }
            }

            return match;
        }

    }

    enum AfterTouchType
    {
        Channel,
        Key
    }
}
