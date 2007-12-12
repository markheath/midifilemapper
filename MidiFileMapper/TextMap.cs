using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;
using System.Text.RegularExpressions;


namespace MarkHeath.MidiUtils
{
    public class TextMap : IEventRule
    {
        private Regex inRegex;
        private string inValue;
        private const string existingValue = "{VALUE}";
        private const string fileName = "{FILENAME}";

        public static TextMap LoadFromXmlNode(XmlNode mappingNode)
        {
            TextMap mappingRule = new TextMap();
            mappingRule.Name = XmlUtils.GetAttribute(mappingNode,"Name","");
            mappingRule.EventType = InsertEventTypeToMetaEventType((TextEventType)Enum.Parse(typeof(TextEventType), XmlUtils.GetAttribute(mappingNode, "EventType", "")));
            mappingRule.OutValue = XmlUtils.GetAttribute(mappingNode, "OutValue", existingValue);
            mappingRule.InValue = XmlUtils.GetAttribute(mappingNode, "InValue", "");

            return mappingRule;
        }

        public TextMap()
        {
            Name = "";
            OutValue = existingValue;
            InValue = "";
            EventType = MetaEventType.TextEvent;
        }
        
        public string OutValue { get; set; }
        public string Name { get; set; }
        public MetaEventType EventType { get; set; }

        public string InValue 
        { 
            get
            {
                return inValue;
            }
            set
            {
                inValue = value;
                inRegex = new Regex(inValue);
            }
        }
        

        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
        {
            bool match = false;

            TextEvent textEvent = inEvent as TextEvent;
            if (textEvent != null && textEvent.MetaEventType == EventType)
            {
                if (textEvent.Text.Length == 0 || inRegex.Match(textEvent.Text).Success)
                {
                    textEvent.Text = ProcessText(textEvent.Text, args);
                    match = true;
                }
            }

            return match;
        }

        private string ProcessText(string oldValue, EventRuleArgs args)
        {
            string processed = OutValue.Replace(existingValue, oldValue);
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
