namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Parameter
    {
        public string Type { get; }
        public string Name { get; }
        public Parameter(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
