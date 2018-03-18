using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Converter.LineMapper
{
    public class Operations : LineCorrector
    {
        public override string CorrectLine(string line)
        {
            line = line.Replace("fract", "frac");
            line = line.Replace("mix", "lerp");
            line = line.Replace("mod", "fmod");

            return line;
        }
    }
}
