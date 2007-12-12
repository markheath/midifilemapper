using System;
using System.Collections.Generic;
using System.Text;
using MarkHeath.MidiUtils;
using NUnit.Framework;
using NAudio.Midi;

namespace MappingTests
{
    [TestFixture]    
    public class TextMapTests
    {
        [Test]
        public void TrackNameMap()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            mappingRules.EventRules.Add(CreateTextMap(MetaEventType.SequenceTrackName, "{FILENAME}"));
            string outFileName ="BlahblahBlah"; 
            EventRuleArgs args = new EventRuleArgs(outFileName);

            CheckTextMap("Hello World", outFileName, MetaEventType.SequenceTrackName, mappingRules, args);
        }

        [Test]
        public void ChangeTextEvent()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            string outText = "out text";
            mappingRules.EventRules.Add(CreateTextMap(MetaEventType.TextEvent, outText));

            CheckTextMap("Hello World", outText, MetaEventType.TextEvent, mappingRules, new EventRuleArgs(""));
        }

        private TextMap CreateTextMap(MetaEventType eventType, string outValue)
        {
            TextMap textMap = new TextMap();
            textMap.EventType = eventType;
            textMap.OutValue = outValue;            
            return textMap;
        }

        private void CheckTextMap(string inText, string outText, MetaEventType eventType, MidiMappingRules mappingRules, EventRuleArgs args)
        {
            TextEvent textEvent = new TextEvent(inText, eventType, 0);
            bool matched = mappingRules.Process(textEvent, args);
            Assert.IsTrue(matched);
            Assert.AreEqual(textEvent.Text, outText);
            Assert.AreEqual(textEvent.MetaEventType, eventType);
        }
    }
}
