using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MarkHeath.MidiUtils;

namespace MappingTests
{
    [TestFixture]
    public class InputValueParametersTests
    {
        [Test]
        public void CanSpecifySingleValueAsString()
        {
            InputValueParameters ivp = new InputValueParameters("45");
            Assert.IsTrue(ivp.IsValueIncluded(45));
            Assert.IsFalse(ivp.IsValueIncluded(44));
            Assert.IsFalse(ivp.IsValueIncluded(46));
        }

        [Test]
        public void CanSpecifyCommaSeparatedValues()
        {
            InputValueParameters ivp = new InputValueParameters("1,4,24,114");
            Assert.IsTrue(ivp.IsValueIncluded(1));
            Assert.IsTrue(ivp.IsValueIncluded(4));
            Assert.IsTrue(ivp.IsValueIncluded(24));
            Assert.IsTrue(ivp.IsValueIncluded(114));
            Assert.IsFalse(ivp.IsValueIncluded(2));
            Assert.IsFalse(ivp.IsValueIncluded(5));
            Assert.IsFalse(ivp.IsValueIncluded(127));
        }

        [Test]
        public void CanSpecifyASingleRange()
        {
            InputValueParameters ivp = new InputValueParameters("15-18");
            Assert.IsFalse(ivp.IsValueIncluded(14));
            Assert.IsTrue(ivp.IsValueIncluded(15));
            Assert.IsTrue(ivp.IsValueIncluded(16));
            Assert.IsTrue(ivp.IsValueIncluded(17));
            Assert.IsTrue(ivp.IsValueIncluded(18));
            Assert.IsFalse(ivp.IsValueIncluded(19));
        }

        [Test]
        public void CanSpecifyTwoRanges()
        {
            InputValueParameters ivp = new InputValueParameters("1-2;104-106");
            Assert.IsFalse(ivp.IsValueIncluded(0));
            Assert.IsTrue(ivp.IsValueIncluded(1));
            Assert.IsTrue(ivp.IsValueIncluded(2));
            Assert.IsFalse(ivp.IsValueIncluded(3));
            Assert.IsFalse(ivp.IsValueIncluded(103));
            Assert.IsTrue(ivp.IsValueIncluded(104));
            Assert.IsTrue(ivp.IsValueIncluded(105));
            Assert.IsTrue(ivp.IsValueIncluded(106));
            Assert.IsFalse(ivp.IsValueIncluded(107));
        }

        [Test]
        public void CanIgnoreWhitespace()
        {
            InputValueParameters ivp = new InputValueParameters(" 5 - 6 ; 15 , 104 , 33-34");
            Assert.IsFalse(ivp.IsValueIncluded(4));
            Assert.IsTrue(ivp.IsValueIncluded(5));
            Assert.IsTrue(ivp.IsValueIncluded(6));
            Assert.IsFalse(ivp.IsValueIncluded(7));
            Assert.IsFalse(ivp.IsValueIncluded(14));
            Assert.IsTrue(ivp.IsValueIncluded(15));
            Assert.IsFalse(ivp.IsValueIncluded(16));
            Assert.IsFalse(ivp.IsValueIncluded(32));
            Assert.IsTrue(ivp.IsValueIncluded(33));
            Assert.IsTrue(ivp.IsValueIncluded(34));
            Assert.IsFalse(ivp.IsValueIncluded(35));
            Assert.IsFalse(ivp.IsValueIncluded(103));            
            Assert.IsTrue(ivp.IsValueIncluded(104));
            Assert.IsFalse(ivp.IsValueIncluded(105));
        }

        [Test]
        public void CanSpecifyAll()
        {
            InputValueParameters ivp = new InputValueParameters("*");
            for (int n = 0; n < 128; n++)
            {
                Assert.IsTrue(ivp.IsValueIncluded(n));
            }
        }
    }
}
