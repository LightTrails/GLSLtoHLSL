using Converter.Converter;
using System.IO;

namespace Converter
{
    public static class Utils
    {
        public static string InputDirectory = Path.GetFullPath("./../../../Input");
        public static string OutputDirectory = Path.GetFullPath("./../../../Output");

        public static string UnityShaderFolder = Path.GetFullPath(@"C:\Users\Rasmus Færing Larsen\Documents\GitHub\TestShader\Assets\Shaders");

        public static GLSLParser LoadShader(string name)
        {
            var input = Path.Combine(InputDirectory, name + ".shader");
            var content = File.ReadAllText(input);

            return new GLSLParser(content);
        }

        public static string GetFile(string fileName)
        {
            return Path.Combine(OutputDirectory, fileName + ".shader");
        }

        public static void SaveToFile(string fileName, HLSLShader hlsl)
        {
            var file = GetFile(fileName);

            if (!File.Exists(file))
            {
                File.Create(file);
            }

            File.WriteAllText(file, hlsl.Build());
        }

        public static void Convert(string name)
        {
            var shader = LoadShader(name);

            var hlsl = new HLSLShader(name);
            hlsl.SetFrom(shader);

            SaveToFile(name, hlsl);
        }

        public static void CopyOutput(string name)
        {
            var item = GetFile(name);
            var fileName = Path.GetFileName(item);
            File.Copy(item, Path.Combine(UnityShaderFolder, fileName), true);
        }

        public static void CopyOutput()
        {
            foreach (var item in Directory.GetFiles(OutputDirectory))
            {
                var fileName = Path.GetFileName(item);
                File.Copy(item, Path.Combine(UnityShaderFolder, fileName), true);
            }
        }
    }
}
