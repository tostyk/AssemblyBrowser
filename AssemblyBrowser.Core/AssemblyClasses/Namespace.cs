namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Namespace
    {
        public string Name;
        public List<Type> Types = new List<Type>();
        public Namespace(string name)
        {
            Name = name;
        }
    }
}
