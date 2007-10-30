using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MarkHeath.MidiUtils;
using NAudio.Midi;

namespace MappingTests
{
    [TestFixture]
    public class MappingTests
    {
        Random random;

        [SetUp]
        public void Setup()
        {
            random = new Random();
        }

        [Test]
        public void NoteNumberMap()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            mappingRules.NoteRules.Add(CreateNoteMap("45","*"));
            mappingRules.NoteRules.Add(CreateNoteMap("46", "47"));

            CheckNoteMap(45, 45, mappingRules);
            CheckNoteMap(46, 47, mappingRules);
            CheckNoteFilteredOut(47, mappingRules);
        }

        [Test]
        public void NoteChannelMap()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();

            NoteMap noteMap = new NoteMap();
            noteMap.InNotes = new InputValueParameters("*");
            noteMap.OutChannel = new NoteEventOutputParameters("7",1,16);
            mappingRules.NoteRules.Add(noteMap);

            CheckChannelMap(1, 7, mappingRules);
            CheckChannelMap(2, 7, mappingRules);
            CheckChannelMap(7, 7, mappingRules);
            CheckChannelMap(16, 7, mappingRules);
        }

        [Test]
        public void NoteRangeTest()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();

            NoteMap noteMap = new NoteMap();
            noteMap.InNotes = new InputValueParameters("3-10");
            noteMap.OutNote = new NoteEventOutputParameters("5", 0, 127);
            mappingRules.NoteRules.Add(noteMap);
            
            noteMap = new NoteMap();
            noteMap.InNotes = new InputValueParameters("20,22-24,26");
            noteMap.OutNote = new NoteEventOutputParameters("-3", 0, 127);

            mappingRules.NoteRules.Add(noteMap);

            CheckNoteFilteredOut(2, mappingRules);
            CheckNoteFilteredOut(11, mappingRules);
            CheckNoteFilteredOut(21, mappingRules);
            CheckNoteFilteredOut(25, mappingRules);
            CheckNoteFilteredOut(27, mappingRules);
            CheckNoteMap(3, 5, mappingRules);
            CheckNoteMap(10, 5, mappingRules);
            CheckNoteMap(5, 5, mappingRules);
            CheckNoteMap(20, 17, mappingRules);
            CheckNoteMap(22, 19, mappingRules);
            CheckNoteMap(23, 20, mappingRules);
            CheckNoteMap(24, 21, mappingRules);
            CheckNoteMap(26, 23, mappingRules);            
        }

        [Test]
        public void VelocityScale()
        {
            MidiMappingRules mappingRules = MidiMappingRules.LoadFromXml(".\\TestFiles\\QuietHatsLoudSnare.xml");
            CheckVelocityMap(42, 40, 30, mappingRules);
            CheckVelocityMap(44, 80, 60, mappingRules);
            CheckVelocityMap(46, 120, 90, mappingRules);
            
            CheckVelocityMap(38, 40, 50, mappingRules);
            CheckVelocityMap(39, 80, 100, mappingRules);
            CheckVelocityMap(40, 120, 127, mappingRules);


        }

        [Test]
        public void TransposeUp()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            mappingRules.NoteRules.Add(CreateNoteMap("*", "+1"));
            // this rule should never get run because the first rule should match everything
            mappingRules.NoteRules.Add(CreateNoteMap("45", "11"));

            CheckNoteMap(45, 46, mappingRules);
            CheckNoteMap(0, 1, mappingRules);
            CheckNoteMap(127, 127, mappingRules);
        }

        [Test]
        public void TransposeDown()
        {
            MidiMappingRules mappingRules = new MidiMappingRules();
            mappingRules.NoteRules.Add(CreateNoteMap("*", "-12"));
            // this rule should never get run because the first rule should match everything
            mappingRules.NoteRules.Add(CreateNoteMap("45", "11"));

            CheckNoteMap(45, 33, mappingRules);
            CheckNoteMap(0, 0, mappingRules);
            CheckNoteMap(5, 0, mappingRules);
            CheckNoteMap(127, 115, mappingRules);
        }


        private void CheckNoteFilteredOut(int inNote, MidiMappingRules mappingRules)
        {
            int velocity = random.Next(1, 127);
            int channel = random.Next(1, 16);
            int duration = random.Next(1, 5000);
            int absoluteTime = random.Next(1, 5000);
            NoteOnEvent noteOnEvent = new NoteOnEvent(absoluteTime, channel, inNote, velocity, duration);
            bool matched = mappingRules.Process(noteOnEvent,null);
            Assert.IsFalse(matched);
        }

        private void CheckChannelMap(int inChannel, int outChannel, MidiMappingRules mappingRules)
        {
            int velocity = random.Next(1, 127);
            int noteNumber = random.Next(0, 127); ;
            int duration = random.Next(1, 5000);
            int absoluteTime = random.Next(1, 5000);
            NoteOnEvent noteOnEvent = new NoteOnEvent(absoluteTime, inChannel, noteNumber, velocity, duration);
            bool matched = mappingRules.Process(noteOnEvent,null);
            Assert.IsTrue(matched);
            Assert.AreEqual(noteOnEvent.NoteNumber, noteNumber);
            Assert.AreEqual(noteOnEvent.Velocity, velocity);
            Assert.AreEqual(noteOnEvent.NoteLength, duration);
            Assert.AreEqual(noteOnEvent.Channel, outChannel);
            Assert.IsNotNull(noteOnEvent.OffEvent);
            Assert.AreEqual(noteOnEvent.OffEvent.NoteNumber, noteNumber);
            Assert.AreEqual(noteOnEvent.OffEvent.Channel, outChannel);
        }

        private void CheckNoteMap(int inNote, int outNote, MidiMappingRules mappingRules)
        {
            int velocity = random.Next(1, 127);
            int channel = random.Next(1, 16);
            int duration = random.Next(1, 5000);
            int absoluteTime = random.Next(1, 5000);
            NoteOnEvent noteOnEvent = new NoteOnEvent(absoluteTime, channel, inNote, velocity, duration);
            bool matched = mappingRules.Process(noteOnEvent,null);
            Assert.IsTrue(matched);
            Assert.AreEqual(noteOnEvent.NoteNumber, outNote);
            Assert.AreEqual(noteOnEvent.Velocity, velocity);
            Assert.AreEqual(noteOnEvent.NoteLength, duration);
            Assert.AreEqual(noteOnEvent.Channel, channel);
            Assert.IsNotNull(noteOnEvent.OffEvent);
            Assert.AreEqual(noteOnEvent.OffEvent.NoteNumber, outNote);
            Assert.AreEqual(noteOnEvent.OffEvent.Channel, channel);
        }

        private void CheckVelocityMap(int note, int inVelocity, int outVelocity, MidiMappingRules mappingRules)
        {
            int channel = random.Next(1, 16);
            int duration = random.Next(1, 5000);
            int absoluteTime = random.Next(1, 5000);
            NoteOnEvent noteOnEvent = new NoteOnEvent(absoluteTime, channel, note, inVelocity, duration);
            bool matched = mappingRules.Process(noteOnEvent,null);
            Assert.IsTrue(matched);
            Assert.AreEqual(note, noteOnEvent.NoteNumber);
            Assert.AreEqual(outVelocity, noteOnEvent.Velocity);
            Assert.AreEqual(duration, noteOnEvent.NoteLength);
            Assert.AreEqual(channel, noteOnEvent.Channel);
            Assert.IsNotNull(noteOnEvent.OffEvent);
            Assert.AreEqual(note, noteOnEvent.OffEvent.NoteNumber);
            Assert.AreEqual(channel, noteOnEvent.OffEvent.Channel);
        }


        private NoteMap CreateNoteMap(string inNotes, string outNote)
        {
            NoteMap noteMap = new NoteMap();
            noteMap.InNotes = new InputValueParameters(inNotes);
            noteMap.OutNote = new NoteEventOutputParameters(outNote, 0, 127);
            return noteMap;
        }
    }
}
