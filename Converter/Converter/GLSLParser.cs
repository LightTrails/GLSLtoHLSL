using Converter.Converter.LineMapper;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Converter.Converter
{
    public class GLSLParser
    {
        private Line[] _lines;

        private string[] _properties;
        private string[] _main;

        public GLSLParser(string content)
        {
            _lines = content.Split('\n').Select(x => new Line(x)).ToArray();

            foreach (var line in _lines)
            {
                line.Convert();
            }

            _properties = ContentUntil(_lines, x => x.IsStartOfMain);
            _main = ContentFrom(_lines, x => x.IsStartOfMain);
        }

        public string GetProperties()
        {
            return string.Join("\n", _properties);
        }

        public string GetMain()
        {
            return string.Join("\n", _main);
        }

        internal string[] ContentUntil(Line[] lines, Func<Line, bool> end = null)
        {
            return ContentBetween(lines, null, end);
        }

        internal string[] ContentFrom(Line[] lines, Func<Line, bool> beginning = null)
        {
            return ContentBetween(lines, beginning, null);
        }

        internal string[] ContentBetween(Line[] lines, Func<Line, bool> beginning = null, Func<Line, bool> end = null)
        {
            List<string> values = new List<string>();

            bool startAdding = beginning == null;

            foreach (var line in lines)
            {
                if (startAdding && (end != null && end(line)))
                {
                    return values.ToArray();
                }

                if (startAdding)
                {
                    values.Add(line.Content);
                }

                if (beginning == null || beginning(line))
                {
                    startAdding = true;
                }
            }

            if (end == null)
            {
                return values.ToArray();
            }

            return null;
        }

        internal class Line
        {
            private Parser<bool> startOfMain =
                from leading in Parse.WhiteSpace.Many()
                from main in Parse.String("void mainImage")
                select true;

            private Parser<bool> endOfMain =
                    from leading in Parse.WhiteSpace.Many()
                    from main in Parse.String("}")
                    select true;

            /*private Parser<bool> vectorParameters =
                    from leading in Parse.WhiteSpace.Many()
                    from vec3 in Parse.WhiteSpace.Many()
                    from main in Parse.String("}")
                    select true;*/

            public bool IsStartOfMain => startOfMain(_input).WasSuccessful;
            public bool IsEndOfMain => endOfMain(_input).WasSuccessful;

            public string Content { get; private set; }
            private Input _input;

            private LineCorrector[] _correctors;

            public void Convert()
            {
                foreach (var corrector in _correctors)
                {
                    Content = corrector.CorrectLine(Content);
                }
            }

            public Line(string line)
            {
                Content = line;
                _input = new Input(line);

                _correctors = new LineCorrector[] {
                    new Vector(),
                    new Matrix(),
                    new Misc(),
                    new Operations(),
                    new Tan()
                };
            }

            public override string ToString()
            {
                return Content;
            }
        }
    }
}
