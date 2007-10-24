using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    class InsertRule
    {
        InsertEventType eventType;
        string value;
        long time;

        public static InsertRule LoadFromXmlNode(XmlNode insertNode)
        {
            InsertRule insertRule = new InsertRule();
            insertRule.eventType = (InsertEventType)Enum.Parse(typeof(InsertEventType), XmlUtils.GetAttribute(insertNode,"EventType",""));
            insertRule.time = long.Parse(XmlUtils.GetAttribute(insertNode,"Time","0"));
            insertRule.value = XmlUtils.GetAttribute(insertNode,"Value","");
            return insertRule;
        }

        public MidiEvent Apply(MidiEvent inEvent)
        {
            switch (eventType)
            {
                case InsertEventType.Copyright:
                    return new TextEvent(value, MetaEventType.Copyright, time);
                case InsertEventType.Marker:
                    return new TextEvent(value, MetaEventType.Marker, time);
                case InsertEventType.Text:
                    return new TextEvent(value, MetaEventType.TextEvent, time);
            }
            return null;
        }
    }

    enum InsertEventType
    {
        Text,
        Marker,
        Copyright
    }
}
