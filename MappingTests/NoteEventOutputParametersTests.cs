using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MarkHeath.MidiUtils;

namespace MappingTests
{
    [TestFixture]
    public class NoteEventOutputParametersTests
    {
        [Test]
        public void CanSpecifyCopyInput()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters("*", 0, 127);
            for (int n = 0; n < 127; n++)
            {
                Assert.AreEqual(n, p.ProcessValue(n));
            }
        }

        [Test]
        public void CanSpecifyAmountToAdd()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters("+16", 0, 127);
            Assert.AreEqual(26, p.ProcessValue(10));
        }

        [Test]
        public void CanSpecifyAmountToSubtract()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters("-5", 0, 127);
            Assert.AreEqual(97, p.ProcessValue(102));
        }

        [Test]
        public void DoesNotAllowOutputToGoBelowMin()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters("-5", 0, 127);
            Assert.AreEqual(0, p.ProcessValue(4));
        }

        [Test]
        public void DoesNotAllowOutputToGoAboveMax()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters("+5", 0, 127);
            Assert.AreEqual(127, p.ProcessValue(125));
        }

        [Test]
        public void CanSpecifyPercent()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters("200%", 0, 127);
            Assert.AreEqual(64, p.ProcessValue(32));
        }

        [Test]
        public void SingleValueConstructorPerformsFixedValue()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters(64);
            Assert.AreEqual(64, p.ProcessValue(64));
            Assert.AreEqual(64, p.ProcessValue(120));
        }


        [Test]
        public void TwoValueConstructorAddsAndScales()
        {
            NoteEventOutputParameters p = new NoteEventOutputParameters(10, 50, 0, 127);
            Assert.AreEqual(26, p.ProcessValue(32));
            Assert.AreEqual(42, p.ProcessValue(64));
        }
    }
}
