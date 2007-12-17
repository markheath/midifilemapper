using System;
using System.Collections.Generic;
using System.Text;

namespace MarkHeath.MidiUtils
{
    public enum TextMatchType
    {
        /// <summary>
        /// Matches exactly
        /// </summary>
        ExactMatch,
        /// <summary>
        /// Is a substring
        /// </summary>
        Substring,
        /// <summary>
        /// Is a regular expression match
        /// </summary>
        Regex
    }
}
