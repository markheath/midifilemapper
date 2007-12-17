using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    public class NoteMap : IEventRule
    {
        private string name;
        private InputValueParameters inNotes;
        private InputValueParameters inChannels;
        private InputValueParameters inVelocity;
        private NoteEventOutputParameters outNote;
        private NoteEventOutputParameters outChannel;
        private NoteEventOutputParameters outVelocity;
        private NoteEventOutputParameters outDuration;
        //private NoteEventOutputParameters outStartTime;

        public static NoteMap LoadFromXmlNode(XmlNode mappingNode)
        {
            NoteMap mappingRule = new NoteMap();
            mappingRule.name = XmlUtils.GetAttribute(mappingNode,"Name","");
            mappingRule.inNotes = new InputValueParameters(XmlUtils.GetAttribute(mappingNode, "InNote", "*"));
            mappingRule.inChannels = new InputValueParameters(XmlUtils.GetAttribute(mappingNode, "InChannel", "*"));
            mappingRule.inVelocity = new InputValueParameters(XmlUtils.GetAttribute(mappingNode, "InVelocity", "*"));
            mappingRule.outNote = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode, "OutNote", "*"), 0, 127);
            mappingRule.outChannel = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode, "OutChannel", "*"), 1, 16);
            mappingRule.outVelocity = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode, "OutVelocity", "*"), 1, 127);
            mappingRule.outDuration = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode, "OutDuration", "*"), 0, Int32.MaxValue);
            // TODO: disable absolute value setting                        
            //mappingRule.outStartTime = new NoteEventOutputParameters(mappingNode.Attributes["OutStartTime"].Value ?? "*", 0, Int32.MaxValue);
            return mappingRule;
        }

        public NoteMap()
        {
            inNotes = new InputValueParameters("*");
            inChannels = new InputValueParameters("*");
            inVelocity = new InputValueParameters("*");
            outNote = new NoteEventOutputParameters("*", 0, 127);
            outChannel = new NoteEventOutputParameters("*", 1, 16);
            outVelocity = new NoteEventOutputParameters("*", 1, 127);
            outDuration = new NoteEventOutputParameters("*", 0, Int32.MaxValue);
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public InputValueParameters InNotes
        {
            get { return inNotes; }
            set { inNotes = value; }
        }

        public InputValueParameters InChannels
        {
            get { return inChannels; }
            set { inChannels = value; }
        }

        public InputValueParameters InVelocity
        {
            get { return inVelocity; }
            set { inVelocity = value; }
        }

        public NoteEventOutputParameters OutNote
        {
            get { return outNote; }
            set { outNote = value; }
        }

        public NoteEventOutputParameters OutChannel
        {
            get { return outChannel; }
            set { outChannel = value; }
        }

        public NoteEventOutputParameters OutVelocity
        {
            get { return outVelocity; }
            set { outVelocity = value; }
        }

        public NoteEventOutputParameters OutDuration
        {
            get { return outDuration; }
            set { outDuration = value; }
        }

        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
        {
            bool match = false;
            NoteOnEvent noteOnEvent = inEvent as NoteOnEvent;
            if (noteOnEvent != null && noteOnEvent.Velocity > 0)
            {
                if (inChannels.IsValueIncluded(inEvent.Channel)
                    && inNotes.IsValueIncluded(noteOnEvent.NoteNumber)
                    && inVelocity.IsValueIncluded(noteOnEvent.Velocity)
                    )
                {
                    noteOnEvent.Channel = outChannel.ProcessValue(noteOnEvent.Channel);
                    noteOnEvent.Velocity = outVelocity.ProcessValue(noteOnEvent.Velocity);
                    noteOnEvent.NoteNumber = outNote.ProcessValue(noteOnEvent.NoteNumber);
                    //noteOnEvent.AbsoluteTime = outStartTime.ProcessValue(noteOnEvent.AbsoluteTime);
                    noteOnEvent.NoteLength = outDuration.ProcessValue(noteOnEvent.NoteLength);
                    match = true;
                }
            }

            return match;
        }
    }
}
