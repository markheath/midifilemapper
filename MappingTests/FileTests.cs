using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MarkHeath.MidiUtils;
using NAudio.Midi;

namespace MappingTests
{
    [TestFixture]
    public class FileTests
    {
        [SetUp]
        public void Setup()
        {
            System.IO.Directory.CreateDirectory(".\\OutputFiles");
        }
        
        [Test]
        public void TestBundledMappings()
        {
            // simply tests that the bundled mapping files can be loaded
            string mappingPath = ".\\..\\..\\..\\MidiFileMapper\\";
            foreach(string file in System.IO.Directory.GetFiles(mappingPath,"*.xml"))
            {                
                System.Diagnostics.Debug.WriteLine(String.Format("Opening {0}",file));
                MidiMappingRules mappingRules = MidiMappingRules.LoadFromXml(file);
            }
        }

        [Test]
        public void TrackName()
        {
            string mappingFileName = ".\\TestFiles\\TrackNameToFilename.xml";
            MidiMappingRules mappingRules = MidiMappingRules.LoadFromXml(mappingFileName);
            string inFileName = ".\\TestFiles\\SimpleTestType0.mid";
            string outFileName = ".\\OutputFiles\\SimpleTestType0Track.mid";
            bool converted = mappingRules.ConvertFile(inFileName, outFileName, -1);
            Assert.IsTrue(converted,"Failed to convert the file");

            MidiFile outMidiFile = new MidiFile(outFileName);
            bool foundTrackName = false;
            foreach (MidiEvent midiEvent in outMidiFile.Events[0])
            {
                TextEvent textEvent = midiEvent as TextEvent;
                if (textEvent != null && textEvent.MetaEventType == MetaEventType.SequenceTrackName)
                {
                    foundTrackName = true;
                    Assert.AreEqual(textEvent.Text, "SimpleTestType0");
                }
            }
            Assert.IsTrue(foundTrackName,"Didn't find an updated TrackName");
        }



        [Test]
        public void TransposeTestType0()
        {
            string mappingFileName = ".\\TestFiles\\Transpose2SemitonesUp.xml";
            string inFileName = ".\\TestFiles\\SimpleTestType0.mid";
            string outFileName = ".\\OutputFiles\\SimpleTestType0_1.mid";
            TransposeTest(mappingFileName, inFileName, outFileName);
        }

        [Test]
        public void TransposeTestType1()
        {
            string mappingFileName = ".\\TestFiles\\Transpose2SemitonesUp.xml";
            string inFileName = ".\\TestFiles\\SimpleTestType1.mid";
            string outFileName = ".\\OutputFiles\\SimpleTestType1_1.mid";
            TransposeTest(mappingFileName, inFileName, outFileName);
        }

        private void TransposeTest(string mappingFileName, string inFileName, string outFileName)
        {
            MidiMappingRules mappingRules = MidiMappingRules.LoadFromXml(mappingFileName);
            bool converted = mappingRules.ConvertFile(inFileName, outFileName, -1);
            Assert.IsTrue(converted);
            MidiFile inMidiFile = new MidiFile(inFileName);
            MidiFile outMidiFile = new MidiFile(outFileName);
            Assert.AreEqual(inMidiFile.DeltaTicksPerQuarterNote, outMidiFile.DeltaTicksPerQuarterNote);
            Assert.AreEqual(inMidiFile.Tracks, outMidiFile.Tracks);
            Assert.AreEqual(inMidiFile.FileFormat, outMidiFile.FileFormat);
            IList<MidiEvent> inEvents = inMidiFile.Events[0];
            IList<MidiEvent> outEvents = outMidiFile.Events[0];
            Assert.AreEqual(inEvents.Count, outEvents.Count);
        }
    }
}

