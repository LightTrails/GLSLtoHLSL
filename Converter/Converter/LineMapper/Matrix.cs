using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Converter.LineMapper
{
    public class Matrix : LineCorrector
    {
        public override string CorrectLine(string line)
        {
            line = line.Replace("mat2", "float2x2");
            line = line.Replace("mat3", "float3x3");
            line = line.Replace("mat4", "float4x4");

            return line;
        }
    }
}
