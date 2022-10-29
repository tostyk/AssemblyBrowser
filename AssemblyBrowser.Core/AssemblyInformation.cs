namespace AssemblyBrowser.Core
{
    public class AssemblyInformation
    {
        public string AssemblyName;
        public List<Namespace> Namespaces = new List<Namespace>();
        public AssemblyInformation(string name)
        {
            AssemblyName = name;
        }
    }
    public class Namespace
    {
        public string Name;
        public List<DataType> DataTypes = new List<DataType>();
        public Namespace(string name)
        {
            Name = name;
        }
    }
    public class DataType
    {
        public string TypeName;
        public List<Field> Fields = new List<Field>();
        public List<Property> Properties = new List<Property>();
        public List<Method> Methods = new List<Method>();
        public DataType(string name)
        {
            TypeName = name;
        }
    }
    public class Field
    {
        public string Name;
        public string Type;
        public Field(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
    public class Property
    {
        public string Name;
        public string Type;
        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
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
    public class Parameter
    {
        public string Type;
        public string Name;
        public Parameter(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
