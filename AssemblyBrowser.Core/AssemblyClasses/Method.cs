namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Method
    {
        public string Name;
        public string ReturnType;
        public Parameter[] Parameters;
        public bool IsExtensionMethod;
        public Method(string name, string returnType)
        {
            Name = name;
            ReturnType = returnType;
            IsExtensionMethod = false;
        }
        public Method(string name, string returnType, bool isExtensionMethod)
        {
            Name = name;
            ReturnType = returnType;
            IsExtensionMethod = isExtensionMethod;
        }
    }
}
