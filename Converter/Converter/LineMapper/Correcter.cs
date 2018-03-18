using Sprache;

namespace Converter.Converter.LineMapper
{
    public abstract class LineCorrector
    {
        public abstract string CorrectLine(string line);

        protected string Correct(Parser<string> parser, string input)
        {
            var result = parser(new Input(input));
            if (result.WasSuccessful)
            {
                return Correct(parser, result.Value);
            }

            return input;
        }
    }
}