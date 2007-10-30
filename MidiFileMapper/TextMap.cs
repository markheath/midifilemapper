using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;


namespace MarkHeath.MidiUtils
{
    public class TextMap : IEventRule
    {
        private string name;
        private string outValue;
        private MetaEventType eventType;

        private const string existingValue = "{VALUE}";
        private const string fileName = "{FILENAME}";

        public static TextMap LoadFromXmlNode(XmlNode mappingNode)
        {
            TextMap mappingRule = new TextMap();
            mappingRule.name = XmlUtils.GetAttribute(mappingNode,"Name","");
            mappingRule.eventType = InsertEventTypeToMetaEventType((TextEventType)Enum.Parse(typeof(TextEventType), XmlUtils.GetAttribute(mappingNode, "EventType", "")));
            mappingRule.outValue = XmlUtils.GetAttribute(mappingNode, "OutValue", existingValue);

            return mappingRule;
        }

        public TextMap()
        {
            name = "";
            outValue = existingValue;
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string OutValue
        {
            get { return outValue; }
            set { outValue = value; }
        }

        public MetaEventType EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
        {
            bool match = false;

            TextEvent textEvent = inEvent as TextEvent;
            if (textEvent != null && textEvent.MetaEventType == eventType)
            {
                textEvent.Text = ProcessText(textEvent.Text,args);
                match = true;
            }

            return match;
        }

        private string ProcessText(string oldValue, EventRuleArgs args)
        {
            string processed = outValue.Replace(existingValue, oldValue);
            processed = processed.Replace(fileName,args.OutFileName);
            return processed;
        }

        public static MetaEventType InsertEventTypeToMetaEventType(TextEventType type)
        {
            switch (type)
            {
                case TextEventType.Copyright:
                    return MetaEventType.Copyright;
                case TextEventType.Marker:
                    return MetaEventType.Marker;
                case TextEventType.Text:
                    return MetaEventType.TextEvent;
                case TextEventType.TrackName:
                    return MetaEventType.SequenceTrackName;
                default:
                    throw new NotSupportedException(String.Format("Unsupported Event Type {0}",type));

            }
        }
    }
}
