using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    public class InputValueParameters
    {
        bool singleValue;
        bool allValues;
        int value;
        List<ValueRange> ranges;
        int min;
        int max;

        private InputValueParameters()
        {
            allValues = true;
        }

        public InputValueParameters(int value)
        {
            this.singleValue = true;
            this.value = value;
        }
        
        public InputValueParameters(string setting)
            : this(setting, 0, 127)
        {
        }

        public InputValueParameters(string setting, int min, int max)
        {
            this.min = min;
            this.max = max;
            ranges = new List<ValueRange>();
            
            setting = setting.Replace(" ", "");
            if (setting == "*")
            {
                allValues = true;
            }
            else
            {
                string[] rangeStrings = setting.Split(',', ';');
                foreach (string rangeString in rangeStrings)
                {
                    int dashIndex = rangeString.IndexOf('-');
                    if (dashIndex != -1)
                    {
                        int first = Int32.Parse(rangeString.Substring(0, dashIndex));
                        int last = Int32.Parse(rangeString.Substring(dashIndex + 1));
                        ranges.Add(new ValueRange(first, last));
                    }
                    else
                    {
                        int note = Int32.Parse(rangeString);
                        ranges.Add(new ValueRange(note, note));

                    }
                }
                if (ranges.Count == 1)
                {
                    if (ranges[0].Minimum == ranges[0].Maximum)
                    {
                        singleValue = true;
                        value = ranges[0].Minimum;
                    }
                }
            }
        }
        
        public bool IsValueIncluded(int input)
        {
            if (allValues)
                return true;
            if (singleValue)
                return input == this.value;

            foreach (ValueRange range in ranges)
            {
                if ((input >= range.Minimum) && (input <= range.Maximum))
                    return true;
            }
            return false;
        }
    }
}
