using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    class ValueRange
    {
        int min;
        int max;

        public ValueRange(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int Minimum
        {
            get { return min; }
        }

        public int Maximum
        {
            get { return max; }
        }
    }
}
