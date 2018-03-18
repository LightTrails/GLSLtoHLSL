using System;

namespace Converter.Converter.Syntax
{
    internal class Main : Node
    {
        public string Content { get; set; }

        public override string Build()
        {
            return Load("Main").Replace("Content", Content).ToString();
        }
    }
}