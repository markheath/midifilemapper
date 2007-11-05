using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;
using MarkHeath.MidiUtils;

namespace MappingTests
{
    [TestFixture]
    public class DrumMapTests
    {
        
        [Test]
        public void TestCubaseMappings()
        {
            // simply tests that the bundled mapping files can be loaded
            string mappingPath = ".\\TestFiles\\";
            int tested = 0;
            foreach (string file in Directory.GetFiles(mappingPath, "*.drm"))
            {
                
                Debug.WriteLine(String.Format("Opening {0}", file));
                MidiMappingRules mappingRules = MidiMappingRules.LoadFromCubaseDrumMap(file);
                Assert.IsTrue(mappingRules.NoteRules.Count > 0);
                Assert.IsTrue(mappingRules.EventRules.Count == 0);
                Assert.IsTrue(mappingRules.InsertEvents.Count == 0);
                tested++;
            }
            Assert.IsTrue(tested > 0);
        }
    }
}
