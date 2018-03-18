using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.Converter.LineMapper
{
    public class Vector : LineCorrector
    {
        public static int ParameterNumber = 0;

        public static readonly Parser<string> Parantheses =
            from start in Parse.String("(").Text()
            from content in Parse.AnyChar.Except(Parse.Chars("()")).Many().Text().Or(Parantheses).Many()
            from end in Parse.String(")").Text()
            select start + string.Join("", content) + end;

        public static readonly Parser<DimensionAndContent> VectorStart =
            from start in Parse.String("vec").Text()
            from digit in Parse.Digit
            from paran in Parse.String("(")
            select new DimensionAndContent() { VectorDimension = (int)Char.GetNumericValue(digit), Content = start + digit + paran };

        public static readonly Parser<string> VectorShortHand =
            from first in Parse.AnyChar.Except(VectorStart).Many().Text()
            from methodStart in VectorStart
            from between in (Parse.AnyChar.Except(Parse.Chars(",()")).Many().Text().Or(Parantheses)).Many()
            from methodEnd in Parse.Char(')')
            from end in Parse.AnyChar.Many().Text()
            select GenerateReplacement(first, methodStart, string.Join("", between), end);

        public static string GenerateReplacement(string first, DimensionAndContent methodStart, string between, string end)
        {
            var newParameter = "genP" + ParameterNumber++;
            string result = "float " + newParameter + " = " + between + ";" + Environment.NewLine;
            result += first + "float" + methodStart.VectorDimension + "(" + string.Join(",", Enumerable.Repeat(newParameter, methodStart.VectorDimension)) + ")" + end;
            return result;
        }

        /*
        select new DimensionAndContent()
        {
            VectorDimension = methodStart.VectorDimension,
            Content = first + "float2(" + afterParan + "," + afterParan + ")" + end
        };        */

        public class DimensionAndContent
        {
            public int VectorDimension;
            public string Content;
        }

        public override string CorrectLine(string line)
        {
            line = Correct(VectorShortHand, line);

            line = line.Replace("vec4", "float4");
            line = line.Replace("vec3", "float3");
            line = line.Replace("vec2", "float2");
            line = line.Replace("vec", "float");

            return line;
        }
    }
}
