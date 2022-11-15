namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Namespace
    {
        public string Name { get; }
        public List<Type> Types { get; internal set; }
        public Namespace(string name)
        {
            Name = name;
            Types = new List<Type>();
        }
    }
}
