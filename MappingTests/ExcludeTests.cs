using System;
using System.Collections.Generic;
using System.Text;
using MarkHeath.MidiUtils;
using NUnit.Framework;
using NAudio.Midi;
using System.IO;

namespace MappingTests
{
    [TestFixture]
    public class ExcludeTests
    {
        [Test]
        public void ExcludeRule()
        {
            string mappingFileName = ".\\TestFiles\\ExcludeMarkers.xml";
            MidiMappingRules mappingRules = MidiMappingRules.LoadFromXml(mappingFileName);

        }

        [Test]
        public void ExcludeKeySignature()
        {
            string mappingFileName = ".\\TestFiles\\ExcludeKeySignature.xml";
            MidiMappingRules mappingRules = MidiMappingRules.LoadFromXml(mappingFileName);
            Assert.IsTrue(mappingRules.ExcludeRules.Count == 1);
            string inFileName = ".\\TestFiles\\SimpleTestType0.mid";
            string outFileName = ".\\OutputFiles\\KeySignaturesExcluded.mid";
            if (!Directory.Exists(".\\OutputFiles"))
            {
                Directory.CreateDirectory(".\\OutputFiles");
            }
            bool converted = mappingRules.ConvertFile(inFileName, outFileName, -1);
            Assert.IsTrue(converted, "Failed to convert the file");

            Assert.IsTrue(HasKeySignature(inFileName), "Test file didn't contain a key signature");
            Assert.IsFalse(HasKeySignature(outFileName), "Key Signature Change was not filtered out");

        }

        private bool HasKeySignature(string midiFileName)
        {
            MidiFile midiFile = new MidiFile(midiFileName);
            bool foundKeySignature = false;
            for (int track = 0; track < midiFile.Tracks; track++)
            {
                foreach (MidiEvent midiEvent in midiFile.Events[track])
                {
                    KeySignatureEvent kse = midiEvent as KeySignatureEvent;
                    if (kse != null)
                    {
                        foundKeySignature = true;
                    }
                }
            }
            return foundKeySignature;
        }
    }
}
