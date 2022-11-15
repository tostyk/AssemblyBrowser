using System.Reflection;

namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Method
    {
        public string Name { get; }
        public string ReturnType { get; }
        public bool IsExtensionMethod { get; }
        public AccessModifier AccessModifier { get; }
        public Parameter[] Parameters { get; internal set; }
        public Method(string name, string returnType, AccessModifier accessModifier)
        {
            Name = name;
            ReturnType = returnType;
            IsExtensionMethod = false;
            AccessModifier = accessModifier;
        }
        public Method(string name, string returnType, AccessModifier accessModifier, bool isExtensionMethod)
        {
            Name = name;
            ReturnType = returnType;
            IsExtensionMethod = isExtensionMethod;
            AccessModifier = accessModifier;
        }
    }
}
