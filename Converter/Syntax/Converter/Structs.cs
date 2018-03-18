using System;

namespace Converter.Converter.Syntax
{
    internal class Structs : Node
    {
        public string ExtraStructs { get; set; }

        public override string Build()
        {
            return Load("Structs").Replace("Structs", ExtraStructs).ToString();

        }
    }
}