using System.Reflection;

namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Method
    {
        public string Name;
        public string ReturnType;
        public bool IsExtensionMethod;
        public AccessModifier AccessModifier;
        public Parameter[] Parameters;
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
