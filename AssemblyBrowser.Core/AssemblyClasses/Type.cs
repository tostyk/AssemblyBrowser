namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Type
    {
        public string Name;
        public AccessModifier AccessModifier;
        public List<Field> Fields = new List<Field>();
        public List<Property> Properties = new List<Property>();
        public List<Method> Methods = new List<Method>();
        public Type(string name)
        {
            Name = name;
        }
    }
}
