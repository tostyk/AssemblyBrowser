using System.Reflection;

namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Field
    {
        public string Name { get; }
        public string Type { get; }
        public AccessModifier AccessModifier { get; }
        public Field(string name, string type, AccessModifier accessModifier)
        {
            Name = name;
            Type = type;
            AccessModifier = accessModifier;
        }
        public Field(FieldInfo fieldInfo)
        {
            AccessModifier accessModifier = 0;
            if (fieldInfo.IsPublic) accessModifier = AccessModifier.Public;
            if (fieldInfo.IsPrivate) accessModifier = AccessModifier.Private;
            if (fieldInfo.IsFamily) accessModifier = AccessModifier.Protected;
            if (fieldInfo.IsAssembly) accessModifier = AccessModifier.Internal;
            Name = fieldInfo.Name;
            Type = fieldInfo.FieldType.Name;
            AccessModifier = accessModifier;
        }
    }
}
