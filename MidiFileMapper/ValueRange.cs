using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    public class ValueRange
    {
        static Random random = new Random();

        public ValueRange(int singleValue)
            : this(singleValue, singleValue)
        {
        }

        public ValueRange(int min, int max)
        {
            this.Minimum = min;
            this.Maximum = max;
        }

        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public int TakeRandom()
        {
            if (Minimum == Maximum)
                return Minimum;
            return random.Next(Minimum, Maximum + 1);
        }

        public static ValueRange Parse(string rangeString)
        {                    
            int dashIndex = rangeString.IndexOf('-');
            if (dashIndex != -1)
            {
                int first = Int32.Parse(rangeString.Substring(0, dashIndex));
                int last = Int32.Parse(rangeString.Substring(dashIndex + 1));
                return new ValueRange(first, last);
            }
            else
            {
                int note = Int32.Parse(rangeString);
                return new ValueRange(note, note);
            }
        }
    }
}
