namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Type
    {
        public string Name { get; }
        public AccessModifier AccessModifier { get; }
        public List<Field> Fields { get; internal set; }
        public List<Property> Properties { get; internal set; }
        public List<Method> Methods { get; internal set; }
        public Type(string name)
        {
            Name = name;
            Fields = new List<Field>();
            Properties = new List<Property>();
            Methods = new List<Method>();
        }
    }
}
