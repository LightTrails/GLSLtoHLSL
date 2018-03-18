using Converter.Converter;
using Converter.Converter.LineMapper;
using NUnit.Framework;
using Sprache;
using System;
using System.IO;

namespace Converter
{
    [TestFixture]
    public class Test
    {
        [Test, Explicit]
        public void GenerateBasic()
        {
            var test = new HLSLShader("Test");
            Utils.SaveToFile("Test", test);
        }

        [Test, Explicit]
        public void Parser()
        {
            string lineToParse = "vec2(fract(15.32354 * (r + xb1)))";

            var result = Vector.VectorShortHand(new Input(lineToParse));

            Console.WriteLine(result);
        }

        [Test, Explicit]
        public void Test2()
        {
            string lineToParse = "(15.32354 * (r + xb1))";

            var result = Vector.Parantheses(new Input(lineToParse));

            Console.WriteLine(result);
        }

        [Test, Explicit]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void CopyFile(string fileName)
        {
            Utils.Convert(fileName);
            Utils.CopyOutput(fileName);
        }

        [Test, Explicit]
        [TestCase("var test = float3(1)", "var test = float3(1,1,1)")]
        public void Parameter(string before, string after)
        {
            Parser<string> correctMethod =
                from first in Parse.AnyChar.Except(Parse.String("float3(")).Many().Text()
                from methodStart in Parse.String("float3(")
                from parameter in Parse.AnyChar.Except(Parse.Chars("(),")).Many().Text()
                from methodEnd in Parse.Char(')')
                from end in Parse.AnyChar.Many().Text()
                select first + "float3(" + parameter + "," + parameter + "," + parameter + ")" + end;

            var result = correctMethod(new Input(before));

            if (!result.WasSuccessful)
            {
                Assert.Fail(result.ToString());
            }

            Assert.That(result.Value, Is.EqualTo(after));
        }
    }
}

