using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    class InsertRule
    {
        TextEventType eventType;
        string value;
        long time;

        public static InsertRule LoadFromXmlNode(XmlNode insertNode)
        {
            InsertRule insertRule = new InsertRule();
            insertRule.eventType = (TextEventType)Enum.Parse(typeof(TextEventType), XmlUtils.GetAttribute(insertNode,"EventType",""));
            insertRule.time = long.Parse(XmlUtils.GetAttribute(insertNode,"Time","0"));
            insertRule.value = XmlUtils.GetAttribute(insertNode,"Value","");
            return insertRule;
        }

        public MidiEvent Apply(MidiEvent inEvent)
        {
            switch (eventType)
            {
                case TextEventType.Copyright:
                    return new TextEvent(value, MetaEventType.Copyright, time);
                case TextEventType.Marker:
                    return new TextEvent(value, MetaEventType.Marker, time);
                case TextEventType.Text:
                    return new TextEvent(value, MetaEventType.TextEvent, time);
                // don't support insert sequence track name
            }
            return null;
        }
    }

    public enum TextEventType
    {
        Text,
        Marker,
        Copyright,
        TrackName
    }
}
