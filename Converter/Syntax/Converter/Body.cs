namespace Converter.Converter.Syntax
{
    public class Body : Node
    {
        internal Properties Properties { get; } = new Properties();
        internal Structs Structs { get; } = new Structs();
        internal Main Main { get; } = new Main();

        public string Name { get; set; } = string.Empty;

        public override string Build()
        {
            return Load("Body").
                   Replace("Name", Name).
                   Replace("Properties", Properties.Build()).
                   Replace("Structs", Structs.Build()).
                   Replace("Main", Main.Build()).
                   ToString();
        }
    }
}
