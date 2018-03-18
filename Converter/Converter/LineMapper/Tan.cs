using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Converter.LineMapper
{
    public class Tan : LineCorrector
    {
        public static readonly Parser<string> ReplaceTan =
            from first in Parse.AnyChar.Except(Parse.String("atan(")).Many().Text()
            from start in Parse.String("atan(").Text()
            from firstParameter in Parse.AnyChar.Except(Parse.Chars(",")).Many().Text()
            from paran in Parse.String(",")
            from secondParameter in Parse.AnyChar.Except(Parse.Chars(")")).Many().Text()
            from end in Parse.AnyChar.Many().Text()
            select first + "atan2(" + secondParameter + "," + firstParameter + end;

        public override string CorrectLine(string line)
        {
            var result = ReplaceTan(new Input(line));

            if (result.WasSuccessful)
            {
                return result.Value;
            }

            return line;
        }
    }
}
