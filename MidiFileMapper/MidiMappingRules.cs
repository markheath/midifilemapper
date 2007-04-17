using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.FileFormats.Map;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    public class MidiMappingRules
    {
        private Dictionary<string,string> generalProperties;
        private List<IEventRule> noteRules;
        private List<IEventRule> excludeRules;
        private List<IEventRule> eventRules;
        private List<MidiEvent> insertEvents;
        
        public const string RootElementName = "MidiMappingRules";
        public const string GeneralSettingsElementName = "General";
        
        // author
        // description
        // version
        // date
        // url
        // mode - first match / all matches

        // exclude settings:
        // Sysex, Start, Stop, End

        public MidiMappingRules()
        {
            generalProperties = new Dictionary<string, string>();
            eventRules = new List<IEventRule>();
            noteRules = new List<IEventRule>();
            insertEvents = new List<MidiEvent>();
            excludeRules = new List<IEventRule>();
        }

        public static MidiMappingRules LoadFromCakewalkDrumMap(string fileName)
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            
            CakewalkMapFile map = new CakewalkMapFile(fileName);
            foreach (CakewalkDrumMapping mapping in map.DrumMappings)
            {
                NoteMap mappingRule = new NoteMap();
                mappingRule.Name = mapping.NoteName;
                mappingRule.InNotes = new InputValueParameters(mapping.InNote);
                mappingRule.OutChannel = new NoteEventOutputParameters(mapping.Channel);
                mappingRule.OutNote = new NoteEventOutputParameters(mapping.OutNote);
                mappingRule.OutVelocity = new NoteEventOutputParameters(mapping.VelocityAdjust, (int) mapping.VelocityScale);
                // TODO: support out velocity scaling
                mappingRules.noteRules.Add(mappingRule);
            }
            return mappingRules;            
        }

        public static MidiMappingRules LoadFromCubaseDrumMap(string fileName)
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            if (xmlDocument.DocumentElement.Name != "DrumMap")
            {
                // this is some other type of XML file
                return null;
            }
            foreach (XmlNode mapNode in xmlDocument.DocumentElement.ChildNodes)
            {
                if (mapNode.Name == "List" && mapNode.Attributes["name"].Value == "Map")
                {
                    foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
                    {
                        NoteMap mappingRule = new NoteMap();
                        if (mapNode.Name == "INote")
                        {
                            mappingRule.InNotes = new InputValueParameters(mapNode.Attributes["value"].Value);
                        }
                        else if (mapNode.Name == "Channel")
                        {
                            mappingRule.OutChannel = new NoteEventOutputParameters((Int32.Parse(mapNode.Attributes["value"].Value) + 1).ToString(), 1, 16);
                        }
                        else if (mapNode.Name == "ONote")
                        {
                            mappingRule.OutNote = new NoteEventOutputParameters(mapNode.Attributes["value"].Value, 0, 127);
                        }
                        else if (mapNode.Name == "Name")
                        {
                            mappingRule.Name = mapNode.Attributes["value"].Value;
                        }

                        // TODO: consider support for:
                        // Length (float)  not exactly sure what this is - 200 a common value
                        // Mute  - mutes this note?
                        // DisplayNote - ?
                        // HeadSymbol
                        // Voice
                        // PortIndex
                        // QuantizeIndex

                        mappingRules.noteRules.Add(mappingRule);
                    }
                }
            }
            return mappingRules;
        }


        public static MidiMappingRules LoadFromXml(string fileName)
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            if (xmlDocument.DocumentElement.Name != RootElementName)
            {
                // this is some other type of XML file
                return null;
            }
            foreach (XmlNode ruleNode in xmlDocument.DocumentElement.ChildNodes)
            {
                if (ruleNode.Name == "NoteMap")
                {
                    NoteMap mappingRule = NoteMap.LoadFromXmlNode(ruleNode);
                    mappingRules.noteRules.Add(mappingRule);
                }
                else if (ruleNode.Name == "Exclude")
                {
                    ExcludeRule excludeRule = ExcludeRule.LoadFromXmlNode(ruleNode);
                    mappingRules.excludeRules.Add(excludeRule);
                }
                else if (ruleNode.Name == "ControllerMap")
                {
                    ControllerMap controllerMap = ControllerMap.LoadFromXmlNode(ruleNode);
                    mappingRules.eventRules.Add(controllerMap);
                }
                else if (ruleNode.Name == "AfterTouchMap")
                {
                    AfterTouchMap afterTouchMap = AfterTouchMap.LoadFromXmlNode(ruleNode);
                    mappingRules.eventRules.Add(afterTouchMap);
                }
                else if (ruleNode.Name == "PitchWheelMap")
                {
                    PitchWheelMap pitchWheelMap = PitchWheelMap.LoadFromXmlNode(ruleNode);
                    mappingRules.eventRules.Add(pitchWheelMap);
                }
                else if (ruleNode.Name == "Insert")
                {
                    InsertRule insertRule = InsertRule.LoadFromXmlNode(ruleNode);
                    mappingRules.insertEvents.Add(insertRule.Apply(null));
                }
                else if (ruleNode.Name == GeneralSettingsElementName)
                {
                    foreach (XmlAttribute attribute in ruleNode.Attributes)
                    {
                        mappingRules.generalProperties.Add(attribute.Name, attribute.Value);
                    }
                    // TODO: could check that property names are as expected and validate any
                    // non-string ones
                }
            }
            return mappingRules;
        }

        public bool Process(MidiEvent inEvent)
        {
            NoteOnEvent noteEvent = inEvent as NoteOnEvent;

            // filter note offs - they will be added by their note-on
            if (MidiEvent.IsNoteOff(inEvent))
            {
                return false;
            }

            // if it is a note event, special processing
            if(noteEvent != null)
            {

                foreach (IEventRule rule in noteRules)
                {
                    if (rule.Apply(inEvent))
                        return true;

                }
                // an unmatched note event
                // TODO: configure to have an option to retain these
                return false;
            }

            // now see if we need to exclude this event
            foreach (IEventRule rule in excludeRules)
            {
                if (rule.Apply(inEvent))
                {
                    return false;
                }
            }

            bool updatedEvent = false;
            foreach (IEventRule rule in eventRules)
            {
                updatedEvent |= rule.Apply(inEvent);
            }
            return true; // updatedEvent;
        }

        public List<IEventRule> NoteRules
        {
            get
            {
                return noteRules;
            }
        }

        public List<IEventRule> EventRules
        {
            get
            {
                return eventRules;
            }
        }

        public List<MidiEvent> InsertEvents
        {
            get
            {
                return insertEvents;
            }
        }


        public bool ConvertFile(string sourceFile, string destFile, int fileType)
        {
            MidiFile midiFile = new MidiFile(sourceFile);
            if (fileType == -1)
            {
                fileType = midiFile.FileFormat;
            }

            MidiEventCollection outputFileEvents = new MidiEventCollection(fileType,midiFile.DeltaTicksPerQuarterNote);
            bool hasNotes = false;
            for (int track = 0; track < midiFile.Tracks; track++)
            {
                IList<MidiEvent> trackEvents = midiFile.Events[track];
                IList<MidiEvent> outputEvents;
                if (fileType == 1 || track == 0)
                {
                    outputEvents = new List<MidiEvent>();
                }
                else
                {
                    outputEvents = outputFileEvents[0];
                }
                foreach (MidiEvent midiEvent in InsertEvents)
                {
                    outputEvents.Add(midiEvent);
                }
                foreach (MidiEvent midiEvent in trackEvents)
                {
                    if (Process(midiEvent))
                    {
                        outputEvents.Add(midiEvent);
                        NoteOnEvent noteOnEvent = midiEvent as NoteOnEvent;
                        if (noteOnEvent != null)
                        {
                            System.Diagnostics.Debug.Assert(noteOnEvent.OffEvent != null);
                            hasNotes = true;
                            outputEvents.Add(noteOnEvent.OffEvent);
                        }
                    }
                }
                if (fileType == 1 || track == 0)
                {
                    outputFileEvents.AddTrack(outputEvents);
                }
            }
            if (hasNotes)
            {
                MidiFile.Export(destFile, outputFileEvents);
            } 
                    
            return hasNotes;
        }
    }
}
