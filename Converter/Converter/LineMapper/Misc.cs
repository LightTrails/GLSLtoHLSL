using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Converter.LineMapper
{
    public class Misc : LineCorrector
    {
        public override string CorrectLine(string line)
        {
            line = line.Replace("iMouse.x", "0");
            line = line.Replace("iMouse.y", "0");

            line = line.Replace("fragCoord.xy", "(_ScreenParams.xy * input.uv)");
            line = line.Replace("iResolution.x", "_ScreenParams.x");
            line = line.Replace("iResolution.y", "_ScreenParams.y");

            line = line.Replace("iTime", "_Time[0]"/*"_InputTime"*/);
            line = line.Replace("iChannel0", "_MainTex");

            line = line.Replace("fragColor =", "return");

            line = line.Replace("texture(", "tex2D(");

            return line;
        }
    }
}
