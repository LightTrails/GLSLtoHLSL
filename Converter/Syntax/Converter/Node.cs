using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Converter.Converter.Syntax
{
    public abstract class Node
    {
        protected StringBuilder _builder;

        public Node()
        {
            _builder = new StringBuilder();
        }

        public Node Load(string name)
        {
            string TemplatePath = Path.GetFullPath("./Syntax/Converter/" + name + ".template");
            _builder.Append(File.Exists(TemplatePath) ? File.ReadAllText(TemplatePath) : string.Empty);
            return this;
        }

        public Node Replace(string name, string value)
        {
            _builder.Replace("{{" + name + "}}", value);
            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        public abstract string Build();
    }
}
