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
        {
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
                    ranges.Add(ValueRange.Parse(rangeString));
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
