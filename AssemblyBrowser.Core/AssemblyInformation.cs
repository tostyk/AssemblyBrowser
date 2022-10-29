namespace AssemblyBrowser.Core
{
    public class AssemblyInformation
    {
        public List<Namespace> Namespaces = new List<Namespace>();
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
        public string Name;
        public List<Member> Members = new List<Member>();
        public DataType(string name)
        {
            Name = name;
        }
    }
    public abstract class Member
    {
        public string Name;
        public Member(string name)
        {
            Name = name;
        }
    }
    // Fields and properties
    public class DataMember : Member
    {
        public string Type;
        public DataMember(string name, string type):base(name)
        {
            Type = type;
        }
    }
    // Methods
    public class ActionMember : Member
    {
        public string Name;
        public string ReturnType;
        public Parameter[] Parameters;
        public ActionMember(string name, string returnType) : base(name) 
        {
            ReturnType = returnType;
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
