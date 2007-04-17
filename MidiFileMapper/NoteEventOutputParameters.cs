using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    public class NoteEventOutputParameters
    {
        bool copyInput;
        bool fixedValue;
        int value;
        int min;
        int max;
        int percent;

        private NoteEventOutputParameters()
        {
            copyInput = true;
        }

        public NoteEventOutputParameters(int value)
            : this(value, 100)
        {
        }

        public NoteEventOutputParameters(int value, int percent)
        {
            fixedValue = true;
            this.value = value;
            this.min = value;
            this.max = value;
            this.percent = percent;
        }

        public int Value
        {
            get { return value; }
        }

        public NoteEventOutputParameters(string setting, int min, int max)
        {
            this.min = min;
            this.max = max;
            this.percent = 100;

            if (setting == "*")
            {
                copyInput = true;
            }
            else if (setting.StartsWith("+"))
            {
                value = Int32.Parse(setting.Substring(1));
            }
            else if (setting.StartsWith("-"))
            {
                value = 0 - Int32.Parse(setting.Substring(1));
            }
            else if (setting.EndsWith("%"))
            {
                value = 0;
                percent = Int32.Parse(setting.Substring(0, setting.Length - 1));
            }
            else
            {
                fixedValue = true;
                value = Int32.Parse(setting);
            }
        }
        
        public int ProcessValue(int input)
        {
            if (copyInput)
                return input;
            if (fixedValue)
                return value;

            if (percent != 100)
            {
                input = (input * percent) / 100;
            }

            int output = input + value;


            if (output < min)
                output = min;
            if (output > max)
                output = max;

            

            return output;
        }
    }
}
