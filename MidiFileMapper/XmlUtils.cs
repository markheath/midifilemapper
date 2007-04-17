using System;
using System.Xml;

namespace MarkHeath.MidiUtils
{
    public class XmlUtils
    {
        public static string GetAttribute(XmlNode node, string attributeName, string defaultValue)
        {
            XmlAttribute attribute = node.Attributes[attributeName];
            if (attribute != null)
                return attribute.Value;
            else
                return defaultValue;
        }
    }
}