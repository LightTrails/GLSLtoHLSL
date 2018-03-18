using Converter.Converter;
using Converter.Converter.Syntax;
using System.IO;
using System.Text;

namespace Converter
{
    public class HLSLShader
    {
        private Body Body;

        public HLSLShader(string name)
        {
            Body = new Body()
            {
                Name = name
            };
        }

        public void SetFrom(GLSLParser glsl)
        {
            Body.Structs.ExtraStructs = glsl.GetProperties();
            Body.Main.Content = glsl.GetMain();
        }

        public string Build()
        {
            return Body.Build();
        }
    }
}
