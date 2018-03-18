using System;

namespace Converter.Converter.Syntax
{
    internal class Properties : Node
    {
        public override string Build()
        {
            ;
            return Load("Properties").ToString();
        }
    }
}